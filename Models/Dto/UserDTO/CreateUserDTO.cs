using ApiMoneda.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApiMoneda.Models.Dto
{
    public class CreateUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        
    }
}
