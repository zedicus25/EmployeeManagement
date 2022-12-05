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

        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? new RelayCommand(DeleteTask); }
        }


        public TaskDeleteVM()
        {
            Tasks = new ObservableCollection<UserTask>();
            MainVM.GetInstance().ServerClient.GetAllTasks += GetAllTasks;
            MainVM.GetInstance().GetAllTasks();
        }

        private void GetAllTasks(IEnumerable<UserTask> obj)
        {
            Tasks.Clear();
            foreach (var item in obj)
            {
                Tasks.Add(item);
            }
        }

        private void DeleteTask()
        {
            if (_selectedTask == null)
                return;

            MainVM.GetInstance().DeleteTask(SelectedTask.Id);
            Tasks.Remove(SelectedTask);
            SelectedTask = null;
        }
    }
}
