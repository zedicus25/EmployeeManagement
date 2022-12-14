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

       
        public IEnumerable<UserTask> GetAllProjectTasks(int projectId)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {

                List<ProjectTask> models = dbContext.ProjectTasks
                    .Where(x => x.ProjectId == projectId && x.TaskConditionId != 3 && x.TaskConditionId != 1).ToList();

                List<UserTask> tasks = new List<UserTask>();


                foreach (var item in models)
                {
                    var projectDesk = dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == item.ProjectId);
                    var taskDesc = dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == item.DescriptionId);
                    TaskCondition taskCondition = dbContext.TaskConditions.FirstOrDefault(x => x.Id == item.TaskConditionId);
                    var conditionDesc = dbContext.TaskConditionDescriptions.FirstOrDefault(x => x.Id == taskCondition.DescriptionId);
                    Importance taskImportance = dbContext.Importances.FirstOrDefault(x => x.Id == item.ImportanceId);
                    var importanceDesc = dbContext.ImportanceDescriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
                    Term taskTerm = dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId);

                    tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.TaskDescription, taskCondition.Id, conditionDesc.Title,
                        taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete, projectDesk.Title,
                        (int)item.ProjectId, item.EmployeeId == null ? 0 : (int)item.EmployeeId));
                }
                return tasks;
            }
        }
        public void SetTaskCondition(int userId, int taskId, int conditionId)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                var task = dbContext.ProjectTasks.FirstOrDefault(x => x.Id == taskId);
                task.TaskConditionId = conditionId;

                if (task.TaskConditionId == 3)
                    task.EmployeeId = userId;
                else if (task.TaskConditionId == 1 || task.TaskConditionId == 4 || task.TaskConditionId == 2)
                    task.EmployeeId = null;

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<UserTask> GetUsersTask(int userId)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                List<UserTask> tasks = new List<UserTask>();

                Employee employee = dbContext.Employees.FirstOrDefault(x => x.Id == userId);

                List<ProjectTask> models = dbContext.ProjectTasks
                    .Where(x => x.ProjectId == employee.ProjectId && x.EmployeeId == employee.Id && x.TaskConditionId != 4 && x.TaskConditionId != 1).ToList();

                foreach (var item in models)
                {
                    var projectDesk = dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == item.ProjectId);
                    var taskDesc = dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == item.DescriptionId);
                    TaskCondition taskCondition = dbContext.TaskConditions.FirstOrDefault(x => x.Id == item.TaskConditionId);
                    var conditionDesc = dbContext.TaskConditionDescriptions.FirstOrDefault(x => x.Id == taskCondition.DescriptionId);
                    Importance taskImportance = dbContext.Importances.FirstOrDefault(x => x.Id == item.ImportanceId);
                    var importanceDesc = dbContext.ImportanceDescriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
                    Term taskTerm = dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId);

                    tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.TaskDescription, taskCondition.Id, conditionDesc.Title,
                        taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete, projectDesk.Title,
                        (int)item.ProjectId, item.EmployeeId == null ? 0 : (int)item.EmployeeId));
                }

                return tasks;
            }
        }

        public IEnumerable<TaskImportance> GetImportances()
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                List<TaskImportance> importances = new List<TaskImportance>();

                List<Importance> import = dbContext.Importances.ToList();
                List<ImportanceDescription> importDesc = dbContext.ImportanceDescriptions.ToList();
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
        }
        public IEnumerable<UserTaskCondtion> GetConditions()
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                List<UserTaskCondtion> conditions = new List<UserTaskCondtion>();

                List<TaskCondition> import = dbContext.TaskConditions.ToList();
                List<TaskConditionDescription> importDesc = dbContext.TaskConditionDescriptions.ToList();
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
        }

        public void AddTask(UserTask task)
        {
            if (task.ProjectId == 0 || task.ImportanceId == 0 || task.ConditionId == 0 ||
                task.Description == String.Empty || task.Title == String.Empty ||
                task.ToComplete == null)
                return;
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
            if(task.EmployeeId != 0)
                newTask.EmployeeId = task.EmployeeId;
            else
                newTask.EmployeeId = null;
            newTask.ProjectTaskDescription = project;
            newTask.ImportanceId = task.ImportanceId;

            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                dbContext.ProjectTasks.Add(newTask);
                dbContext.SaveChanges();
            }        
        }

        public bool DeleteTask(int id)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                ProjectTask task = dbContext.ProjectTasks.FirstOrDefault(x => x.Id == id);
                if (task == null)
                    return false;

                ProjectTaskDescription description = dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == task.DescriptionId);
                Term term = dbContext.Terms.FirstOrDefault(x => x.Id == task.TermId);

                dbContext.ProjectTasks.Remove(task);
                dbContext.Terms.Remove(term);
                dbContext.ProjectTaskDescriptions.Remove(description);
                if (dbContext.SaveChanges() > 0)
                    return true;
                return false;
            }
        }

        public void UpdateTask(int id, UserTask newTask)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                if (newTask.ProjectId == 0 || newTask.ImportanceId == 0 || newTask.ConditionId == 0 ||
               newTask.Description == String.Empty || newTask.Title == String.Empty ||
               newTask.ToComplete == null)
                    return;

                ProjectTask task = dbContext.ProjectTasks.FirstOrDefault(x => x.Id == id);
                ProjectTaskDescription project = dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == task.DescriptionId);
                Term term = dbContext.Terms.FirstOrDefault(x => x.Id == task.TermId);

                if (project.Title == newTask.Title && project.TaskDescription == project.TaskDescription
                    && task.TaskConditionId == newTask.ConditionId && task.EmployeeId == newTask.EmployeeId &&
                    task.ImportanceId == newTask.ImportanceId && task.ProjectId == newTask.ProjectId && term.ToComplete == newTask.ToComplete)
                    return;

                project.Title = newTask.Title;
                project.TaskDescription = newTask.Description;
                term.ToComplete = newTask.ToComplete;
                task.ProjectId = newTask.ProjectId;
                task.ImportanceId = newTask.ImportanceId;
                if (newTask.EmployeeId == 0)
                    task.EmployeeId = null;
                else
                    task.EmployeeId = newTask.EmployeeId;
                task.TaskConditionId = newTask.ConditionId;
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<UserTask> GetAllTasks()
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                List<ProjectTask> models = dbContext.ProjectTasks.ToList();

                List<UserTask> tasks = new List<UserTask>();
                foreach (var item in models)
                {
                    var projectDesk = dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == item.ProjectId);
                    var taskDesc = dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == item.DescriptionId);
                    TaskCondition taskCondition = dbContext.TaskConditions.FirstOrDefault(x => x.Id == item.TaskConditionId);
                    var conditionDesc = dbContext.TaskConditionDescriptions.FirstOrDefault(x => x.Id == taskCondition.DescriptionId);
                    Importance taskImportance = dbContext.Importances.FirstOrDefault(x => x.Id == item.ImportanceId);
                    var importanceDesc = dbContext.ImportanceDescriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
                    Term taskTerm = dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId);

                    tasks.Add(new UserTask(item.Id, taskDesc.Title, taskDesc.TaskDescription, taskCondition.Id, conditionDesc.Title,
                        taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete, projectDesk.Title,
                        (int)item.ProjectId, item.EmployeeId == null ? 0 : (int)item.EmployeeId));
                }
                return tasks;
            }
        }

        public void AddTaskCondition(UserTaskCondtion newCondition)
        {
            if (newCondition.Title == String.Empty || newCondition.Description == String.Empty)
                return;
            TaskCondition condititon = new TaskCondition();
            TaskConditionDescription description = new TaskConditionDescription();
            description.Title = newCondition.Title;
            description.Description = newCondition.Description;
            condititon.TaskConditionDescription = description;
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                dbContext.TaskConditions.Add(condititon);
                dbContext.SaveChanges();
            }
            
        }
        public void DeleteTaskCondition(int conditionId)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                if (dbContext.TaskConditions.Count() == 1)
                    return;

                TaskCondition condititon = dbContext.TaskConditions.FirstOrDefault(x => x.Id == conditionId);
                if (condititon == null)
                    return;
                TaskConditionDescription description = dbContext.TaskConditionDescriptions.
                    FirstOrDefault(x => x.Id == condititon.DescriptionId);

                TaskCondition newCondition = dbContext.TaskConditions.Where(x => x.Id != conditionId).First();

                List<ProjectTask> tasks = dbContext.ProjectTasks.Where(x => x.TaskConditionId == conditionId).ToList();
                for (int i = 0; i < tasks.Count; i++)
                {
                    tasks[i].TaskConditionId = newCondition.Id;
                }

                dbContext.TaskConditions.Remove(condititon);
                dbContext.TaskConditionDescriptions.Remove(description);
                dbContext.SaveChanges();
            }
        }
        public void UpdateTaskCondition(int oldId, UserTaskCondtion newCondition)
        {
            if (newCondition.Title == String.Empty || newCondition.Description == String.Empty)
                return;
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                TaskCondition condititon = dbContext.TaskConditions.FirstOrDefault(x => x.Id == oldId);
                if (condititon == null)
                    return;
                TaskConditionDescription description = dbContext.TaskConditionDescriptions.
                    FirstOrDefault(x => x.Id == condititon.DescriptionId);

                if (description.Title == newCondition.Title && description.Description == newCondition.Description)
                    return;
                description.Description = newCondition.Description;
                description.Title = newCondition.Title;

                dbContext.SaveChanges();
            }
        }

        public void AddTaskImportance(TaskImportance newImportance)
        {
            if (newImportance.Title == String.Empty || newImportance.Description == String.Empty)
                return;

            Importance importance = new Importance();
            ImportanceDescription description = new ImportanceDescription();
            description.Title = newImportance.Title;
            description.Description = newImportance.Description;
            importance.ImportanceDescription = description;
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                dbContext.Importances.Add(importance);
                dbContext.SaveChanges();
            }   
        }
        public void DeleteTaskImportance(int importanceId)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                if (dbContext.Importances.Count() == 1)
                    return;

                Importance importance = dbContext.Importances.FirstOrDefault(x => x.Id == importanceId);
                if (importance == null)
                    return;
                ImportanceDescription description = dbContext.ImportanceDescriptions.
                    FirstOrDefault(x => x.Id == importance.DescriptionId);

                Importance newImportance = dbContext.Importances.Where(x => x.Id != importanceId).First();

                List<ProjectTask> tasks = dbContext.ProjectTasks.Where(x => x.ImportanceId == importanceId).ToList();
                for (int i = 0; i < tasks.Count; i++)
                {
                    tasks[i].ImportanceId = newImportance.Id;
                }

                dbContext.Importances.Remove(importance);
                dbContext.ImportanceDescriptions.Remove(description);
                dbContext.SaveChanges();
            }
        }
        public void UpdateTaskImportance(int oldId, TaskImportance newImportance)
        {
            if (newImportance.Title == String.Empty || newImportance.Description == String.Empty)
                return;
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                Importance importance = dbContext.Importances.FirstOrDefault(x => x.Id == oldId);
                if (importance == null)
                    return;
                ImportanceDescription description = dbContext.ImportanceDescriptions.
                    FirstOrDefault(x => x.Id == importance.DescriptionId);

                if (description.Title == newImportance.Title && description.Description == newImportance.Description)
                    return;
                description.Description = newImportance.Description;
                description.Title = newImportance.Title;

                dbContext.SaveChanges();
            }
        }

    }
}
