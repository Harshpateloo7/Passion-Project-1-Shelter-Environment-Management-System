using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_1.Models.ViewModels
{
    public class DetailsDepartments
    {
        public DepartmentsDto SelectedDepartments { get; set; }
        public IEnumerable<EmployeeDto> RelatedEmployees { get; set; }
    }
}