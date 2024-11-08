using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_Auth.Data.Contexts;
using WebAuth.Models;
using WebAuth.Repositories;

namespace WebDBFirst.Repositories
{
    public class DepartmentService : IDepartmentService
    {
        private readonly SecondDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(SecondDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public List<DepartmentDto> GetAllDepartments()
        {
            var departments = _context.TblDeparments
                .Include(d => d.TblEmployees) // Include employees to avoid lazy loading
                .ToList();

            // Use AutoMapper to map the list of TblDeparment entities to List<DepartmentDto>
            return _mapper.Map<List<DepartmentDto>>(departments);
        }


        public int AddNewDepartment(DepartmentDto departmentDto)
        {
            try
            {
                if (departmentDto != null)
                {
                    // Map DepartmentDto to TblDeparment for entity operations
                    var department = _mapper.Map<TblDeparment>(departmentDto);
                    _context.TblDeparments.Add(department);
                    _context.SaveChanges();
                    return department.Id;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string DeleteDepartment(int id)
        {
            if (id != 0)
            {
                var department = _context.TblDeparments.FirstOrDefault(x => x.Id == id);
                if (department != null)
                {
                    _context.TblDeparments.Remove(department);
                    _context.SaveChanges();
                    return $"The department with ID {id} was removed.";
                }
                return "Department not found.";
            }
            return "ID should not be zero.";
        }

        public DepartmentDto GetDepartmentById(int id)
        {
            if (id != 0)
            {
                var department = _context.TblDeparments
                    .Include(d => d.TblEmployees) // Include employees for related data
                    .FirstOrDefault(x => x.Id == id);

                // Map TblDeparment entity to DepartmentDto
                return department != null ? _mapper.Map<DepartmentDto>(department) : null;
            }
            return null;
        }

        public string UpdateDepartment(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<TblDeparment>(departmentDto);
            var existingDepartment = _context.TblDeparments.FirstOrDefault(x => x.Id == department.Id);

            if (existingDepartment != null)
            {
                // Update properties of the existing department
                existingDepartment.Name = department.Name;
                existingDepartment.DepartmentHead = department.DepartmentHead;

                _context.Entry(existingDepartment).State = EntityState.Modified;
                _context.SaveChanges();
                return "Department updated successfully.";
            }
            return "Department not found.";
        }
    }
}