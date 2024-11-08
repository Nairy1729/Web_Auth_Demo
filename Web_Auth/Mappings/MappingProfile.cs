using AutoMapper;
using WebAuth.Models;

namespace WebAuth.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map TblEmployee to EmployeeDto with explicit properties mapping
            CreateMap<TblEmployee, EmployeeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Department.Name)) // Only if DepartmentName is part of EmployeeDto
                .ReverseMap();

            // Other mappings
            CreateMap<TblDeparment, DepartmentDto>()
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.TblEmployees))
                .ReverseMap();
        }
    }
}