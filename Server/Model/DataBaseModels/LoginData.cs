using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("LoginData")]
    public class LoginData
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
