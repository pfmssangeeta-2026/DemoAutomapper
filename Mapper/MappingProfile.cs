using AutoMapper;
using JWTToeknDemo.DTOs;
using JWTToeknDemo.Models;
using System.Runtime;

namespace JWTToeknDemo.Mapper
{
    public class MappingProfile:Profile

    {
        public MappingProfile()
        {
            CreateMap<UserRequestDtos, User>();

            // Optional reverse mapping
            CreateMap<User, UserRequestDtos>();
        }
    }
}
