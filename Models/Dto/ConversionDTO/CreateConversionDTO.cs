namespace ApiMoneda.Models.Dto.ConversionDTO
{
    public class CreateConversionDTO
    {
        public int UserId { get; set; }
       // public int FirstCurrencyId { get; set; }
        public string FirstCurrencyName { get; set; }
        public decimal FirstCurrencyAmount { get; set; } // Cantidad a convertir
        //public int SecondCurrencyId { get; set; }
        public string SecondCurrencyName { get; set; }
        public decimal ConvertedAmount { get; set; } // Cantidad convertida
    }
}
