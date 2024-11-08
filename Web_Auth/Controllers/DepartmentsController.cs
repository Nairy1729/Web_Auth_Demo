using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAuth.Models;
using WebAuth.Repositories;
using WebDBFirst.Repositories;

namespace WebDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

        // GET: api/Departments
        // Allow access to both User and Admin roles for viewing departments
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAll()
        {
            List<DepartmentDto> departments = _service.GetAllDepartments();
            return Ok(departments);
        }

        // GET: api/Departments/{id}
        // Allow access to both User and Admin roles for viewing a specific department
        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetDepartmentById(int id)
        {
            DepartmentDto department = _service.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }
            return Ok(department);
        }

        // POST: api/Departments
        // Restrict access to Admin role only for creating a department
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post(DepartmentDto departmentDto)
        {
            int result = _service.AddNewDepartment(departmentDto);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetDepartmentById), new { id = result }, departmentDto);
            }
            return BadRequest("Error creating department.");
        }

        // PUT: api/Departments
        // Restrict access to Admin role only for updating a department
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(DepartmentDto departmentDto)
        {
            string result = _service.UpdateDepartment(departmentDto);
            if (result.Contains("success"))
            {
                return Ok(result);
            }
            return BadRequest("Error updating department.");
        }

        // DELETE: api/Departments/{id}
        // Restrict access to Admin role only for deleting a department
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            string result = _service.DeleteDepartment(id);
            if (result.Contains("removed"))
            {
                return Ok(result);
            }
            return NotFound("Error deleting department or department not found.");
        }
    }
}
