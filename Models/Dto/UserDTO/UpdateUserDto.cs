using ApiMoneda.Entities;
using System.ComponentModel.DataAnnotations;
namespace ApiMoneda.Models.Dto
{
    public class UpdateUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int SubscriptionId { get; set; }
    }
}
