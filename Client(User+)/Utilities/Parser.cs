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
        public List<TaskImportant> GetTaskImportances(string data) => 
            JsonConvert.DeserializeObject<List<TaskImportant>>(data);
        public List<TaskCondition> GetTaskConditions(string data) =>
            JsonConvert.DeserializeObject<List<TaskCondition>>(data);
        public List<Employee> GetEmployees(string data) =>
            JsonConvert.DeserializeObject<List<Employee>>(data);
        public List<UserProject> GetProjects(string data) =>
            JsonConvert.DeserializeObject<List<UserProject>>(data);

        public List<UserTask> GetAllUserTasks(string data) =>
            JsonConvert.DeserializeObject<List<UserTask>>(data);
        public UserTask GetUserTask(string data) =>
            JsonConvert.DeserializeObject<UserTask>(data);
    }
}
