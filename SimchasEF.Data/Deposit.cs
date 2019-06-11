using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimchasEF.Data
{
    public class Deposit
    {
        public int Id { get; set; }
        public int ContributorId { get; set; }
        public Contributor Contributor { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        [NotMapped]
        public string Action { get; set; }
    }

}
