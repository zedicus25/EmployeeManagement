﻿using EmployeeManagement.Model;
using Newtonsoft.Json;

namespace EmployeeManagement.Utilities
{
    public class Parser
    {
        public static Parser Instance { get; private set; }

        public Parser()
        {
            Instance = this;
        }

        public User GetUser(string data) => JsonConvert.DeserializeObject<User>(data);
    }
}
