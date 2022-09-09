using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService1.Models.ViewModels
{
    public class EmployeeViewModel
    {
        [Key]
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string PhoneNumber { get; set; }

        public string Skill { get; set; }

        public int YearsExperience { get; set; }

    }
}
