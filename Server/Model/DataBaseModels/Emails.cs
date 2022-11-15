using Dapper.Contrib.Extensions;


namespace Server.Model
{
    [Table("Emails")]
    public class Emails
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Email { get; set; }
    }
}
