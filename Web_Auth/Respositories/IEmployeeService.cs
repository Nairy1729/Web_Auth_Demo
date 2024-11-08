using WebAuth.Models;

namespace WebAuth.Repositories
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAllEmployees();
        EmployeeDto GetEmployeeById(int id);
        int AddNewEmployee(EmployeeDto employeeDto);
        string UpdateEmployee(EmployeeDto employeeDto);
        string DeleteEmployee(int id);
    }
}