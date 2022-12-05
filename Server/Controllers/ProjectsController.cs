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
    }
}
