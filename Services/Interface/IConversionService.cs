using ApiMoneda.Entities;
using ApiMoneda.Models.Dto.ConversionDTO;

namespace ApiMoneda.Services.Interface
{
    public interface IConversionService
    {
        List<Conversion> GetAll(int userId);
        void CreateConversion(CreateConversionDTO dto);
        int ConversionCounter(int userId);
    }
}
