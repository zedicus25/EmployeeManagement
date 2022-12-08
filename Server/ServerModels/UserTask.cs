
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
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public int EmployeeId { get; set; }
        public string ConditionName { get; set; }
        public int ImportanceId { get; set; }
        public string ImportanceName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ToComplete { get; set; }

        public UserTask()
        {
        }

        public UserTask(int id, string title, string description, int conditionId, string conditionName, 
            int importanceId, string importanceName, DateTime creationDate, DateTime toComplete ,string projectTitle, 
            int projectId, int employeeId)
        {
            this.Id = id;
            this.Title = title;
            this.EmployeeId = employeeId;
            this.Description = description;
            this.ConditionId = conditionId;
            this.ProjectTitle = projectTitle;
            this.ConditionName = conditionName;
            this.ImportanceId = importanceId;
            this.ImportanceName = importanceName;
            this.CreationDate = creationDate;
            this.ToComplete = toComplete;
            this.ProjectId = projectId;
        }
    }
}
