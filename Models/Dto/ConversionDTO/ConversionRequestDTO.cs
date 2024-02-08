namespace ApiMoneda.Models.Dto.ConversionDTO
{
    public class ConversionRequestDTO
    {
        public int UserId { get; set; }
        public string FirstCurrencyName { get; set; }
        public string SecondCurrencyName { get; set; }
        public decimal FirstCurrencyAmount { get; set; }
    }
}
