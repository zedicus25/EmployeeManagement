using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("Adresses")]
    public class Adress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House_Number { get; set; }
        public string Full_Adress { get; set; }
    }
}
