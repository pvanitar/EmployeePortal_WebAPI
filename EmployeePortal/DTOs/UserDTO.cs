namespace EmployeePortal.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
    }

    public class UpdateStatusDTO
    {
        public bool Status { get; set; }
    }
}
