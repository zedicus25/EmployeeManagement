﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ServerModels
{
    [Serializable]
    public class UserEmployeeRole
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
