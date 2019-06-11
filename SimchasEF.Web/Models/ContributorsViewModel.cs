using SimchasEF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimchasEF.Web.Models
{
    public class ContributorsViewModel
    {
        public IEnumerable<Contributor> Contributors { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
