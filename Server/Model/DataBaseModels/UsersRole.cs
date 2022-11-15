using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("UsersRoles")]
    public class UsersRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
