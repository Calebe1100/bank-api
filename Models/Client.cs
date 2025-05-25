using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bank_api.Models
{
    public class Client
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(11)]
        [Column("document")]
        public string Document { get; set; }

        [Required]
        [Column("phone")]
        public string Phone { get; set; }
    }
}
