using EmployeeManagement.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EmployeeManagement.ViewModel
{
    public class AllTask_VM : BaseVM
    {
        public ProjectTask SelectedTask
        {
            get => _selectedTask;
            set 
            { 
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }


        public ObservableCollection<ProjectTask> Tasks
        {
            get => _tasks;
            set 
            { 
                _tasks = value; 
                OnPropertyChanged("Tasks");
            }
        }

        private ObservableCollection<ProjectTask> _tasks;
        private ProjectTask _selectedTask;
        public AllTask_VM()
        {
            MainViewModel.Instance.ServerClient.AllTasks += GetTasks;
            MainViewModel.Instance.ServerClient.GetAllTasks();
        }

        

        private void GetTasks(List<ProjectTask> tasks) => _tasks = new ObservableCollection<ProjectTask>(tasks);
    }
}
