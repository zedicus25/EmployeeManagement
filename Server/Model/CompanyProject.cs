
namespace Server.Model
{
    public class CompanyProject
    {
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public int EmployeeCount { get; set; }
        public string Company { get; set; }
        public int Id { get; set; }

        public CompanyProject(string projectTitle, string projectDescription, int employeeCount, string company, int id)
        {
            this.ProjectTitle = projectTitle;
            this.ProjectDescription = projectDescription;
            this.EmployeeCount = employeeCount;
            this.Company = company;
            this.Id = id;
        }
    }
}
