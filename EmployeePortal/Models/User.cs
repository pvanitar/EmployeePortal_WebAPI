using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Mobile { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public bool Status { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
