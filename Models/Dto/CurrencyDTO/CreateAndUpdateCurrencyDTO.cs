using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ApiMoneda.Models.Dto
{
    public class CreateAndUpdateCurrencyDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Symbol { get; set; }
        [Required]

        public decimal Value { get; set; }
    }
}
