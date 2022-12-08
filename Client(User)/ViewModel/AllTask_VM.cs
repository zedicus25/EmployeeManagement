using EmployeeManagement.Model;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
                    Tasks.Remove(SelectedTask);
                    SelectedTask = null;
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
            Tasks = new ObservableCollection<UserTask>();
            MainViewModel.GetInstance().ServerClient.AllTasks += SetTasks;
        }

        public void SetTasks(List<UserTask> tasks)
        {
            if (Tasks.Count <= 0)
            {
                FillTasks(tasks);
                return;
            }

            Tasks.Union(tasks);
        }

        private void FillTasks(List<UserTask> tasks)
        {
            Tasks = new ObservableCollection<UserTask>(tasks);
            OnPropertyChanged("Tasks");
        }

    }
}
