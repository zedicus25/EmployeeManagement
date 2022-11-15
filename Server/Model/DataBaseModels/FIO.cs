using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("FIOs")]
    public class FIO
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Patronymic { get; set; }
    }
}
