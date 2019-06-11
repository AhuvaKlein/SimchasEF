using System;
using System.Collections.Generic;
using System.Text;

namespace SimchasEF.Data
{
    public class SimchaContributor
    {
        public int SimchaId { get; set; }
        public Contributor Contributor { get; set; }
        public int ContributorId { get; set; }
        public bool Contributed { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
    }
}
