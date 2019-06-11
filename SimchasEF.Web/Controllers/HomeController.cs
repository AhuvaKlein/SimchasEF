using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimchasEF.Web.Models;
using SimchasEF.Data;
using Microsoft.Extensions.Configuration;

namespace SimchasEF.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        private SimchaRepository _repo;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _repo = new SimchaRepository(_connectionString);
        }

        public IActionResult Index()
        {
            IndexViewModel vm = new IndexViewModel();
            vm.Simchas = _repo.GetAllSimchas();
            vm.TotalContributors = _repo.GetTotalContributorCount();
            foreach (Simcha s in vm.Simchas)
            {
                s.TotalContributorsForSimcha = _repo.GetTotalContributorsForSimcha(s.Id);
            }
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddSimcha(Simcha simcha)
        {
            _repo.AddSimcha(simcha);
            return Redirect("/");
        }

        public IActionResult Contributions(int id)
        {
            ContributionsViewModel vm = new ContributionsViewModel
            {
                Contributors = _repo.GetThisSimchaContributors(id),
                Simcha = _repo.GetSimchaById(id)
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddContributor(Contributor contributor, Deposit deposit)
        {
            _repo.AddContributor(contributor);
            deposit.ContributorId = contributor.Id;
            _repo.AddDeposit(deposit);
            return Redirect("/home/displaycontributors");
        }

        public IActionResult DisplayContributors()
        {
            ContributorsViewModel vm = new ContributorsViewModel
            {
                Contributors = _repo.GetAllContributors(),
                TotalBalance = _repo.GetTotalBalance()
            };
            foreach (Contributor c in vm.Contributors)
            {
                c.Balance = c.Deposits.Sum(con => con.Amount) - c.Contributions.Sum(con => con.Amount);
            }

            return View(vm);
        }

        public IActionResult History(int id)
        {
            HistoryViewModel vm = new HistoryViewModel();
            vm.Contributor = _repo.GetContributorById(id);
            IEnumerable<Deposit> deposits = vm.Contributor.Deposits;
            foreach (Deposit d in deposits)
            {
                d.Action = "Deposit";
            }
            IEnumerable<Contribution> contributions = vm.Contributor.Contributions;
            foreach (Contribution c in contributions)
            {
                c.Action = $"Contribution for {c.Simcha.Name} Simcha";
            }
            List<ContributorHistory> history = new List<ContributorHistory>();
            foreach (Deposit d in deposits)
            {
                history.Add(new ContributorHistory
                {
                    Action = d.Action,
                    Date = d.Date,
                    Amount = d.Amount
                });
            }
            foreach (Contribution c in contributions)
            {
                history.Add(new ContributorHistory
                {
                    Action = c.Action,
                    Date = c.Simcha.Date,
                    Amount = c.Amount
                });
            }
            vm.ContributionsDeposits = history.OrderBy(c => c.Date);


            return View(vm);
        }

        [HttpPost]
        public IActionResult EditContributor(Contributor contributor)
        {
            _repo.EditContributor(contributor);
            return Redirect("/home/displaycontributors");
        }

        [HttpPost]
        public IActionResult AddDeposit(Deposit deposit)
        {
            _repo.AddDeposit(deposit);
            return Redirect("/home/displaycontributors");
        }

        [HttpPost]
        public IActionResult UpdateContributions(IEnumerable<SimchaContributor> contributors, int id)
        {
            _repo.DeleteContributions(id);
            _repo.UpdateContributions(contributors, id);
            return Redirect("/");
        }
    }
}
