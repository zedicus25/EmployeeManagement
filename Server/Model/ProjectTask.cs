using System;

namespace Server.Model
{
    [Serializable]
    public class ProjectTask
    {
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int Importance { get; set; }
        public DateTime Term { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public bool IsComplete { get; set; }

        public ProjectTask(string taskTitle, string taskDesciption, int importance, DateTime term, int userId, int id, int projectId, bool isComplete)
        {
            this.TaskTitle = taskTitle;
            this.TaskDescription = taskDesciption;
            this.Importance = importance;
            this.Term = term;
            this.UserId = userId;
            this.Id = id;
            this.ProjectId = projectId;
            this.IsComplete = isComplete;
        }
    }
}
