using AutoMapper;
using EmployeePortal.Data;
using EmployeePortal.DTOs;
using EmployeePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Repository
{
    public class UserRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext context,IMapper mapper) {
            _dbContext = context;
            _mapper = mapper;

        }

        
        // Save User (sign up)
        public async Task<User> SaveUserAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        
        // Login User (check credentials)
        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _dbContext.Users
                .Where(e => e.EmailId == email && e.Password == password)
                .FirstOrDefaultAsync();
        }

        
        // Get all employees
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
