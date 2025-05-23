using System.ComponentModel.DataAnnotations;

namespace bank_api.Models
{
    public class Client
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(11)]
        public string Document { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
