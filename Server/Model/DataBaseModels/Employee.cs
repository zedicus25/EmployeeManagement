using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("Employees")]
    public class Employee
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int RoleId { get; set; }
        public int TaskId { get; set; }
        public int LoginDataId { get; set; }
        public float Salary { get; set; }
        public string Avatar { get; set; }
    }
}
