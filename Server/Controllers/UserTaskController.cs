using Server.Models;
using Server.ServerModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Server.Controllers
{
    public class UserTaskController
    {
        EmployeeManagement _dbContext;

        public UserTaskController(DbContext dbContext)
        {
            _dbContext = dbContext as EmployeeManagement;
        }
        public IEnumerable<UserTask> GetAllProjectTasks(int projectId)
        {
            if (_dbContext == null)
                return new List<UserTask>();

            List<ProjectTask> models = _dbContext.ProjectTasks
                .Where(x => x.ProjectId == projectId && x.TaskConditionId != 3 && x.TaskConditionId != 1).ToList();

            List<UserTask> tasks = new List<UserTask>();


            foreach (var item in models)
            {
                var taskDesc = _dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == item.DescriptionId);
                TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id == item.TaskConditionId);
                var conditionDesc = _dbContext.TaskConditionDescriptions.FirstOrDefault(x => x.Id == taskCondition.DescriptionId);
                Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id == item.ImportanceId);
                var importanceDesc = _dbContext.ImportanceDescriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
                Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId);

                tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.TaskDescription, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete));
            }
            return tasks;
        }

        public void SetTaskCondition(int userId, int taskId, int conditionId)
        {
            if (_dbContext == null)
                return;
            
            var task = _dbContext.ProjectTasks.FirstOrDefault(x => x.Id == taskId);

            if (conditionId != 3)
            {
                task.EmployeeId = null;
                return;
            }

            task.TaskConditionId = conditionId;

            if (conditionId != 3)
                task.EmployeeId = null;
            else if (conditionId == 3)
                task.EmployeeId = userId;

            _dbContext.SaveChanges();
        }

        public IEnumerable<UserTask> GetUsersTask(int userId)
        {
            List<UserTask> tasks = new List<UserTask>();
            if (_dbContext == null)
                return tasks;

            Employee employee = _dbContext.Employees.FirstOrDefault(x => x.Id == userId);

            List<ProjectTask> models = _dbContext.ProjectTasks
                .Where(x => x.ProjectId == employee.ProjectId && x.EmployeeId == employee.Id && x.TaskConditionId != 4 && x.TaskConditionId != 1).ToList();

            foreach (var item in models)
            {
                var taskDesc = _dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == item.DescriptionId);
                TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id == item.TaskConditionId);
                var conditionDesc = _dbContext.TaskConditionDescriptions.FirstOrDefault(x => x.Id == taskCondition.DescriptionId);
                Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id == item.ImportanceId);
                var importanceDesc = _dbContext.ImportanceDescriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
                Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId);

                tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.TaskDescription, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete));
            }

            return tasks;
        }
    }
}
