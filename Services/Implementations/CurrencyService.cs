using ApiMoneda.Data;
using ApiMoneda.Entities;
using ApiMoneda.Models.Dto;
using ApiMoneda.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiMoneda.Service.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ConversorContext _context;
        public CurrencyService(ConversorContext context)
        {
            _context = context;
        }

        public Currency? GetById(int Id)
        {
            return _context.Currencies.SingleOrDefault(c => c.Id == Id);
        }

        public Currency? GetByName(string name) 
        {
            return _context.Currencies.SingleOrDefault(c=> c.Name == name);
        }
        public List<Currency> GetAll()
        {
            return _context.Currencies.ToList();
        }
        public void Create(CreateAndUpdateCurrencyDTO dto)
        {
            Currency currency = new Currency()
            {
                Name = dto.Name,
                ISOcode = dto.isOcode,
                Value = dto.Value,
            };
            _context.Currencies.Add(currency);
            _context.SaveChanges();
        }
        public void Update(CreateAndUpdateCurrencyDTO dto, int id)
        {
            Currency currencyToUpdate = _context.Currencies.SingleOrDefault(c =>c.Id == id);
            currencyToUpdate.Name = dto.Name;
            currencyToUpdate.ISOcode = dto.isOcode;
            currencyToUpdate.Value = dto.Value;

            _context.SaveChanges();
        }
        public void DeleteCurrency(int id)
        {
            _context.Currencies.Remove(_context.Currencies.Single(u => u.Id == id));
            _context.SaveChanges();
        }
        public bool CheckIfCurrencyExists(int id)
        {
            Currency? currency = _context.Currencies.FirstOrDefault(c => c.Id == id);
            if (currency == null)
            {
                return false;
            }
            return true;
        }

    }
}
