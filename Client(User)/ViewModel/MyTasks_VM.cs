using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EmployeeManagement.Model;
using GalaSoft.MvvmLight.Command;

namespace EmployeeManagement.ViewModel
{
    public class MyTasks_VM : BaseVM
    {
        public ObservableCollection<UserTask> MyTasks
        {
            get => _tasks;
            set 
            { 
                _tasks = value;
                OnPropertyChanged("MyTasks");
            }
        }
        public UserTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }


        public string BranchName
        {
            get => _branchName;
            set
            {
                _branchName = value;
                OnPropertyChanged("BranchName");
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        public RelayCommand SubmitTask
        {
            get
            {
                return _submitTaskCommand ?? (_submitTaskCommand = new RelayCommand(() =>
                {
                    if (BranchName.Equals(String.Empty) || Message.Equals(String.Empty))
                        return;

                    /*MainViewModel.GetInstance().SubmitTask(MyTask.Id, BranchName, Message);
                    Message = String.Empty;
                    BranchName = String.Empty;
                    MyTask = null;*/
                }));
            }
        }

        private string _branchName;
        private string _message;
        private UserTask _selectedTask;

        private RelayCommand _submitTaskCommand;
        
        private ObservableCollection<UserTask> _tasks;

        public MyTasks_VM()
        {
            MyTasks = new ObservableCollection<UserTask>();
            Message = String.Empty;
            BranchName = String.Empty;
            MainViewModel.GetInstance().ServerClient.MyTask += SetUserTasks;
        }

        private void SetUserTasks(List<UserTask> tasks)
        {
            if (MyTasks.Count <= 0)
            {
                FillTasks(tasks);
                return;
            }

            MyTasks.Union(tasks);
        }

        private void FillTasks(IEnumerable<UserTask> tasks)
        {
            MyTasks = new ObservableCollection<UserTask>(tasks);
            OnPropertyChanged("MyTasks");
        }
    }
}
