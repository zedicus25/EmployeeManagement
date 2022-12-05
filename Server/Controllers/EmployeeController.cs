using Server.Models;
using Server.ServerModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class EmployeeController : BaseController
    {
        public EmployeeController(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<UserEmployee> GetAllShortEmployeeData()
        {
            List<UserEmployee> employees = new List<UserEmployee>();
            if (_dbContext == null)
                return employees;

            List<Employee> users = _dbContext.Employees.ToList();
            List<Person> persons = _dbContext.Persons.ToList();
            List<FIO> fIOs = _dbContext.FIOs.ToList();

            foreach (var item in users)
            {
                FIO fio = fIOs.FirstOrDefault(x => x.Id == persons.FirstOrDefault(y => y.Id == item.Id)?.FIO_Id);
                employees.Add(new UserEmployee() { Id = item.Id, Name = fio.First_Name, LastName = fio.Last_Name });
            }
            return employees;

        }
    }
}
