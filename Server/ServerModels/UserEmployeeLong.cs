using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ServerModels
{
    public class UserEmployeeLong
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House_Number { get; set; }
        public string Full_Adress { get; set; }
        public DateTime Birthday { get; set; }
        public float Salary { get; set; }
        public List<string> Emails { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public int EmployeeRoleId { get; set; }
        public int ProjectId { get; set; }
        public int UserRoleId { get; set; }
    }
}
