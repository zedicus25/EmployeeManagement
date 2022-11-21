using EmployeeManagement.Model;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EmployeeManagement.ViewModel
{
    public class AllTask_VM : BaseVM
    {
        public UserTask SelectedTask
        {
            get => _selectedTask;
            set 
            { 
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }


        public RelayCommand GetTaskCommand
        {
            get
            {
                return _getTaskCommand ?? (_getTaskCommand = new RelayCommand(() =>
                {
                    if (_selectedTask == null)
                        return;

                    MainViewModel.GetInstance().ServerClient.GetTaskFromAll(_selectedTask.Id);
                    MainViewModel.GetInstance().GetAllTasks();
                }));
            }
        }

        public ObservableCollection<UserTask> Tasks
        {
            get => _tasks;
            set 
            { 
                _tasks = value; 
                OnPropertyChanged("Tasks");
            }
        }

        private ObservableCollection<UserTask> _tasks;
        private UserTask _selectedTask;
        

        private RelayCommand _getTaskCommand;
        public AllTask_VM()
        {
            MainViewModel.GetInstance().ServerClient.AllTasks += GetTasks;
        }

        private void GetTasks(List<UserTask> tasks)
        {
            _tasks = new ObservableCollection<UserTask>(tasks);
            OnPropertyChanged("Tasks");
        }

    }
}
