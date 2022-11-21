using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.ServerModels;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class UserTaskController
    {
        public IEnumerable<UserTask> GetUserTasks(int projectId, DbContext dbContext)
        {
            if(!(dbContext is EmployeeManagementContext))
                return new List<UserTask>();

            EmployeeManagementContext context = dbContext as EmployeeManagementContext;

            List<Models.Task> models = context.Tasks
                .Where(x => x.ProjectId.Equals(projectId)).ToList();

            List<UserTask> tasks = new List<UserTask>();


            foreach (var item in models)
            {
                Description taskDesc = context.Descriptions.FirstOrDefault(x => x.Id.Equals(item.DescriptionId));
                TaskCondition taskCondition = context.TaskConditions.FirstOrDefault(x => x.Id.Equals(item.TaskConditionId));
                Description conditionDesc = context.Descriptions.FirstOrDefault(x => x.Id.Equals(taskCondition.DescriptionId));
                Importance taskImportance = context.Importances.FirstOrDefault(x => x.Id.Equals(item.ImportanceId));
                Description importanceDesc = context.Descriptions.FirstOrDefault(x => x.Id.Equals(taskImportance.DescriptionId));
                Term taskTerm = context.Terms.FirstOrDefault(x => x.Id.Equals(item.TermId));

                tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.Description1, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete));
            }
            return tasks;
        }

        public void SetTaskCondition(int taskId, int conditionId, DbContext dbContext)
        {
            if (!(dbContext is EmployeeManagementContext))
                return;

            EmployeeManagementContext context = dbContext as EmployeeManagementContext;

            context.Tasks.FirstOrDefault(x => x.Id.Equals(taskId)).TaskConditionId = conditionId;
            context.SaveChanges();
        }
    }
}
