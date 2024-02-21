using ApiMoneda.Entities;
using ApiMoneda.Models.Dto;
namespace ApiMoneda.Service.Interface
{
    public interface ICurrencyService
    {
        List<Currency> GetAll();
        Currency? GetById(int id);
        Currency? GetByName(string name);
        void Create(CreateAndUpdateCurrencyDTO dto);
        void Update(CreateAndUpdateCurrencyDTO dto, int currencyId);
        void DeleteCurrency(int currencyId);
        bool CheckIfCurrencyExists(int currencyId);
    }
}
