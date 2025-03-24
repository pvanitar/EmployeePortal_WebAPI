using EmployeePortal.Models;
using EmployeePortal.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeeController(EmployeeRepository employeeRepository) { 
            this.employeeRepository = employeeRepository;   
        }

        [HttpGet]
        public async Task<ActionResult> EmployeeList()
        {
            var allEmployees=await employeeRepository.GetAllEmployees();
            return Ok(allEmployees);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(Employee emp)
        {
            await employeeRepository.SaveEmployee(emp); 
            return Ok(emp);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, Employee emp)
        {
            try
            {
                await employeeRepository.UpdateEmployee(emp, id);
                return Ok(new { Message = "Employee updated successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = "Employee not found", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                await employeeRepository.DeleteEmployee(id);
                return Ok(new { Message = "Employee deleted successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = "Employee not found", Error = ex.Message });
            }
        }
    } 
}
