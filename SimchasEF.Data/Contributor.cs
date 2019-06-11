using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimchasEF.Data
{
    public class Contributor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cell { get; set; }
        public DateTime DateJoined { get; set; }
        public bool AlwaysInclude { get; set; }
        public List<Contribution> Contributions { get; set; }
        public List<Deposit> Deposits { get; set; }

        [NotMapped]
        public decimal Balance { get; set; }
    }

}
