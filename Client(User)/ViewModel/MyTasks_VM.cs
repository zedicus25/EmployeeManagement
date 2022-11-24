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

        
        public RelayCommand SubmitTask
        {
            get
            {
                return _submitTaskCommand ?? (_submitTaskCommand = new RelayCommand(() =>
                {
                    
                }));
            }
        }

        private RelayCommand _submitTaskCommand;
        
        private UserTask _task;

        public MyTasks_VM()
        {
            MyTask = new UserTask();
            MainViewModel.GetInstance().ServerClient.MyTask += SetUserTask;
        }

        private void SetUserTask(UserTask task) => MyTask = task;
    }
}
