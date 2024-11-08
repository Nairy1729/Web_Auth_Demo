using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_Auth.Data.Contexts;
using WebAuth.Models;
using WebAuth.Repositories;
using WebAuth.Mappings;

namespace WebDBFirst.Repositories
{
    public class EmployeeService : IEmployeeService
    {
        private readonly SecondDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(SecondDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            var employees = _context.TblEmployees
                .Include(e => e.Department) // Include Department to access DepartmentName
                .ToList();

            // Map list of TblEmployee entities to list of EmployeeDto
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            if (id != 0)
            {
                var employee = _context.TblEmployees
                    .Include(e => e.Department)
                    .FirstOrDefault(e => e.EmployeeId == id);

                // Map TblEmployee entity to EmployeeDto
                return employee != null ? _mapper.Map<EmployeeDto>(employee) : null;
            }
            return null;
        }

        public int AddNewEmployee(EmployeeDto employeeDto)
        {
            try
            {
                if (employeeDto != null)
                {
                    // Map EmployeeDto to TblEmployee for adding to the database
                    var employee = _mapper.Map<TblEmployee>(employeeDto);
                    _context.TblEmployees.Add(employee);
                    _context.SaveChanges();
                    return employee.EmployeeId;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string DeleteEmployee(int id)
        {
            if (id != 0)
            {
                var employee = _context.TblEmployees.FirstOrDefault(e => e.EmployeeId == id);
                if (employee != null)
                {
                    _context.TblEmployees.Remove(employee);
                    _context.SaveChanges();
                    return $"The employee with ID {id} was removed successfully.";
                }
                return "Employee not found.";
            }
            return "ID should not be zero.";
        }

        public string UpdateEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<TblEmployee>(employeeDto);
            var existingEmployee = _context.TblEmployees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

            if (existingEmployee != null)
            {
                // Update properties of the existing employee
                existingEmployee.Name = employee.Name;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Designation = employee.Designation;
                existingEmployee.Email = employee.Email;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.DepartmentId = employee.DepartmentId;

                _context.Entry(existingEmployee).State = EntityState.Modified;
                _context.SaveChanges();
                return "Employee updated successfully.";
            }
            return "Employee not found.";
        }
    }
}
