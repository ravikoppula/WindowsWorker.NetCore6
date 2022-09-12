using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService1.Models; 
using WorkerService1.Models.ViewModels;

namespace WorkerService1
{
    public class EmplyeeService : IEmplyeeService
    {
        #region Contructor 

        private readonly EmployeeContext _context;

        public EmplyeeService(EmployeeContext context )
        {
            _context = context;

        }

        #endregion

        public List<EmployeeViewModel> GetAllEmployeeDetails()
        {
            var Results = (from e in _context.tblEmployees
                           join s in _context.tblSkills on e.SkillID equals s.SkillID
                           select new EmployeeViewModel
                           {
                               EmployeeID = e.EmployeeID,
                               EmployeeName = e.EmployeeName,
                               PhoneNumber = e.PhoneNumber,
                               YearsExperience = e.YearsExperience,
                               Skill = s.Title
                           }).ToList();

            return Results;

        }
    }
}
