using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_1.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeePosition { get; set; }

        //An employee belongs to one department
        //Department can have many employees
        [ForeignKey("Departments")]
        public int DepartmentID { get; set; }
        public virtual Departments Departments { get; set; }
    }
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeePosition { get; set; }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

    }
}