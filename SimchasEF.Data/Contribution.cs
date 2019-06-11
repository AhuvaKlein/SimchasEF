using System.ComponentModel.DataAnnotations.Schema;

namespace SimchasEF.Data
{
    public class Contribution
    {
        public int SimchaId { get; set; }
        public Simcha Simcha { get; set; }
        public int ContributorId { get; set; }
        public Contributor Contributor { get; set; }
        public decimal Amount { get; set; }

        [NotMapped]
        public string Action { get; set; }
    }

}
