using SimchasEF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimchasEF.Web.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Simcha> Simchas { get; set; }
        public int TotalContributors { get; set; }
    }
}
