using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.ServerModels;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class UserTaskController
    {
        EmployeeManagementContext _dbContext;

        public UserTaskController(DbContext dbContext)
        {
            _dbContext = dbContext as EmployeeManagementContext;
        }
        public IEnumerable<UserTask> GetUserTasks(int projectId)
        {
            if (_dbContext == null)
                return new List<UserTask>();

            List<Models.Task> models = _dbContext.Tasks
                .Where(x => x.ProjectId.Equals(projectId) && x.TaskConditionId != 3).ToList();

            List<UserTask> tasks = new List<UserTask>();


            foreach (var item in models)
            {
                Description taskDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(item.DescriptionId));
                TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id.Equals(item.TaskConditionId));
                Description conditionDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(taskCondition.DescriptionId));
                Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id.Equals(item.ImportanceId));
                Description importanceDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(taskImportance.DescriptionId));
                Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id.Equals(item.TermId));

                tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.Description1, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete));
            }
            return tasks;
        }

        public void SetTaskCondition(int userId, int taskId, int conditionId)
        {
            if (_dbContext == null)
                return;

            EmployeeManagementContext context = _dbContext as EmployeeManagementContext;
            context.Employees.FirstOrDefault(x => x.Id.Equals(userId)).TaskId = taskId;
            context.Tasks.FirstOrDefault(x => x.Id.Equals(taskId)).TaskConditionId = conditionId;
            context.SaveChanges();
        }

        public UserTask GetUserTask(int userId)
        {
            if (_dbContext == null)
                return new UserTask();

            Employee employee = _dbContext.Employees.FirstOrDefault(x => x.Id.Equals(userId));
            Models.Task item = _dbContext.Tasks.FirstOrDefault(x => x.Id.Equals(employee.TaskId));

            Description taskDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(item.DescriptionId));
            TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id.Equals(item.TaskConditionId));
            Description conditionDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(taskCondition.DescriptionId));
            Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id.Equals(item.ImportanceId));
            Description importanceDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(taskImportance.DescriptionId));
            Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id.Equals(item.TermId));

            return new UserTask(item.Id, taskDesc.Title, taskDesc.Description1, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete);
        }
    }
}
