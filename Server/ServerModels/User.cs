using Newtonsoft.Json;
using Server.Controllers;
using Server.Models;
using Image = Server.Models.Image;

namespace Server.ServerModels
{
    [Serializable]
    public class User : IJson
    {
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

        public UserTask Task { get; set; }
        public UserProject Project { get; set; }

        #region LoginData
        public string Login { get; set; }
        public string Password { get; set; }
        #endregion

        public float Salary { get; set; }
        public string Avatar { get; set; }

        public void FillFio(string firstName, string lastName, string patronymic, DateTime birthday)
        {
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

        public void FillPhoneNumbers(IEnumerable<PhoneNumber> numbers)
        {
            PhoneNumbers = new List<UserPhoneNumber>();
            foreach (var item in numbers)
            {
                PhoneNumbers.Add(new UserPhoneNumber() { PhoneNumber = item.PhoneNumber1 });
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

        public void FillUserTask(int id, string title, string description, int conditionId, string conditionName, int importanceId,
            string importanceName, DateTime creationDate, DateTime toComplete)
        {
            Task = new UserTask();
            Task.Id = id;
            Task.Title = title;
            Task.Description = description;
            Task.ConditionId = conditionId;
            Task.ConditionName = conditionName;
            Task.ImportanceId = importanceId;
            Task.ImportanceName = importanceName;
            Task.CreationDate = creationDate;
            Task.ToComplete = toComplete;
        }

        public void FillUserProject(int id, string title, string description, IEnumerable<Image> images)
        {
            Project = new UserProject();
            Project.Id = id;
            Project.Title = title;
            Project.Description = description;
            Project.Images = new List<string>();
            foreach (var item in images)
            {
                Project.Images.Add(item.Path);
            }
        }

        public void FillLoginData(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public void FillEmployeeDate(float salary, string avatar)
        {
            Salary = salary;
            Avatar = avatar;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
