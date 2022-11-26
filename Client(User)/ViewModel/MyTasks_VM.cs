using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using GalaSoft.MvvmLight.Command;

namespace EmployeeManagement.ViewModel
{
    public class MyTasks_VM : BaseVM
    {
        public UserTask MyTask 
        {
            get => _task;
            set 
            { 
                _task = value;
                OnPropertyChanged("MyTask");
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

                    MainViewModel.GetInstance().SubmitTask(MyTask.Id, BranchName, Message);
                    Message = String.Empty;
                    BranchName = String.Empty;
                    MyTask = null;
                }));
            }
        }

        private string _branchName;
        private string _message;

        private RelayCommand _submitTaskCommand;
        
        private UserTask _task;

        public MyTasks_VM()
        {
            MyTask = new UserTask();
            Message = String.Empty;
            BranchName = String.Empty;
            MainViewModel.GetInstance().ServerClient.MyTask += SetUserTask;
        }

        private void SetUserTask(UserTask task) => MyTask = task;
    }
}
