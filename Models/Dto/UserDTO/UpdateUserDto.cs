using ApiMoneda.Entities;
using System.ComponentModel.DataAnnotations;
namespace ApiMoneda.Models.Dto
{
    public class UpdateUserDto
    {
        [Required]
        public string FistName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int SubscriptionId { get; set; }
    }
}
