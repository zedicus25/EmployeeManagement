﻿using EmployeeManagement.Model;
using GalaSoft.MvvmLight.Command;
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


        public RelayCommand GetTask
        {
            get
            {
                return _getTaskCommand ?? (_getTaskCommand = new RelayCommand(() =>
                {

                }));
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

        private RelayCommand _getTaskCommand;
        public AllTask_VM()
        {
            MainViewModel.Instance.ServerClient.AllTasks += GetTasks;
            MainViewModel.Instance.ServerClient.GetAllTasks();
        }

        

        private void GetTasks(List<ProjectTask> tasks) => _tasks = new ObservableCollection<ProjectTask>(tasks);
    }
}
