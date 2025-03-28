using AutoMapper;
using EmployeePortal.DTOs;
using EmployeePortal.Models;
using EmployeePortal.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceController(DeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        // Get all devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            var devices = await _deviceRepository.GetAllDevicesAsync();
            return Ok(devices);
        }

        // Get device by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _deviceRepository.GetDeviceByIdAsync(id);
            if (device == null)
            {
                return NotFound(new { Message = "Device not found" });
            }
            return Ok(device);
        }

        // Create a new device
        [HttpPost]
        public async Task<ActionResult<Device>> AddDevice([FromBody] DeviceDTO deviceDTO)
        {
            if (ModelState.IsValid)
            {
                var device = _mapper.Map<Device>(deviceDTO);
                var createdDevice = await _deviceRepository.SaveDeviceAsync(device);
                return CreatedAtAction(nameof(GetDevice), new { id = createdDevice.Id }, createdDevice);
            }
            return BadRequest(new { Message = "Invalid data" });
        }

        // Update an existing device
        [HttpPut("{id}")]
        public async Task<ActionResult<Device>> UpdateDevice(int id, [FromBody] DeviceDTO deviceDTO)
        {
            if (ModelState.IsValid)
            {
                var existingDevice = await _deviceRepository.GetDeviceByIdAsync(id);
                if (existingDevice == null)
                {
                    return NotFound(new { Message = "Device not found" });
                }
                var updatedDevice = _mapper.Map(deviceDTO, existingDevice);
                await _deviceRepository.UpdateDeviceAsync(updatedDevice);
                return Ok(updatedDevice);
            }
            return BadRequest(new { Message = "Invalid data" });
        }

        // Delete a device
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(int id)
        {
            var isDeleted = await _deviceRepository.DeleteDeviceAsync(id);
            if (isDeleted)
            {
                return Ok(new { Message = "Device deleted successfully" });
            }
            return NotFound(new { Message = "Device not found" });
        }
    }
}
