using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Server.Model
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
        public int UserRoleName { get; set; }
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

        public void FillFio(string firstName, string lastName, string patronymic)
        {
            First_Name = firstName;
            Last_Name = lastName;
            Patronymic = patronymic;
        }

        public void FillAddress(string country, string city, string street, string houseNumber, string fullAdress)
        {

        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
