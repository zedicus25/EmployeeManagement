using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("Phone_Numbers")]
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Phone_Number { get; set; }
    }
}
