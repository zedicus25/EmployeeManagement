using Dapper.Contrib.Extensions;
using Server.Model;
using System.Collections.Generic;


namespace Server.Utilities
{
    internal class DataBaseController
    {

        public static DataBaseController GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataBaseController();
                    }
                }
            }
            
            return _instance;
        }
        private static DataBaseController _instance;
        private static readonly object _lock = new object();

        private DataBaseController()
        {
        }

        public IEnumerable<Adress> GetAdresses() => SqlConnectionSingleton.GetInstance().GetAll<Adress>();
        public IEnumerable<Descriptions> GetDescriptions() => SqlConnectionSingleton.GetInstance().GetAll<Descriptions>();
        public IEnumerable<Emails> GetEmails() => SqlConnectionSingleton.GetInstance().GetAll<Emails>();
        public IEnumerable<Employee> GetEmployees() => SqlConnectionSingleton.GetInstance().GetAll<Employee>();
        public IEnumerable<EmployeRole> GetEmployeRoles() => SqlConnectionSingleton.GetInstance().GetAll<EmployeRole>();
        public IEnumerable<FIO> GetFIOs() => SqlConnectionSingleton.GetInstance().GetAll<FIO>();
        public IEnumerable<Images> GetImages() => SqlConnectionSingleton.GetInstance().GetAll<Images>();
        public IEnumerable<Importance> GetImportances() => SqlConnectionSingleton.GetInstance().GetAll<Importance>();
        public IEnumerable<LoginData> GetLoginData() => SqlConnectionSingleton.GetInstance().GetAll<LoginData>();
        public IEnumerable<Person> GetPersons() => SqlConnectionSingleton.GetInstance().GetAll<Person>();
        public IEnumerable<PhoneNumber> GetPhoneNumbers() => SqlConnectionSingleton.GetInstance().GetAll<PhoneNumber>();
        public IEnumerable<Project> GetProjects() => SqlConnectionSingleton.GetInstance().GetAll<Project>();
        public IEnumerable<TaskCondition> GetTaskConditions() => SqlConnectionSingleton.GetInstance().GetAll<TaskCondition>();
        public IEnumerable<ProjectTask> GetTasks() => SqlConnectionSingleton.GetInstance().GetAll<ProjectTask>();
        public IEnumerable<Term> GetTerms() => SqlConnectionSingleton.GetInstance().GetAll<Term>();
        public IEnumerable<UsersRole> GetUsersRoles() => SqlConnectionSingleton.GetInstance().GetAll<UsersRole>();

    }
}
