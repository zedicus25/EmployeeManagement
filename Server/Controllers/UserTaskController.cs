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
                importances.Add(new TaskImportance() 
                { 
                    Id = item.Id, 
                    Title = importDesc.FirstOrDefault(x => x.Id == item.DescriptionId)?.Title,
                    Description = importDesc.FirstOrDefault(x => x.Id == item.DescriptionId)?.Description

                });
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
                    Title = importDesc.FirstOrDefault(x => x.Id == item.DescriptionId)?.Title,
                    Description = importDesc.FirstOrDefault(x => x.Id == item.DescriptionId)?.Description
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

            ProjectTaskDescription description = _dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == task.DescriptionId);
            Term term = _dbContext.Terms.FirstOrDefault(x => x.Id == task.TermId);

            _dbContext.ProjectTasks.Remove(task);
            _dbContext.Terms.Remove(term);
            _dbContext.ProjectTaskDescriptions.Remove(description);
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

        public void AddTaskCondition(UserTaskCondtion newCondition)
        {
            TaskCondition condititon = new TaskCondition();
            TaskConditionDescription description = new TaskConditionDescription();
            description.Title = newCondition.Title;
            description.Description = newCondition.Description;
            condititon.TaskConditionDescription = description;
            _dbContext.TaskConditions.Add(condititon);
            _dbContext.SaveChanges();
        }
        public void DeleteTaskCondition(int conditionId)
        {
            if (_dbContext.TaskConditions.Count() == 1)
                return;

            TaskCondition condititon = _dbContext.TaskConditions.FirstOrDefault(x => x.Id ==conditionId);
            if (condititon == null)
                return;
            TaskConditionDescription description = _dbContext.TaskConditionDescriptions.
                FirstOrDefault(x => x.Id == condititon.DescriptionId);

            TaskCondition newCondition = _dbContext.TaskConditions.Where(x => x.Id != conditionId).First();

            List<ProjectTask> tasks = _dbContext.ProjectTasks.Where(x => x.TaskConditionId == conditionId).ToList();
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].TaskConditionId = newCondition.Id;
            }

            _dbContext.TaskConditions.Remove(condititon);
            _dbContext.TaskConditionDescriptions.Remove(description);
            _dbContext.SaveChanges();
        }
        public void UpdateTaskCondition(int oldId, UserTaskCondtion newCondition)
        {
            TaskCondition condititon = _dbContext.TaskConditions.FirstOrDefault(x => x.Id == oldId);
            if (condititon == null)
                return;
            TaskConditionDescription description = _dbContext.TaskConditionDescriptions.
                FirstOrDefault(x => x.Id == condititon.DescriptionId);

            if (description.Title == newCondition.Title && description.Description == newCondition.Description)
                return;
            description.Description = newCondition.Description;
            description.Title = newCondition.Title;

            _dbContext.SaveChanges();
        }

        public void AddTaskImportance(TaskImportance newImportance)
        {
            Importance importance = new Importance();
            ImportanceDescription description = new ImportanceDescription();
            description.Title = newImportance.Title;
            description.Description = newImportance.Description;
            importance.ImportanceDescription = description;
            _dbContext.Importances.Add(importance);
            _dbContext.SaveChanges();
        }
        public void DeleteTaskImportance(int importanceId)
        {
            if (_dbContext.Importances.Count() == 1)
                return;

            Importance importance = _dbContext.Importances.FirstOrDefault(x => x.Id == importanceId);
            if (importance == null)
                return;
            ImportanceDescription description = _dbContext.ImportanceDescriptions.
                FirstOrDefault(x => x.Id == importance.DescriptionId);

            Importance newImportance = _dbContext.Importances.Where(x => x.Id != importanceId).First();

            List<ProjectTask> tasks = _dbContext.ProjectTasks.Where(x => x.ImportanceId == importanceId).ToList();
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].ImportanceId = newImportance.Id;
            }

            _dbContext.Importances.Remove(importance);
            _dbContext.ImportanceDescriptions.Remove(description);
            _dbContext.SaveChanges();
        }
        public void UpdateTaskImportance(int oldId, TaskImportance newImportance)
        {
            Importance importance = _dbContext.Importances.FirstOrDefault(x => x.Id == oldId);
            if (importance == null)
                return;
            ImportanceDescription description = _dbContext.ImportanceDescriptions.
                FirstOrDefault(x => x.Id == importance.DescriptionId);

            if (description.Title == newImportance.Title && description.Description == newImportance.Description)
                return;
            description.Description = newImportance.Description;
            description.Title = newImportance.Title;

            _dbContext.SaveChanges();
        }

    }
}
