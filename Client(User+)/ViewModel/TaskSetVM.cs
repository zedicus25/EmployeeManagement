﻿using Client_User__.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_User__.ViewModel
{
    public class TaskSetVM :BaseVM
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
                if(SelectedTask != null)
                {
                    if (SelectedTask.EmployeeId != 0)
                        SelectedEmployee = Employees.FirstOrDefault(x => x.Id == SelectedTask.EmployeeId);
                    else
                        SelectedEmployee = null;
                }
            }
        }
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged("Employees");
            }
        }
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        private RelayCommand _setTaskCommand;

        public RelayCommand SetTaskCommand
        {
            get { return _setTaskCommand ?? new RelayCommand(SetTask); }
        }

        private ObservableCollection<Employee> _employees;
        private Employee _selectedEmployee;

        public TaskSetVM()
        {
            Employees = new ObservableCollection<Employee>();
            Tasks = new ObservableCollection<UserTask>();
            MainVM.GetInstance().ServerClient.GetAllTasks += GetAllTasks;
            MainVM.GetInstance().ServerClient.GetEmployees += GetEmployees;
            MainVM.GetInstance().ServerClient.SendQuerryForEmployees();
            MainVM.GetInstance().ServerClient.SendQuerryForAllTasks();
        }

        private void SetTask()
        {
            if (SelectedEmployee == null || SelectedTask == null)
                return;
            MainVM.GetInstance().ServerClient.QuerrySetTaskToEmployee(SelectedTask.Id, SelectedEmployee.Id);
        }

        private void GetEmployees(List<Employee> obj)
        {
            if (Employees.Count <= 0)
            {
                Employees = new ObservableCollection<Employee>(obj);
                return;
            }

            Employees.Union(obj);
        }

        private void GetAllTasks(List<UserTask> obj)
        {
            if (Tasks.Count <= 0)
            {
                Tasks = new ObservableCollection<UserTask>(obj);
                return;
            }

            Tasks.Union(obj);
        }
    }
}
