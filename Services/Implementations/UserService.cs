using ApiMoneda.Service.Interface;
using ApiMoneda.Data;
using ApiMoneda.Entities;
using Microsoft.EntityFrameworkCore;
using ApiMoneda.Models.Dto;
using ApiMoneda.Models.Enum;
using System.ComponentModel.DataAnnotations;
using static ApiMoneda.Models.Dto.AuthenticationRequestBodyDTO;
using ApiMoneda.Models.Dto.UserDTO;

namespace ApiMoneda.Service.Implementations
{
    public class UserService : IUserService 
    {
        private readonly ConversorContext _context;
        public UserService(ConversorContext context)
        {
            _context = context;
        }

        public User? ValidateUser(AuthenticationRequestBodyDTO authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.UserName == authRequestBody.UserName && p.Password == authRequestBody.Password);
        }

        public User? GetById(int UserId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == UserId);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Create(CreateUserDTO dto)
        {
            User User = new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                UserName = dto.UserName,
                Role = Role.User,
            };
            _context.Users.Add(User);
            _context.SaveChanges();
        }

        public void Update(UpdateUserDto dto, int userId)
        {
            User userToUpdate = _context.Users.Single(u => u.Id == userId); 
            userToUpdate.FirstName = dto.FirstName;
            userToUpdate.LastName = dto.LastName;
            userToUpdate.UserName = dto.UserName;
            userToUpdate.Email = dto.Email;
            userToUpdate.Password = dto.Password;
            userToUpdate.SubscriptionId = dto.SubscriptionId;
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == userId));
            _context.SaveChanges();
        }


        public bool CheckIfUserExists(int UserId)
        {
            User? user = _context.Users.SingleOrDefault(user => user.Id == UserId); 
            if(user == null)
            {
             return false;
            }
            return true;
        }


        public void UpdateUserSubscription( int userId, int subscriptionId)
        {
            User userToUpdate = _context.Users.SingleOrDefault(u => u.Id == userId);
            if (userToUpdate != null)
            {
                userToUpdate.SubscriptionId = subscriptionId;
                _context.SaveChanges();
            }

        }
    }
}
