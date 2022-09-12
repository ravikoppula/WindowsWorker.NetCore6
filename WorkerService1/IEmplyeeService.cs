using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService1.Models.ViewModels;

namespace WorkerService1
{
    public  interface IEmplyeeService
    {
        #region Get Employee Details

        List<EmployeeViewModel> GetAllEmployeeDetails();

        #endregion 
    }
}
