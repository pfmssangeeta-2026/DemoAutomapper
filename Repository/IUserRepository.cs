using JWTToeknDemo.DTOs;
using JWTToeknDemo.Models;

namespace JWTToeknDemo.Repository
{
    public interface IUserRepository
    {
        Task<string> CreateUser(User userRequestDtos );
        Task <User> GetPassword(UserRequestDtos userRequestDtos);
        Task<List<User>> GetAllUsers();
    }
}
