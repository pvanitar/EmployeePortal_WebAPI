using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DeviceName { get; set; }
        [Required]
        public string IPAddress { get; set; }
        [Required]
        public string DeviceGroup { get; set; }
        [Required]
        public string ConnectionType { get; set; }
        public int Port { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}
