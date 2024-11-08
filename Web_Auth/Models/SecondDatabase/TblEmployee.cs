using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebAuth.Models;

namespace WebAuth.Models
{
    public partial class TblEmployee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Designation { get; set; }
        public string Email { get; set; } = null!;
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }


        public virtual TblDeparment Department { get; set; } = null!;
    }
    public record EmployeeDto(int EmployeeId, string Name, string Designation, string Email, decimal Salary);
}