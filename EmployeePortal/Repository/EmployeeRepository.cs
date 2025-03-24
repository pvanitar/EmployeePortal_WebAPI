using EmployeePortal.Data;
using EmployeePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Repository
{
    public class EmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext dbContext) {
            this._context = dbContext;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task SaveEmployee(Employee emp)
        {
            await _context.Employees.AddAsync(emp);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployee(Employee emp, int id)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);

            if (existingEmployee == null)
            {
                throw new Exception("Employee not found");
            }

            existingEmployee.Name = emp.Name;
            existingEmployee.Email = emp.Email;
            existingEmployee.Mobile = emp.Mobile;
            existingEmployee.Age = emp.Age;
            existingEmployee.Salary = emp.Salary;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                throw new Exception("Employee not found");
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

    }
}
