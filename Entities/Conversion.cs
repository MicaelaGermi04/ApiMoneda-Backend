using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiMoneda.Entities
{
    public class Conversion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FirstCurrencyName { get; set; }
        public string SecondCurrencyName { get; set; }
        public decimal FirstCurrencyAmount { get; set; }
        public decimal ConvertedAmount { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }

    }
}
