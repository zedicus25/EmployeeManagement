using Server.Model;
using Server.Utilities;
using System.Collections.Generic;


namespace Server.Controller
{
    public class ProjectTaskController
    {
        public List<ProjectTask> Tasks { get; private set; }
        private JsonDataController<ProjectTask> _dataController;

        public ProjectTaskController()
        {
            _dataController = new JsonDataController<ProjectTask>("data", "tasks.txt");
            Tasks = _dataController.ReadData();
        }
    }
}
