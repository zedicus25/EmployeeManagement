using Client_User__.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_User__.ViewModel
{
    public class TaskDeleteVM :BaseVM
    {
        private ObservableCollection<UserTask> _userTasks;

        public ObservableCollection<UserTask> Tasks
        {
            get => _userTasks;
            set 
            { 
                _userTasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        private UserTask _selectedTask;

        public UserTask SelectedTask
        {
            get => _selectedTask;
            set 
            { 
                _selectedTask = value; 
                OnPropertyChanged("SelectedTask");
            }
        }
        public bool CanRemoveTasks
        {
            get => _canAddTasks;
            set
            {
                _canAddTasks = value;
                OnPropertyChanged("CanRemoveTasks");
            }
        }
        private bool _canAddTasks;

        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? new RelayCommand(DeleteTask); }
        }


        public TaskDeleteVM()
        {
            CanRemoveTasks = false;
            Tasks = new ObservableCollection<UserTask>();
            MainVM.GetInstance().ServerClient.GetAllTasks += GetAllTasks;
        }

        private void GetAllTasks(List<UserTask> obj)
        {
            CanRemoveTasks = true;
            Tasks = new ObservableCollection<UserTask>(obj);
            OnPropertyChanged("Tasks");
        }

        private async void DeleteTask()
        {
            if (_selectedTask == null)
                return;

            MainVM.GetInstance().DeleteTask(SelectedTask.Id);
            Tasks.Remove(SelectedTask);
            SelectedTask = null;
            CanRemoveTasks = false;
            await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForAllTasks();
        }
    }
}
