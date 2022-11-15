using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("Importances")]
    public class Importance
    {
        public int Id { get; set; }
        public int DescriptionId { get; set; }
    }
}
