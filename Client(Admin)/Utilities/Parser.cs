using Client_Admin_.Model;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Client_Admin_.Utilities
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
        public IEnumerable<EmployeeRole> GetEmployeeRoles(string data) 
            => JsonConvert.DeserializeObject<List<EmployeeRole>>(data);
        public IEnumerable<Project> GetProjects(string data) =>
            JsonConvert.DeserializeObject<List<Project>>(data);
    }
}
