using SimchasEF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimchasEF.Web.Models
{
    public class HistoryViewModel
    {
        public IEnumerable<ContributorHistory> ContributionsDeposits { get; set; }
        public Contributor Contributor { get; set; }
    }
}
