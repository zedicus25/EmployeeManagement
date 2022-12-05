using Newtonsoft.Json;
using Server.Models;
using System;
using System.Collections.Generic;

namespace Server.ServerModels
{
    [Serializable]
    public class User 
    {
        public int Id { get; set; }
        #region Person Info
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Patronymic { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House_Number { get; set; }
        public string Full_Adress { get; set; }
        public DateTime Birthday { get; set; }
        public List<UserPhoneNumber> PhoneNumbers { get; set; }
        public List<UserEmail> Emails { get; set; }
        #endregion

        #region Role Info
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public string EmployeeRoleName { get; set; }
        public string EmployeeRoleDescription { get; set; }
        #endregion

        public UserProject Project { get; set; }

        #region LoginData
        public string Login { get; set; }
        public string Password { get; set; }
        #endregion

        public float Salary { get; set; }

        public void FillFio(int id,string firstName, string lastName, string patronymic, DateTime birthday)
        {
            Id = id;
            First_Name = firstName;
            Last_Name = lastName;
            Patronymic = patronymic;
            Birthday = birthday;
        }

        public void FillAddress(string country, string city, string street, string houseNumber, string fullAdress)
        {
            Country = country;
            City = city;
            Street = street;
            House_Number = houseNumber;
            Full_Adress = fullAdress;
        }

        public void FillPhoneNumbers(IEnumerable<Phone_Numbers> numbers)
        {
            PhoneNumbers = new List<UserPhoneNumber>();
            foreach (var item in numbers)
            {
                PhoneNumbers.Add(new UserPhoneNumber() { PhoneNumber = item.Phone_Number });
            }
        }
        public void FillEmails(IEnumerable<Email> emails)
        {
            Emails = new List<UserEmail>();
            foreach (var item in emails)
            {
                Emails.Add(new UserEmail() { Email = item.Email1 });
            }
        }

        public void FillRoleInfo(int userRoleId, string userRoleName, string employeeRoleName, string employeeRoleDescription)
        {
            UserRoleId = userRoleId;
            UserRoleName = userRoleName;
            EmployeeRoleName = employeeRoleName;
            EmployeeRoleDescription = employeeRoleDescription;
        }


        public void FillUserProject(int id, string title, string description)
        {
            Project = new UserProject();
            Project.Id = id;
            Project.Title = title;
            Project.Description = description;
            Project.Images = new List<string>();
        }

        public void FillLoginData(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public void FillEmployeeDate(float salary)
        {
            Salary = salary;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
