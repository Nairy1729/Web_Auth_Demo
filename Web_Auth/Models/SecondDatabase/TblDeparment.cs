using System;
using System.Collections.Generic;
using System.Globalization;

namespace WebAuth.Models
{
    public partial class TblDeparment
    {
        public TblDeparment()
        {
            TblEmployees = new HashSet<TblEmployee>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string DepartmentHead { get; set; } = null!;

        public virtual ICollection<TblEmployee> TblEmployees { get; set; }
    }
    public record DepartmentDto
    {
        public DepartmentDto() { }

        public DepartmentDto(int Id, string Name, string DepartmentHead, List<EmployeeDto> Employees)
        {
            this.Id = Id;
            this.Name = Name;
            this.DepartmentHead = DepartmentHead;
            this.Employees = Employees;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public string DepartmentHead { get; init; }
        public List<EmployeeDto> Employees { get; init; } = new List<EmployeeDto>();
    }

}