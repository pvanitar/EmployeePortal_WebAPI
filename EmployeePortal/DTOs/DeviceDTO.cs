namespace EmployeePortal.DTOs
{
    public class DeviceDTO
    {
        public string DeviceName { get; set; }
        public string IPAddress { get; set; }
        public string DeviceGroup { get; set; }
        public string ConnectionType { get; set; }
        public int Port { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}
