﻿using Client_Admin_.Model;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Markup;

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
        public IEnumerable<Employee> GetEmployees(string data) =>
            JsonConvert.DeserializeObject<List<Employee>>(data);
        public IEnumerable<UserRole> GetUserRoles(string data) =>
            JsonConvert.DeserializeObject<List<UserRole>>(data);
        public List<TaskImportance> GetTaskImportances(string data) =>
            JsonConvert.DeserializeObject<List<TaskImportance>>(data);
        public IEnumerable<TaskCondition> GetTaskConditions(string data) =>
            JsonConvert.DeserializeObject<List<TaskCondition>>(data);
    }
}
