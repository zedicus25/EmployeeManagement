using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("Descriptions")]
    public class Descriptions
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
