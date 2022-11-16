using System;
using System.Collections.Generic;

namespace Server.ServerModels
{
    [Serializable]
    public class UserProject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
    }
}
