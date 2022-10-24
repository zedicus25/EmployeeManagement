using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModel
{
    public class AllTask_VM : BaseVM
    {
        public List<ProjectTask> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }

        private List<ProjectTask> _tasks;
        public AllTask_VM()
        {
            MainViewModel.Instance.ServerClient.AllTasks += GetTasks;
            MainViewModel.Instance.ServerClient.GetAllTasks();
        }

        private void GetTasks(List<ProjectTask> tasks) => _tasks = tasks;
    }
}
