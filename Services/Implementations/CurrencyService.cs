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
            var CurrencyToDelete = _context.Currencies.Single(c => c.Id == id);
            if (CurrencyToDelete is not null)
            {
                _context.Currencies.Remove(CurrencyToDelete);
            }
            _context.SaveChanges();
        }
        public bool CheckIfCurrencyExists(int id)
        {
            Currency? currency = _context.Currencies.FirstOrDefault(c => c.Id == id);
            return currency != null;
        }

        public List<Currency> GetFavouriteCurrencies(int userId)
        {
            // Obtiene el usuario con el id especificado e incluye las monedas favoritas
            User user = _context.Users
                .Include(u => u.Currencies)
                .FirstOrDefault(u => u.Id == userId);

            // Si el usuario no existe o no tiene monedas favoritas, devuelve una lista vacía
            if (user == null || user.Currencies == null)
            {
                return new List<Currency>();
            }

            // Devuelve las monedas favoritas del usuario, seleccionando solo las propiedades de Currency
            return user.Currencies.Select(c=> new Currency
            {
                Id = c.Id,
                Name = c.Name,
                ISOcode = c.ISOcode,
                Value = c.Value,
            }).ToList();
        }
        
        public void AddFavouriteCurrency(int currencyId, int userId)
        {
            var user = _context.Users.Include( u => u.Currencies).FirstOrDefault(u => u.Id == userId);
            var currency = _context.Currencies.FirstOrDefault(c=> c.Id == currencyId);

            if( user != null && currency != null )
            {
                user.Currencies.Add(currency);
                _context.SaveChanges();
            }
        }
        public void DeleteFavouriteCurrency(int currencyId, int userId)
        {
            var user = _context.Users.Include(u => u.Currencies).FirstOrDefault(u => u.Id == userId);
            var currency = _context.Currencies.FirstOrDefault(c => c.Id == currencyId);

            if( user != null && currency != null )
            {
                user.Currencies.Remove(currency);
                _context.SaveChanges();
            }

        }

    }
}
