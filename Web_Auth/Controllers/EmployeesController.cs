using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAuth.Models;
using WebAuth.Repositories;
using WebDBFirst.Repositories;

namespace WebDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/Employees
        // Allow access to both User and Admin roles for viewing employees
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAll()
        {
            List<EmployeeDto> employees = _service.GetAllEmployees();
            return Ok(employees);
        }

        // GET: api/Employees/{id}
        // Allow access to both User and Admin roles for viewing a specific employee
        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetEmployeeById(int id)
        {
            EmployeeDto employee = _service.GetEmployeeById(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound($"Employee with ID {id} not found.");
        }

        // POST: api/Employees
        // Restrict access to Admin role only for creating a new employee
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post(EmployeeDto employeeDto)
        {
            int result = _service.AddNewEmployee(employeeDto);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetEmployeeById), new { id = result }, employeeDto);
            }
            return BadRequest("Failed to create employee.");
        }

        // PUT: api/Employees
        // Restrict access to Admin role only for updating employee details
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(EmployeeDto employeeDto)
        {
            string result = _service.UpdateEmployee(employeeDto);
            if (result.Contains("successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // DELETE: api/Employees/{id}
        // Restrict access to Admin role only for deleting an employee
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            string result = _service.DeleteEmployee(id);
            if (result.Contains("removed"))
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
