using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using EmployeePortal.DTOs;
using EmployeePortal.Models;
using EmployeePortal.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

       public UserController(UserRepository userRepository,IMapper mapper)
       {
            _userRepository= userRepository;
            _mapper= mapper;
       }
        
        // Employee signup (POST request)
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.SaveUserAsync(userDTO);
                return Ok(new { Message = "User created successfully", User  = user });
            }
            return BadRequest(new { Message = "Invalid data" });
        }

        // Employee login (POST request)
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Fetch user from the database
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(loginRequest.EmailId, loginRequest.Password);

            // If user is found
            if (user != null)
            {
                // Create claims for the JWT token
                var claims = new[]
                {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.EmailId),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                // Retrieve the key from environment variable
                var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

                // Check if the secret key is available
                if (string.IsNullOrEmpty(secretKey))
                {
                    return Unauthorized(new { Message = "JWT_SECRET_KEY is not set in the environment variables" });
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Create the token
                var token = new JwtSecurityToken(
                    issuer: "Angular",  // Replace with your own issuer
                    audience: "Angular",  // Replace with your audience
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),  // Set token expiry time (1 hour in this case)
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    Message = "Login successful",
                    Token = tokenString  // Return the token
                });
            }

            return Unauthorized(new { Message = "Invalid credentials" });
        }

        // Get list of Users
        [Authorize]
        [HttpGet("users")]
        public async Task<ActionResult> UserList()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        // Get list of Users with paggination
        [Authorize]
        [HttpGet("usersWithPagi")]
        public async Task<ActionResult> UserList(int pageNumber = 1, int pageSize = 5)
        {
            var (users, totalCount) = await _userRepository.GetAllUsersAsync(pageNumber, pageSize);

            foreach (var user in users)
            {
                user.Password = "***";
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); 

            return Ok(new
            {
                users,        
                totalCount,   
                totalPages,   
            });
        }

        [HttpPut("{id}/status")]
        [Authorize]
        public async Task<ActionResult> UpdateUserStatus(int id, [FromBody] UpdateStatusDTO statusDTO)
        {
            if (statusDTO == null || (statusDTO.Status != true && statusDTO.Status != false))
            {
                return BadRequest(new { Message = "Invalid status value" });
            }

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            user.Status = statusDTO.Status;

            var updatedUser = await _userRepository.UpdateUserAsync(user);

            if (updatedUser == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Failed to update user status" });
            }

            return Ok(new { Message = "User status updated successfully", User = updatedUser });
        }
    }
}
