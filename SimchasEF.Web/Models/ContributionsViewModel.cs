using SimchasEF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimchasEF.Web.Models
{
    public class ContributionsViewModel
    {
        public IEnumerable<SimchaContributor> Contributors { get; set; }
        public Simcha Simcha { get; set; }
    }
}
