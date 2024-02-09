using ApiMoneda.Data;
using ApiMoneda.Entities;
using ApiMoneda.Models.Dto.ConversionDTO;
using ApiMoneda.Services.Interface;

namespace ApiMoneda.Services.Implementations
{
    public class ConversionService : IConversionService
    {
        private readonly ConversorContext _context;
        public ConversionService(ConversorContext context)
        {
            _context = context;
        }

        public List<Conversion> GetAll(int userId)
        {
            return _context.Conversions.Where(c => c.User.Id == userId).ToList();
        }
        public void CreateConversion(CreateConversionDTO dto)
        {
            Conversion conversion = new Conversion()
            {
                Date = DateTime.Now,
                UserId = dto.UserId,
                FirstCurrencyName = dto.FirstCurrencyName,
                FirstCurrencyAmount = dto.FirstCurrencyAmount,
                SecondCurrencyName = dto.SecondCurrencyName,
                ConvertedAmount = dto.ConvertedAmount,
            };
            _context.Conversions.Add(conversion);
            _context.SaveChanges();
        }

        //Contador de conversiones por mes 
        public int ConversionCounter(int userId)
        {

            DateTime today = DateTime.Today;

            // el primer día del mes actual
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

            // el último día del mes actual
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            int conversionCount = _context.Conversions.Count(c => c.UserId == userId && c.Date >= firstDayOfMonth && c.Date <= lastDayOfMonth);

            return conversionCount;
        }
    }
    
};
