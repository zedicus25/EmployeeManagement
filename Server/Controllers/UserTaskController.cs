using Server.Models;
using Server.ServerModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Server.Controllers
{
    public class UserTaskController : BaseController
    {

        public UserTaskController(DbContext dbContext) :base(dbContext)
        {
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
                var projectDesk = _dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == item.ProjectId);
                var taskDesc = _dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == item.DescriptionId);
                TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id == item.TaskConditionId);
                var conditionDesc = _dbContext.TaskConditionDescriptions.FirstOrDefault(x => x.Id == taskCondition.DescriptionId);
                Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id == item.ImportanceId);
                var importanceDesc = _dbContext.ImportanceDescriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
                Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId);

                tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.TaskDescription, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete, projectDesk.Title));
            }
            return tasks;
        }

        public void SetTaskCondition(int userId, int taskId, int conditionId)
        {
            if (_dbContext == null)
                return;
            
            var task = _dbContext.ProjectTasks.FirstOrDefault(x => x.Id == taskId);
            task.TaskConditionId = conditionId;

            if (task.TaskConditionId == 3)
                task.EmployeeId = userId;
            else if (task.TaskConditionId == 1 || task.TaskConditionId == 4 || task.TaskConditionId == 2)
                task.EmployeeId = null;
           
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
                var projectDesk = _dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == item.ProjectId);
                var taskDesc = _dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == item.DescriptionId);
                TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id == item.TaskConditionId);
                var conditionDesc = _dbContext.TaskConditionDescriptions.FirstOrDefault(x => x.Id == taskCondition.DescriptionId);
                Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id == item.ImportanceId);
                var importanceDesc = _dbContext.ImportanceDescriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
                Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId);

                tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.TaskDescription, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete, projectDesk.Title));
            }

            return tasks;
        }

        public IEnumerable<TaskImportance> GetImportances()
        {
            List<TaskImportance> importances = new List<TaskImportance>();
            if (_dbContext == null)
                return importances;

            List<Importance> import = _dbContext.Importances.ToList();
            List<ImportanceDescription> importDesc = _dbContext.ImportanceDescriptions.ToList();
            foreach (var item in import)
            {
                importances.Add(new TaskImportance() { Id = item.Id, 
                    Title = importDesc.FirstOrDefault(x => x.Id == item.DescriptionId)?.Title });
            }
            return importances;
        }
        public IEnumerable<UserTaskCondtion> GetConditions()
        {
            List<UserTaskCondtion> conditions = new List<UserTaskCondtion>();
            if (_dbContext == null)
                return conditions;

            List<TaskCondition> import = _dbContext.TaskConditions.ToList();
            List<TaskConditionDescription> importDesc = _dbContext.TaskConditionDescriptions.ToList();
            foreach (var item in import)
            {
                conditions.Add(new UserTaskCondtion()
                {
                    Id = item.Id,
                    Title = importDesc.FirstOrDefault(x => x.Id == item.DescriptionId)?.Title
                });
            }
            return conditions;
        }

        public void AddTask(UserTask task)
        {
            ProjectTaskDescription project = new ProjectTaskDescription();
            project.TaskDescription = task.Description;
            project.Title = task.Title;
            Term term = new Term();
            term.CreationDate = DateTime.Parse(task.CreationDate.ToString("u"));
            term.ToComplete = DateTime.Parse(task.ToComplete.ToString("u"));

            ProjectTask newTask = new ProjectTask();
            newTask.Term = term;
            newTask.ProjectId = task.ProjectId;
            newTask.TaskConditionId = task.ConditionId;
            newTask.EmployeeId = task.EmployeeId;
            newTask.ProjectTaskDescription = project;
            newTask.ImportanceId = task.ImportanceId;

            _dbContext.ProjectTasks.Add(newTask);
            _dbContext.SaveChanges();
        }

        public bool DeleteTask(int id)
        {
            ProjectTask task = _dbContext.ProjectTasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
                return false;

            _dbContext.ProjectTasks.Remove(task);
            if (_dbContext.SaveChanges() > 0)
                return true;
            return false;
            
        }

        public void UpdateTask(int id, UserTask newTask)
        {
            ProjectTask task = _dbContext.ProjectTasks.FirstOrDefault(x => x.Id == id);
            ProjectTaskDescription project = _dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == task.DescriptionId);
            Term term = _dbContext.Terms.FirstOrDefault(x => x.Id == task.TermId);
            project.Title = newTask.Title;  
            project.TaskDescription = newTask.Description;
            term.ToComplete = newTask.ToComplete;
            task.ProjectId = newTask.ProjectId;
            task.ImportanceId = newTask.ImportanceId;
            task.EmployeeId = newTask.EmployeeId;
            task.TaskConditionId = newTask.ConditionId;
            _dbContext.SaveChanges();
        }

        public IEnumerable<UserTask> GetAllTasks()
        {
            if (_dbContext == null)
                return new List<UserTask>();

            List<ProjectTask> models = _dbContext.ProjectTasks.ToList();

            List<UserTask> tasks = new List<UserTask>();
            foreach (var item in models)
            {
                var projectDesk = _dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == item.ProjectId);
                var taskDesc = _dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == item.DescriptionId);
                TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id == item.TaskConditionId);
                var conditionDesc = _dbContext.TaskConditionDescriptions.FirstOrDefault(x => x.Id == taskCondition.DescriptionId);
                Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id == item.ImportanceId);
                var importanceDesc = _dbContext.ImportanceDescriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
                Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId);

                tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.TaskDescription, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete, projectDesk.Title));
            }
            return tasks;
        }

    }
}
