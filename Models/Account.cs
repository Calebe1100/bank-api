using System.ComponentModel.DataAnnotations.Schema;

namespace bank_api.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdClient { get; set; }
        public string Number { get; set; }
    }
}
