using Dapper.Contrib.Extensions;

namespace Server.Model
{
    [Table("Tasks")]
    public class ProjectTask
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int DescriptionId { get; set; }
        public int TaskConditionId { get; set; }
        public int ImportanceId { get; set; }
        public int TermId { get; set; }

    }
}
