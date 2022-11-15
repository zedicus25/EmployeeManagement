using System;

namespace Server.Model
{
    [Serializable]
    public class UserTask
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public int ConditionId { get; set; }
        public string ConditionName { get; set; }
        public int ImportanceId { get; set; }
        public int ImportanceName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ToComplete { get; set; }
    }
}
