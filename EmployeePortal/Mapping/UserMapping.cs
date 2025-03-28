using AutoMapper;
using EmployeePortal.DTOs;
using EmployeePortal.Models;

namespace EmployeePortal.Mapping
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            // Mapping from Employee to EmployeeDTO
            CreateMap<User, UserDTO>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            // Mapping from EmployeeDTO to Employee (for creating a new employee)
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            // Mapping from UpdateStatusDTO to User
            CreateMap<UpdateStatusDTO, User>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            // Mapping from Device to DeviceDTO
            CreateMap<Device, DeviceDTO>()
                 .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.DeviceName));

            // Mapping from DeviceDTO to Device
            CreateMap<DeviceDTO, Device>()
                .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.DeviceName));

        }
    }
}
