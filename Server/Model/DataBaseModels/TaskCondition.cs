using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("TaskConditions")]
    public class TaskCondition
    {
        public int Id { get; set; }
        public int Description_Id { get; set; }
    }
}
