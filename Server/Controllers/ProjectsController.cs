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
        public ProjectsController(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<UserProject> GetAllProjects()
        {
            List<UserProject> projects = new List<UserProject>();
            if(_dbContext == null)
                 return projects;

            List<Project> proj = _dbContext.Projects.ToList();
            List<ProjectDescription> projDes = _dbContext.ProjectDescriptions.ToList();
            foreach (var item in proj)
            {
                ProjectDescription description = projDes.FirstOrDefault(x => x.Id == item.DescriptionId);
                projects.Add(new UserProject() { Id = item.Id, Title = description.Title, Description = description.Description });
            }
            return projects;

        }

        public void AddProject(UserProject userProject)
        {
            Project project = new Project();
            ProjectDescription desc = new ProjectDescription();
            desc.Description = userProject.Description;
            desc.Title = userProject.Title;
            project.ProjectDescription = desc;
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
        }
        public void DeleteProject(int id)
        {
            if (_dbContext.Projects.Count() == 1)
                return;
            Project project = _dbContext.Projects.FirstOrDefault(x=>x.Id == id);
            if (project == null)
                return;
            ProjectDescription desc =_dbContext.ProjectDescriptions.FirstOrDefault(x=>x.Id == project.DescriptionId);

            Project newProject = _dbContext.Projects.Where(x => x.Id != id).First();
            List<Employee> employees = _dbContext.Employees.Where(x => x.ProjectId == id).ToList();
            for (int i = 0; i < employees.Count; i++)
            {
                employees[i].ProjectId = newProject.Id;
            }

            _dbContext.Projects.Remove(project);
            _dbContext.ProjectDescriptions.Remove(desc);
            _dbContext.SaveChanges();

            
        }
        public void UpdateProject(int id, UserProject newProject)
        {
            Project project = _dbContext.Projects.FirstOrDefault(x => x.Id == id);
            if (project == null)
                return;
            ProjectDescription desc = _dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == project.DescriptionId);
            if (desc.Title == newProject.Title && desc.Description == newProject.Description)
                return;
            desc.Title = newProject.Title;
            desc.Description = newProject.Description;
            _dbContext.SaveChanges();
        }
    }
}
