using Server.Models;
using Server.ServerModels;
using System;
using System.Collections.Generic;
using System.Data;
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
                employees.Add(new UserEmployeeShort() { Id = item.Id, FirstName = fio.First_Name, LastName = fio.Last_Name });
            }
            return employees;
        }

        public IEnumerable<UserEmployeeLong> GetAllLongEmployeeData()
        {
            List<UserEmployeeLong> employees = new List<UserEmployeeLong>();
            if (_dbContext == null)
                return employees;

            List<Employee> users = _dbContext.Employees.ToList();
            List<Person> persons = _dbContext.Persons.ToList();
            List<FIO> fIOs = _dbContext.FIOs.ToList();
            List<LoginData> loginDatas = _dbContext.LoginDatas.ToList();
            List<Adress> adresses = _dbContext.Adresses.ToList();
            foreach (var item in users)
            {
                Person person = persons.FirstOrDefault(x => x.Id == item.PersonId);
                FIO fio = fIOs.FirstOrDefault(x => x.Id == person.FIO_Id);
                LoginData login = loginDatas.FirstOrDefault(x => x.Id == item.LoginDataId);
                Adress adress = adresses.FirstOrDefault(x => x.Id == person.Adress_Id);
                List<Email> emails = _dbContext.Emails.Where(x => x.PersonId == person.Id).ToList();
                List<string> eml = new List<string>();
                foreach (var email in emails)
                {
                    eml.Add(email.Email1);
                }
                List<Phone_Numbers> numbers = _dbContext.Phone_Numbers.Where(x => x.PersonId == person.Id).ToList();
                List<string> num = new List<string>();
                foreach (var number in numbers)
                {
                    num.Add(number.Phone_Number);
                }
                employees.Add(new UserEmployeeLong()
                {
                    Id = item.Id,
                    Login = login.Login,
                    Password = login.Password,
                    FirstName = fio.First_Name,
                    LastName = fio.Last_Name,
                    Patronymic = fio.Patronymic,
                    Country = adress.Country,
                    City = adress.City,
                    Street = adress.Street,
                    House_Number = adress.House_Number,
                    Full_Adress = adress.Full_Adress,
                    Birthday = person.Birthday,
                    Salary = (float)item.Salary,
                    EmployeeRoleId = (int)item.RoleId,
                    ProjectId = (int)item.ProjectId,
                    Emails = new List<string>(eml),
                    PhoneNumbers = new List<string>(num)

                });
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
                    Title = descriptions.FirstOrDefault(x => x.Id == item.DescriptionId).Title,
                    Description = descriptions.FirstOrDefault(x => x.Id == item.DescriptionId).Description,
                    UserRoleId = item.UserRoleId
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

        public void DeleteEmployee(int employeId)
        {
            Employee emp = _dbContext.Employees.FirstOrDefault(x => x.Id == employeId);
            if (emp == null)
                return;
            Person person = _dbContext.Persons.FirstOrDefault(x => x.Id == emp.PersonId);
            FIO fio = _dbContext.FIOs.FirstOrDefault(x => x.Id == person.FIO_Id);
            Adress adress = _dbContext.Adresses.FirstOrDefault(x => x.Id == person.Adress_Id);
            LoginData data = _dbContext.LoginDatas.FirstOrDefault(x => x.Id == emp.LoginDataId);
            List<Email> emails = _dbContext.Emails.Where(x => x.PersonId == person.Id).ToList();
            List<Phone_Numbers> numbers = _dbContext.Phone_Numbers.Where(x => x.PersonId == person.Id).ToList();  
            _dbContext.Employees.Remove(emp);
            _dbContext.LoginDatas.Remove(data);
            _dbContext.Persons.Remove(person);
            _dbContext.Emails.RemoveRange(emails);
            _dbContext.Phone_Numbers.RemoveRange(numbers);
            _dbContext.FIOs.Remove(fio);
            _dbContext.Adresses.Remove(adress);
            _dbContext.SaveChanges();
        }

        public void UpdateEmployee(int id, UserEmployeeLong newEmp)
        {
            Employee emp = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
                return;
            Person person = _dbContext.Persons.FirstOrDefault(x => x.Id == emp.PersonId);
            FIO fio = _dbContext.FIOs.FirstOrDefault(x => x.Id == person.FIO_Id);
            Adress adress = _dbContext.Adresses.FirstOrDefault(x => x.Id == person.Adress_Id);
            LoginData data = _dbContext.LoginDatas.FirstOrDefault(x => x.Id == emp.LoginDataId);
            List<Email> emails = _dbContext.Emails.Where(x => x.PersonId == person.Id).ToList();
            List<Phone_Numbers> numbers = _dbContext.Phone_Numbers.Where(x => x.PersonId == person.Id).ToList();
            if (fio.First_Name == newEmp.FirstName && fio.Last_Name == newEmp.LastName && fio.Patronymic == newEmp.Patronymic
                && person.Birthday == newEmp.Birthday && adress.Country == newEmp.Country && adress.City == newEmp.City
                && adress.Street == newEmp.Street && adress.House_Number == newEmp.House_Number && adress.Full_Adress == newEmp.Full_Adress
                && data.Login == newEmp.Login && data.Password == newEmp.Password)
                return;
            _dbContext.Emails.RemoveRange(emails);
            _dbContext.Phone_Numbers.RemoveRange(numbers);

            List<Email> newEmails = new List<Email>();
            foreach (var item in newEmp.Emails)
            {
                emails.Add(new Email() { Email1 = item, Person = person });
            }

            List<Phone_Numbers> newNumbers = new List<Phone_Numbers>();
            foreach (var item in newEmp.PhoneNumbers)
            {
                numbers.Add(new Phone_Numbers() { Phone_Number = item, Person = person });
            }
            _dbContext.Phone_Numbers.AddRange(newNumbers);
            _dbContext.Emails.AddRange(newEmails);

            person.Birthday = newEmp.Birthday;

            fio.First_Name = newEmp.FirstName;
            fio.Last_Name = newEmp.LastName;
            fio.Patronymic = newEmp.Patronymic;

            adress.Country = newEmp.Country;
            adress.City = newEmp.City;
            adress.Street = newEmp.Street;
            adress.House_Number = newEmp.House_Number;
            adress.Full_Adress = newEmp.Full_Adress;

            data.Login = newEmp.Login;
            if(newEmp.Password != null)
                data.Password = newEmp.Password;

            emp.Salary = (decimal)newEmp.Salary;
            emp.RoleId = newEmp.EmployeeRoleId;
            emp.ProjectId = newEmp.ProjectId;

            _dbContext.SaveChanges();
        }

        public IEnumerable<UserRole> GetUsersRoles()
        {
            List<UserRole> roles = new List<UserRole>();
            foreach (var item in _dbContext.UsersRoles)
            {
                roles.Add(new UserRole { Id = item.Id, Name = item.Name });
            }
            return roles;
        }

        public void AddEmployeeRole(UserEmployeeRole role)
        {
            EmployeesRole newRole = new EmployeesRole();
            newRole.UserRoleId = role.UserRoleId;
            EmployeeRoleDescription description = new EmployeeRoleDescription();
            description.Description = role.Description;
            description.Title = role.Title;
            newRole.EmployeeRoleDescription = description;
            _dbContext.EmployeesRoles.Add(newRole);
            _dbContext.SaveChanges();
        }

        public void SetNewUserRoleForEmployeeRole(int userRoleId, int employeeRoleId)
        {
            EmployeesRole role = _dbContext.EmployeesRoles.FirstOrDefault(x => x.Id == employeeRoleId);
            if (role == null)
                return;
            role.UserRoleId = userRoleId;
            _dbContext.SaveChanges();
        }

        public void RemoveEmployeeRole(int empRoleId)
        {
            EmployeesRole role = _dbContext.EmployeesRoles.FirstOrDefault(x => x.Id == empRoleId);
            if (role == null)
                return;
            EmployeesRole newRole = _dbContext.EmployeesRoles.Where(x=> x.Id != empRoleId).First();
            List<Employee> employees = _dbContext.Employees.Where(x => x.RoleId == empRoleId).ToList();
            for (int i = 0; i < employees.Count; i++)
            {
                employees[i].RoleId = newRole.Id;
            }
            EmployeeRoleDescription desck = _dbContext.EmployeeRoleDescriptions.FirstOrDefault(x => x.Id == role.DescriptionId);
            _dbContext.EmployeesRoles.Remove(role);
            _dbContext.EmployeeRoleDescriptions.Remove(desck);
            _dbContext.SaveChanges();
        }

        public void UpdateEmployeeRole(int oldId, UserEmployeeRole newRole)
        {
            EmployeesRole oldRole = _dbContext.EmployeesRoles.FirstOrDefault(x => x.Id == oldId);
            if (oldRole == null)
                return;
            EmployeeRoleDescription desck = _dbContext.EmployeeRoleDescriptions.FirstOrDefault(x => x.Id == oldRole.DescriptionId);
            if (desck.Description == newRole.Description && desck.Title == newRole.Title && oldRole.UserRoleId == newRole.UserRoleId)
                return;
            desck.Title = newRole.Title;
            desck.Description = newRole.Description;
            oldRole.UserRoleId = newRole.UserRoleId;
            _dbContext.SaveChanges();
        }
    }
}
