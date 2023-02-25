using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_1.Models.ViewModels
{
    public class UpdateEmployee
    {
        //This viewModel is a class which stores information that we need to present to/Employee/Update/{}

        // the existing Employee information
       public EmployeeDto SelectedEmployee { get; set; }
        
        //all Department to choose from when updating this Employee

       public IEnumerable<DepartmentsDto> DepartmentsOptions { get; set; } 
    }
}