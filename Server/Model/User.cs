using Newtonsoft.Json;
using System;

namespace Server.Model
{
    [Serializable]
    public class User
    {
        public uint DataBaseId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public float Salary { get; set; }
        public string CompanyName { get; set; }
        public int CurrentProject { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Avatar { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsMainAdmin { get; set; }
        

        public User(uint dataBaseId, string login, string password, string name, string lastName, string patronymic, DateTime birthday, 
            float salary, string companyName, int currentProject, string position, string email, string phoneNumber, string country, 
            string city, string adress, string avatar, bool isAdmin, bool isMainAdmin)
        {
            DataBaseId = dataBaseId;
            Login = login;
            Password = password;
            Name = name;
            LastName = lastName;
            Patronymic = patronymic;
            Birthday = birthday;
            Salary = salary;
            CompanyName = companyName;
            CurrentProject = currentProject;
            Position = position;
            Email = email;
            PhoneNumber = phoneNumber;
            Country = country;
            City = city;
            Adress = adress;
            Avatar = avatar;
            IsAdmin = isAdmin;
            IsMainAdmin = isMainAdmin;
            
        }

        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}
