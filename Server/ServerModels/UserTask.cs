
using System;

namespace Server.ServerModels
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
        public string ImportanceName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ToComplete { get; set; }

        public UserTask()
        {
        }

        public UserTask(int id, string title, string description, int conditionId, string conditionName, int importanceId, string importanceName, DateTime creationDate, DateTime toComplete)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.ConditionId = conditionId;
            this.ConditionName = conditionName;
            this.ImportanceId = importanceId;
            this.ImportanceName = importanceName;
            this.CreationDate = creationDate;
            this.ToComplete = toComplete;
        }
    }
}
