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

        public IEnumerable<UserEmployeeShort> GetAllShortEmployeeData()
        {
            List<UserEmployeeShort> employees = new List<UserEmployeeShort>();
            if (_dbContext == null)
                return employees;

            List<Employee> users = _dbContext.Employees.ToList();
            List<Person> persons = _dbContext.Persons.ToList();
            List<FIO> fIOs = _dbContext.FIOs.ToList();

            foreach (var item in users)
            {
                FIO fio = fIOs.FirstOrDefault(x => x.Id == persons.FirstOrDefault(y => y.Id == item.Id)?.FIO_Id);
                employees.Add(new UserEmployeeShort() { Id = item.Id, Name = fio.First_Name, LastName = fio.Last_Name });
            }
            return employees;

        }

        public IEnumerable<UserEmployeeRole> GetAllEmployeeRoles()
        {
            List<UserEmployeeRole> roles = new List<UserEmployeeRole>();
            if (_dbContext == null)
                return roles; 

            List<EmployeesRole> employeesRoles = _dbContext.EmployeesRoles.ToList();
            List<EmployeeRoleDescription> descriptions = _dbContext.EmployeeRoleDescriptions.ToList();
            foreach (var item in employeesRoles)
            {
                roles.Add(new UserEmployeeRole()
                {
                    Id = item.Id,
                    Title = descriptions.FirstOrDefault(x => x.Id == item.DescriptionId).Title
                });
            }
            return roles;
        } 

        public void AddEmployee(UserEmployeeLong employee)
        {
            FIO fio = new FIO() 
            { 
                First_Name = employee.FirstName,
                Last_Name = employee.LastName, 
                Patronymic = employee.Patronymic
            };

            Adress adress = new Adress()
            {
                Country = employee.Country,
                City = employee.City,
                Street = employee.Street,
                House_Number = employee.House_Number,
                Full_Adress = employee.Full_Adress
            };

            LoginData loginData = new LoginData()
            {
                Login = employee.Login,
                Password = employee.Password
            };

            Person person = new Person()
            {
                Birthday = employee.Birthday,
                FIO = fio,
                Adress = adress
            };

            List<Email> emails = new List<Email>();
            foreach (var item in employee.Emails)
            {
                emails.Add(new Email() { Email1 = item, Person = person });
            }

            List<Phone_Numbers> numbers = new List<Phone_Numbers>();
            foreach (var item in employee.PhoneNumbers)
            {
                numbers.Add(new Phone_Numbers() { Phone_Number = item, Person = person});
            }

            Employee empl = new Employee()
            {
                Person = person,
                RoleId = employee.EmployeeRoleId,
                ProjectId = employee.ProjectId,
                LoginData = loginData,
                Salary = (decimal)employee.Salary
            };

            _dbContext.Employees.Add(empl);
            _dbContext.Emails.AddRange(emails);
            _dbContext.Phone_Numbers.AddRange(numbers);
            _dbContext.SaveChanges();

        }
    }
}
