
using Server.Models;
using Server.ServerModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class UserTaskController
    {
        EmployeeManagement _dbContext;

        public UserTaskController(DbContext dbContext)
        {
            _dbContext = dbContext as EmployeeManagement;
        }
        public IEnumerable<UserTask> GetUserTasks(int projectId)
        {
            if (_dbContext == null)
                return new List<UserTask>();

            List<ProjectTask> models = _dbContext.ProjectTasks
                .Where(x => x.ProjectId == projectId && x.TaskConditionId != 3 && x.TaskConditionId != 1).ToList();

            List<UserTask> tasks = new List<UserTask>();


            foreach (var item in models)
            {
                Description taskDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == item.DescriptionId);
                TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id == item.TaskConditionId);
                Description conditionDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == taskCondition.Description_Id);
                Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id == item.ImportanceId);
                Description importanceDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
                Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId);

                tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.Description1, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete));
            }
            return tasks;
        }

        public void SetTaskCondition(int userId, int taskId, int conditionId)
        {
            if (_dbContext == null)
                return;
            
            _dbContext.Employees.FirstOrDefault(x => x.Id == userId).TaskId = taskId;
            _dbContext.ProjectTasks.FirstOrDefault(x => x.Id == taskId).TaskConditionId = conditionId;
            if (conditionId == 1)
            {
                _dbContext.Employees.FirstOrDefault(x => x.Id == userId).TaskId = null;
            }
            _dbContext.SaveChanges();
        }

        public UserTask GetUserTask(int userId)
        {
            if (_dbContext == null)
                return new UserTask();

            Employee employee = _dbContext.Employees.FirstOrDefault(x => x.Id == userId);
            if (employee.TaskId == null)
                return new UserTask();
            ProjectTask item = _dbContext.ProjectTasks.FirstOrDefault(x => x.Id == employee.TaskId);

            Description taskDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == item.DescriptionId);
            TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id == item.TaskConditionId);
            Description conditionDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == taskCondition.Description_Id);
            Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id == item.ImportanceId);
            Description importanceDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
            Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId);

            return new UserTask(item.Id, taskDesc.Title, taskDesc.Description1, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete);
        }
    }
}
