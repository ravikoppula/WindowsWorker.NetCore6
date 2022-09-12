using Microsoft.Extensions.Logging;
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

        private readonly ILogger<WindowsBackgroundService> _logger;
        private readonly EmployeeContext _db;

        public EmplyeeService(ILogger<WindowsBackgroundService> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            _db = factory.CreateScope().ServiceProvider.GetRequiredService<EmployeeContext>();

        }

        #endregion

        public List<EmployeeViewModel> GetAllEmployeeDetails()
        {
            var Results = (from e in _db.tblEmployees
                           join s in _db.tblSkills on e.SkillID equals s.SkillID
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

        public tblEmployee AddEmployees()
        {
            tblEmployee item = new tblEmployee();
            {
                item.EmployeeName = "test";
                item.PhoneNumber = "222222222222";
                item.SkillID = 7;
                item.YearsExperience = 9;

                _db.tblEmployees.Add(item);

                _db.SaveChangesAsync();
            }

            return item;
        }
         
    }
}
