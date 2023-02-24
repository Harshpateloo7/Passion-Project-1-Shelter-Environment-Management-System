using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_1.Models
{
    public class Manager
    {
        [Key]
        public int ManagerID { get; set; }

        public string ManagerName { get; set; }

        public string ManagerBranch { get; set; }

        public string ManagerPosition { get; set; }
        
        // A Manager can take care of many department
        public ICollection<Departments> Departments { get; set; }
    }
    public class ManagerDto
    {
        public int ManagerID { get; set; }

        public string ManagerName { get; set; }

        public string ManagerBranch { get; set; }

        public string ManagerPosition { get; set; }

    }
}