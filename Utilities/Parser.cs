﻿using EmployeeManagement.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EmployeeManagement.Utilities
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
        public List<ProjectTask> GetTasks(string data) => JsonConvert.DeserializeObject<List<ProjectTask>>(data);
    }
}
