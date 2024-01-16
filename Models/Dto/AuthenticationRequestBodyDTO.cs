using System.ComponentModel.DataAnnotations;

namespace ApiMoneda.Models.Dto
{
    public class AuthenticationRequestBodyDTO
    {
     [Required]
     public string? UserName { get; set; }
     [Required]
     public string? Password { get; set; }
    }
}
