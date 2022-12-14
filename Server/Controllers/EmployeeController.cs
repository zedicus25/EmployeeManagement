using Server.Models;
using Server.ServerModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class EmployeeController : BaseController
    {
        public IEnumerable<UserEmployeeShort> GetAllShortEmployeeData()
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                List<UserEmployeeShort> employees = new List<UserEmployeeShort>();

                List<Employee> users = dbContext.Employees.ToList();
                List<Person> persons = dbContext.Persons.ToList();
                List<FIO> fIOs = dbContext.FIOs.ToList();

                foreach (var item in users)
                {
                    FIO fio = fIOs.FirstOrDefault(x => x.Id == persons.FirstOrDefault(y => y.Id == item.Id)?.FIO_Id);
                    employees.Add(new UserEmployeeShort() { Id = item.Id, FirstName = fio.First_Name, LastName = fio.Last_Name });
                }
                return employees;
            }  
        }

        public IEnumerable<UserEmployeeLong> GetAllLongEmployeeData()
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                List<UserEmployeeLong> employees = new List<UserEmployeeLong>();

                List<Employee> users = dbContext.Employees.ToList();
                List<Person> persons = dbContext.Persons.ToList();
                List<FIO> fIOs = dbContext.FIOs.ToList();
                List<LoginData> loginDatas = dbContext.LoginDatas.ToList();
                List<Adress> adresses = dbContext.Adresses.ToList();
                foreach (var item in users)
                {
                    Person person = persons.FirstOrDefault(x => x.Id == item.PersonId);
                    FIO fio = fIOs.FirstOrDefault(x => x.Id == person.FIO_Id);
                    LoginData login = loginDatas.FirstOrDefault(x => x.Id == item.LoginDataId);
                    Adress adress = adresses.FirstOrDefault(x => x.Id == person.Adress_Id);
                    List<Email> emails = dbContext.Emails.Where(x => x.PersonId == person.Id).ToList();
                    List<string> eml = new List<string>();
                    foreach (var email in emails)
                    {
                        eml.Add(email.Email1);
                    }
                    List<Phone_Numbers> numbers = dbContext.Phone_Numbers.Where(x => x.PersonId == person.Id).ToList();
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
        }

        public IEnumerable<UserEmployeeRole> GetAllEmployeeRoles()
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                List<UserEmployeeRole> roles = new List<UserEmployeeRole>();


                List<EmployeesRole> employeesRoles = dbContext.EmployeesRoles.ToList();
                List<EmployeeRoleDescription> descriptions = dbContext.EmployeeRoleDescriptions.ToList();
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
        } 

        public void AddEmployee(UserEmployeeLong employee)
        {
            if (employee.FirstName == String.Empty || employee.LastName == String.Empty || employee.Patronymic == String.Empty)
                return;

            FIO fio = new FIO() 
            { 
                First_Name = employee.FirstName,
                Last_Name = employee.LastName, 
                Patronymic = employee.Patronymic
            };

            if (employee.Country == String.Empty || employee.City == String.Empty || employee.Street == String.Empty ||
                employee.House_Number == String.Empty || employee.Full_Adress == String.Empty)
                return;
            Adress adress = new Adress()
            {
                Country = employee.Country,
                City = employee.City,
                Street = employee.Street,
                House_Number = employee.House_Number,
                Full_Adress = employee.Full_Adress
            };

            if (employee.Login == String.Empty || employee.Password == String.Empty)
                return;
            LoginData loginData = new LoginData()
            {
                Login = employee.Login,
                Password = PasswordHasher.HashPassword(employee.Password)
            };

            if (employee.Birthday == null)
                return;
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
            if (employee.Salary == 0.0f || employee.EmployeeRoleId == 0 || employee.ProjectId == 0)
                return;
            Employee empl = new Employee()
            {
                Person = person,
                RoleId = employee.EmployeeRoleId,
                ProjectId = employee.ProjectId,
                LoginData = loginData,
                Salary = (decimal)employee.Salary
            };
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                dbContext.Employees.Add(empl);
                dbContext.Emails.AddRange(emails);
                dbContext.Phone_Numbers.AddRange(numbers);
                dbContext.SaveChanges();
            }
        }

        public void DeleteEmployee(int employeId)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                Employee emp = dbContext.Employees.FirstOrDefault(x => x.Id == employeId);
                if (emp == null)
                    return;
                List<ProjectTask> tasks = dbContext.ProjectTasks.Where(x => x.EmployeeId == employeId).ToList();
                for (int i = 0; i < tasks.Count; i++)
                {
                    tasks[i].EmployeeId = null;
                }
                Person person = dbContext.Persons.FirstOrDefault(x => x.Id == emp.PersonId);
                FIO fio = dbContext.FIOs.FirstOrDefault(x => x.Id == person.FIO_Id);
                Adress adress = dbContext.Adresses.FirstOrDefault(x => x.Id == person.Adress_Id);
                LoginData data = dbContext.LoginDatas.FirstOrDefault(x => x.Id == emp.LoginDataId);
                List<Email> emails = dbContext.Emails.Where(x => x.PersonId == person.Id).ToList();
                List<Phone_Numbers> numbers = dbContext.Phone_Numbers.Where(x => x.PersonId == person.Id).ToList();
                dbContext.Employees.Remove(emp);
                dbContext.LoginDatas.Remove(data);
                dbContext.Persons.Remove(person);
                dbContext.Emails.RemoveRange(emails);
                dbContext.Phone_Numbers.RemoveRange(numbers);
                dbContext.FIOs.Remove(fio);
                dbContext.Adresses.Remove(adress);
                dbContext.SaveChanges();
            }
        }

        public void UpdateEmployee(int id, UserEmployeeLong newEmp)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                Employee emp = dbContext.Employees.FirstOrDefault(x => x.Id == id);
                if (emp == null)
                    return;
                Person person = dbContext.Persons.FirstOrDefault(x => x.Id == emp.PersonId);
                FIO fio = dbContext.FIOs.FirstOrDefault(x => x.Id == person.FIO_Id);
                Adress adress = dbContext.Adresses.FirstOrDefault(x => x.Id == person.Adress_Id);
                LoginData data = dbContext.LoginDatas.FirstOrDefault(x => x.Id == emp.LoginDataId);
                List<Email> emails = dbContext.Emails.Where(x => x.PersonId == person.Id).ToList();
                List<Phone_Numbers> numbers = dbContext.Phone_Numbers.Where(x => x.PersonId == person.Id).ToList();
                if (fio.First_Name == newEmp.FirstName && fio.Last_Name == newEmp.LastName && fio.Patronymic == newEmp.Patronymic
                    && person.Birthday == newEmp.Birthday && adress.Country == newEmp.Country && adress.City == newEmp.City
                    && adress.Street == newEmp.Street && adress.House_Number == newEmp.House_Number && adress.Full_Adress == newEmp.Full_Adress
                    && data.Login == newEmp.Login && data.Password == newEmp.Password)
                    return;
                dbContext.Emails.RemoveRange(emails);
                dbContext.Phone_Numbers.RemoveRange(numbers);

                List<Email> newEmails = new List<Email>();
                foreach (var item in newEmp.Emails)
                {
                    newEmails.Add(new Email() { Email1 = item, Person = person });
                }

                List<Phone_Numbers> newNumbers = new List<Phone_Numbers>();
                foreach (var item in newEmp.PhoneNumbers)
                {
                    newNumbers.Add(new Phone_Numbers() { Phone_Number = item, Person = person });
                }
                dbContext.Phone_Numbers.AddRange(newNumbers);
                dbContext.Emails.AddRange(newEmails);

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
                if (newEmp.Password != null)
                    data.Password = PasswordHasher.HashPassword(newEmp.Password);

                emp.Salary = (decimal)newEmp.Salary;
                emp.RoleId = newEmp.EmployeeRoleId;
                emp.ProjectId = newEmp.ProjectId;

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<UserRole> GetUsersRoles()
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                List<UserRole> roles = new List<UserRole>();
                foreach (var item in dbContext.UsersRoles)
                {
                    roles.Add(new UserRole { Id = item.Id, Name = item.Name });
                }
                return roles;
            }
        }

        public void AddEmployeeRole(UserEmployeeRole role)
        {   
            EmployeesRole newRole = new EmployeesRole();
            newRole.UserRoleId = role.UserRoleId;
            EmployeeRoleDescription description = new EmployeeRoleDescription();
            description.Description = role.Description;
            description.Title = role.Title;
            newRole.EmployeeRoleDescription = description;
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                dbContext.EmployeesRoles.Add(newRole);
                dbContext.SaveChanges();
            }     
        }

        public void SetNewUserRoleForEmployeeRole(int userRoleId, int employeeRoleId)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                EmployeesRole role = dbContext.EmployeesRoles.FirstOrDefault(x => x.Id == employeeRoleId);
                if (role == null)
                    return;
                role.UserRoleId = userRoleId;
                dbContext.SaveChanges();
            }
        }
            

        public void RemoveEmployeeRole(int empRoleId)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                EmployeesRole role = dbContext.EmployeesRoles.FirstOrDefault(x => x.Id == empRoleId);
                if (role == null)
                    return;
                EmployeesRole newRole = dbContext.EmployeesRoles.Where(x => x.Id != empRoleId).First();
                List<Employee> employees = dbContext.Employees.Where(x => x.RoleId == empRoleId).ToList();
                for (int i = 0; i < employees.Count; i++)
                {
                    employees[i].RoleId = newRole.Id;
                }
                EmployeeRoleDescription desck = dbContext.EmployeeRoleDescriptions.FirstOrDefault(x => x.Id == role.DescriptionId);
                dbContext.EmployeesRoles.Remove(role);
                dbContext.EmployeeRoleDescriptions.Remove(desck);
                dbContext.SaveChanges();
            }  
        }

        public void UpdateEmployeeRole(int oldId, UserEmployeeRole newRole)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                EmployeesRole oldRole = dbContext.EmployeesRoles.FirstOrDefault(x => x.Id == oldId);
                if (oldRole == null)
                    return;
                EmployeeRoleDescription desck = dbContext.EmployeeRoleDescriptions.FirstOrDefault(x => x.Id == oldRole.DescriptionId);
                if (desck.Description == newRole.Description && desck.Title == newRole.Title && oldRole.UserRoleId == newRole.UserRoleId)
                    return;
                desck.Title = newRole.Title;
                desck.Description = newRole.Description;
                oldRole.UserRoleId = newRole.UserRoleId;
                dbContext.SaveChanges();
            }   
        }

        public User TryGetUser(string login, string password)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                LoginData loginData = dbContext.LoginDatas.FirstOrDefault(x => x.Login.Equals(login));
                User userData = new User();

                if (loginData == null)
                    return new User();

                if (!PasswordHasher.VerifyHashedPassword(loginData.Password, password))
                    return new User();


                Employee employee = dbContext.Employees.FirstOrDefault(x => x.LoginDataId == loginData.Id);
                Person person = dbContext.Persons.FirstOrDefault(x => x.Id == employee.PersonId);
                IEnumerable<Email> emails = dbContext.Emails.Where(x => x.PersonId == person.Id);
                IEnumerable<Phone_Numbers> phoneNumbers = dbContext.Phone_Numbers.Where(x => x.PersonId == person.Id);
                FIO fio = dbContext.FIOs.FirstOrDefault(x => x.Id == person.FIO_Id);
                Adress adress = dbContext.Adresses.FirstOrDefault(x => x.Id == person.Id);
                EmployeesRole employeeRole = dbContext.EmployeesRoles.FirstOrDefault(x => x.Id == employee.RoleId);
                UsersRole userRole = dbContext.UsersRoles.FirstOrDefault(x => x.Id == employeeRole.UserRoleId);
                var employeeDesc = dbContext.EmployeeRoleDescriptions.FirstOrDefault(x => x.Id == employeeRole.DescriptionId);
                Project project = dbContext.Projects.FirstOrDefault(x => x.Id == employee.ProjectId);
                var projectDesc = dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == project.DescriptionId);

                userData.FillUserProject(project.Id, projectDesc.Title, projectDesc.Description);

                userData.FillFio(employee.Id, fio.First_Name, fio.Last_Name, fio.Patronymic, person.Birthday);
                userData.FillAddress(adress.Country, adress.City, adress.Street, adress.House_Number, adress.Full_Adress);
                userData.FillPhoneNumbers(phoneNumbers);
                userData.FillEmails(emails);
                userData.FillRoleInfo(userRole.Id, userRole.Name, employeeDesc.Title, employeeDesc.Description);

                userData.FillLoginData(loginData.Login, loginData.Password);

                userData.FillEmployeeDate((float)employee.Salary);
                return userData;
            }
        }
    }
}
