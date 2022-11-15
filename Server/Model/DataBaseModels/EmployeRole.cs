using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("EmployeesRoles")]
    public class EmployeRole
    {
        public int Id { get; set; }
        public int DescriptionId { get; set; }
        public int UserRoleId { get; set; }
    }
}
