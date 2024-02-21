using ApiMoneda.Entities;
using ApiMoneda.Models.Dto;

namespace ApiMoneda.Service.Interface
{
    public interface IUserService
    {
        void Create(CreateUserDTO dto);
        void Delete(int id);
        List<User> GetAll(); 
        User? GetById(int id); 
        void Update(UpdateUserDto dto, int userId);
        bool CheckIfUserExists(int userId);
        User? ValidateUser(AuthenticationRequestBodyDTO authRequestBody);
        void UpdateUserSubscription(int userId, int subscriptionId);


    }
}
