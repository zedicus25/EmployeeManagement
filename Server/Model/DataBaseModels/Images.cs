using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("Images")]
    public class Images
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Path { get; set; }
    }
}
