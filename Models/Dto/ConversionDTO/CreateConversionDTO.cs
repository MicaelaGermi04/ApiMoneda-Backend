namespace ApiMoneda.Models.Dto.ConversionDTO
{
    public class CreateConversionDTO
    {
        public int UserId { get; set; }
        public string FirstCurrencyName { get; set; }
        public decimal FirstCurrencyAmount { get; set; } // Cantidad a convertir
        public string SecondCurrencyName { get; set; }
        public decimal ConvertedAmount { get; set; } // Cantidad convertida
    }
}
