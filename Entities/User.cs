using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiMoneda.Models.Enum;

namespace ApiMoneda.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Email { get; set; }

        public Role Role { get; set; } = Role.User;



        [ForeignKey("SubscriptionId")]
        public Subscription Subscription { get; set; }
        public int SubscriptionId { get; set; } = 1;

        
        public List<Currency> Currencies { get; set; }

        public List<Conversion> Conversions { get; set; }

    }
}
