using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SimchasEF.Data
{

    public class SimchaRepository
    {
        private string _connectionString;

        public SimchaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Simcha> GetAllSimchas()
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                return ctx.Simchas.Include(s => s.Contributions).ToList();
            }
        }

        public void AddSimcha(Simcha simcha)
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                ctx.Simchas.Add(simcha);
                ctx.SaveChanges();
            }
        }

        public void AddContributor(Contributor contributor)
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                ctx.Contributors.Add(contributor);
                ctx.SaveChanges();
            }
        }

        public void EditContributor(Contributor contributor)
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                ctx.Database.ExecuteSqlCommand(
                    "UPDATE Contributors SET Name = @name, Cell = @cell, DateJoined=@datejoined, AlwaysInclude = @alwaysinclude WHERE Id = @id",
                    new SqlParameter("@id", contributor.Id), new SqlParameter("@name", contributor.Name),
                    new SqlParameter("@cell", contributor.Cell), new SqlParameter("@alwaysinclude", contributor.AlwaysInclude),
                    new SqlParameter("@datejoined", contributor.DateJoined));
                ctx.SaveChanges();
            }
        }

        public void AddDeposit(Deposit deposit)
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                ctx.Deposits.Add(deposit);
                ctx.SaveChanges();
            }
        }

        public decimal GetTotalBalance()
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                return ctx.Deposits.Sum(d => d.Amount) - ctx.Contributions.Sum(c => c.Amount);
            }
        }

        public IEnumerable<Contributor> GetAllContributors()
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                return ctx.Contributors.Include(c => c.Contributions).Include(c => c.Deposits).ToList();
            }
        }

        public Simcha GetSimchaById(int id)
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                return ctx.Simchas.FirstOrDefault(s => s.Id == id);
            }
        }

        public IEnumerable<SimchaContributor> GetThisSimchaContributors(int id)
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                IEnumerable<Contributor> contributors = GetAllContributors();
                IEnumerable<SimchaContributor> sc = contributors.Select(c =>
                {
                    var s = new SimchaContributor();
                    s.Contributor = c;
                    s.Contributed = AlreadyContributed(c, id);
                    s.SimchaId = id;
                    s.Balance = c.Deposits.Sum(d => d.Amount) - c.Contributions.Sum(d => d.Amount);
                    if (c.Contributions.FirstOrDefault(con => con.SimchaId == id) != null)
                    {
                        s.Amount = c.Contributions.FirstOrDefault(con => con.SimchaId == id).Amount;
                    }
                    else
                    {
                        s.Amount = 0;
                    }
                    return s;

                });
                return sc;
            }
        }

        private bool AlreadyContributed(Contributor contributor, int simchaId)
        {
            return contributor.Contributions.Any(c => c.SimchaId == simchaId);
        }

        //private Contribution GetAmountContributed(Contributor contributor, int simchaId)
        //{
        //    return contributor.Contributions.FirstOrDefault(c => c.SimchaId == simchaId);
        //}

        public int GetTotalContributorCount()
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                return ctx.Contributors.Count();
            }
        }

        public int GetTotalContributorsForSimcha(int id)
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                return ctx.Contributions.Where(s => s.SimchaId == id).Count();
            }
        }

        public Contributor GetContributorById(int id)
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                return ctx.Contributors.Include(c => c.Contributions).ThenInclude(c => c.Simcha).Include(c => c.Deposits).FirstOrDefault(c => c.Id == id);
            }
        }

        public void DeleteContributions(int simchaId)
        {
            using (var ctx = new SimchaContext(_connectionString))
            {
                ctx.Contributions.RemoveRange(ctx.Contributions.Where(c => c.SimchaId == simchaId));
                ctx.SaveChanges();
            }
        }

        public void UpdateContributions(IEnumerable<SimchaContributor> contributors, int simchaId)
        {
            IEnumerable<SimchaContributor> con = contributors.Where(c => c.Contributed);

            using (var ctx = new SimchaContext(_connectionString))
            {
                foreach (SimchaContributor sc in con)
                {
                    ctx.Contributions.Add(new Contribution { ContributorId = sc.ContributorId, SimchaId = sc.SimchaId, Amount = sc.Amount });
                }
                ctx.SaveChanges();
            }
        }

    }

}
