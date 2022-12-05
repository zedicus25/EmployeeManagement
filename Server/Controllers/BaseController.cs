using Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class BaseController
    {
        protected EmployeeManagement _dbContext;

        public BaseController(DbContext dbContext)
        {
            _dbContext = dbContext as EmployeeManagement;
        }
    }
}
