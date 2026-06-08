using AutoMapper;
using JWTToeknDemo.DTOs;
using JWTToeknDemo.Models;
using JWTToeknDemo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWTToeknDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        public readonly IMapper _mapper;
        private IUserRepository _userRepository;
        private readonly  JwtService _jwtService;

        public LoginController(IMapper mapper, IUserRepository userRepository, JwtService jwtService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtService = jwtService;

        }
        
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register(UserRequestDtos loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Username and password are required.");

            }

            var user = new User();
            user.UserName = loginModel.Username;
            user.Password = new PasswordHasher<User>().HashPassword(user, loginModel.Password);
            var result = await _userRepository.CreateUser(user);
            if (result == null)
            {
                return BadRequest("something wrong");
            }
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserRequestDtos loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Username and password are required.");
            }
            var user = await _userRepository.GetPassword(loginModel);



            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "User not found",
                    success = false
                });
            }
          var hasher = new PasswordHasher<User>();
          var result = hasher.VerifyHashedPassword(user, user.Password, loginModel.Password);
           if(result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new
                {
                    message = "Invalid password",
                    success = false
                });
            }

            // Generate JWT token
            string token = _jwtService.GenerateToken(user.UserName);
            return Ok(new
            {
                success = true,
                username = user.UserName,
                token = token
            });
        }
        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userRepository.GetAllUsers();
            if (result == null)
            {
                return BadRequest("something wrong");
            }
            return Ok(result);
        }
    }
}
