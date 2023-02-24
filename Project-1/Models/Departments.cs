using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_1.Models
{
    public class Departments
    {
        [Key]
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentRole { get; set; }

        // An department can be taken care of by many Mangers

        public ICollection<Manager> Managers { get; set; }
    }
    public class DepartmentsDto
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentRole { get; set; }

    }
}