using Dapper.Contrib.Extensions;


namespace Server.Model
{
    [Table("Projects")]
    public class Project
    {
        public int Id { get; set; }
        public int Description_Id { get; set; }
    }
}
