using Client_User__.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Client_User__.Utilities
{
    public class Parser
    {
        public static Parser GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Parser();
            }
            return _instance;
        }
        private static Parser _instance;

        private Parser()
        {
        }

        public User GetUser(string data) => JsonConvert.DeserializeObject<User>(data);
        public IEnumerable<TaskImportant> GetTaskImportances(string data) => 
            JsonConvert.DeserializeObject<List<TaskImportant>>(data);
        public IEnumerable<Employee> GetEmployees(string data) =>
            JsonConvert.DeserializeObject<List<Employee>>(data);
        public IEnumerable<UserProject> GetProjects(string data) =>
            JsonConvert.DeserializeObject<List<UserProject>>(data);

        public IEnumerable<UserTask> GetUserTasks(string data) =>
            JsonConvert.DeserializeObject<List<UserTask>>(data);
    }
}
