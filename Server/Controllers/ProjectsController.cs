using Server.Models;
using Server.ServerModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class ProjectsController : BaseController
    {
        public IEnumerable<UserProject> GetAllProjects()
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                List<UserProject> projects = new List<UserProject>();
                if (dbContext == null)
                    return projects;

                List<Project> proj = dbContext.Projects.ToList();
                List<ProjectDescription> projDes = dbContext.ProjectDescriptions.ToList();
                foreach (var item in proj)
                {
                    ProjectDescription description = projDes.FirstOrDefault(x => x.Id == item.DescriptionId);
                    projects.Add(new UserProject() { Id = item.Id, Title = description.Title, Description = description.Description });
                }
                return projects;
            }
        }

        public void AddProject(UserProject userProject)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                Project project = new Project();
                ProjectDescription desc = new ProjectDescription();
                if (userProject.Title == String.Empty || userProject.Description == String.Empty)
                    return;
                desc.Description = userProject.Description;
                desc.Title = userProject.Title;
                project.ProjectDescription = desc;
                dbContext.Projects.Add(project);
                dbContext.SaveChanges();
            }  
        }
        public void DeleteProject(int id)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                Project project = dbContext.Projects.FirstOrDefault(x => x.Id == id);
                if (project == null)
                    return;
                ProjectDescription desc = dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == project.DescriptionId);

                Project newProject = dbContext.Projects.Where(x => x.Id != id).First();
                List<Employee> employees = dbContext.Employees.Where(x => x.ProjectId == id).ToList();
                for (int i = 0; i < employees.Count; i++)
                {
                    employees[i].ProjectId = newProject.Id;
                }
                List<ProjectTask> tasks = dbContext.ProjectTasks.Where(x => x.ProjectId == id).ToList();
                List<ProjectTaskDescription> descriptions = new List<ProjectTaskDescription>();
                List<Term> terms = new List<Term>();
                foreach (var item in tasks)
                {
                    descriptions.Add(dbContext.ProjectTaskDescriptions.FirstOrDefault(x => x.Id == item.DescriptionId));
                    terms.Add(dbContext.Terms.FirstOrDefault(x => x.Id == item.TermId));
                }
                dbContext.ProjectTasks.RemoveRange(tasks);
                dbContext.ProjectTaskDescriptions.RemoveRange(descriptions);
                dbContext.Terms.RemoveRange(terms);
                dbContext.Projects.Remove(project);
                dbContext.ProjectDescriptions.Remove(desc);
                dbContext.SaveChanges();
            } 
        }
        public void UpdateProject(int id, UserProject newProject)
        {
            using (EmployeeManagement dbContext = new EmployeeManagement())
            {
                if (newProject.Title == String.Empty || newProject.Description == String.Empty)
                    return;
                Project project = dbContext.Projects.FirstOrDefault(x => x.Id == id);
                if (project == null)
                    return;
                ProjectDescription desc = dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == project.DescriptionId);
                if (desc.Title == newProject.Title && desc.Description == newProject.Description)
                    return;
                desc.Title = newProject.Title;
                desc.Description = newProject.Description;
                dbContext.SaveChanges();
            } 
        }
    }
}
