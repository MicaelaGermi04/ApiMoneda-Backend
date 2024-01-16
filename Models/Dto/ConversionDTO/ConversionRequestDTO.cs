namespace ApiMoneda.Models.Dto.ConversionDTO
{
    public class ConversionRequestDTO
    {
        public int UserId { get; set; }
        public int FirstCurrencyId { get; set; }
        public int SecondCurrencyId { get; set; }
        public decimal FirstCurrencyAmount { get; set; }
    }
}
