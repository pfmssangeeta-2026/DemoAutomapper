using AutoMapper;
using JWTToeknDemo.Data;
using JWTToeknDemo.DTOs;
using JWTToeknDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JWTToeknDemo.Repository
{
    public class UserRepository : IUserRepository
    {
       
        private readonly MyDBContext _context;

        public UserRepository( MyDBContext context)
        {
           
            _context = context;
        }
        public  async Task<string> CreateUser(User userModel)
        {
          var res=await _context.Users.AddAsync(userModel);
           await _context.SaveChangesAsync();
            return "success";
            
        }

        public Task<List<User>> GetAllUsers()
        {
            return _context.Users.ToListAsync();
        }

        public async Task<User> GetPassword(UserRequestDtos userRequestDtos)
        {

            var result= await _context.Users.Where(x=>x.UserName.Equals(userRequestDtos.Username)).FirstOrDefaultAsync();
                                              

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
