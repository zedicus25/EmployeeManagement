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
    public class TaskUpdateVM :BaseVM
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
                    SelectedImportance = Importances.FirstOrDefault(x => x.Id == SelectedTask.ImportanceId);
                    SelectedCondition = Conditions.FirstOrDefault(x => x.Id == SelectedTask.ConditionId);
                    if (SelectedTask.EmployeeId != 0)
                        SelectedEmployee = Employees.FirstOrDefault(x => x.Id == SelectedTask.EmployeeId);
                    else
                    {
                        SelectedEmployee = null;
                    }
                        

                    SelectedProject = Projects.FirstOrDefault(x => x.Id == SelectedTask.ProjectId);
                }
            }
        }

        public ObservableCollection<TaskImportant> Importances
        {
            get => _importances;
            set
            {
                _importances = value;
                OnPropertyChanged("Importances");
            }
        }
        public ObservableCollection<TaskCondition> Conditions
        {
            get => _conditions;
            set
            {
                _conditions = value;
                OnPropertyChanged("Conditions");
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
        public ObservableCollection<UserProject> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                OnPropertyChanged("Projects");
            }
        }
        public DateTime ToCompleteDate
        {
            get => _toCompleteDate;
            set
            {
                _toCompleteDate = value;
                OnPropertyChanged("ToCompleteDate");
            }
        }


        public UserProject SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }

        public TaskImportant SelectedImportance
        {
            get { return _selectedImportance; }
            set
            {
                _selectedImportance = value;
                OnPropertyChanged("SelectedImportance");
            }
        }
        public TaskCondition SelectedCondition
        {
            get { return _selectedCondition; }
            set
            {
                _selectedCondition = value;
                OnPropertyChanged("SelectedCondition");
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

        

        public RelayCommand UpdateCommand
        {
            get { return _updateCommand ?? new RelayCommand(UpdateTask); }
        }
        public bool CanUpdateTasks
        {
            get => _canUpdate;
            set
            {
                _canUpdate = value;
                OnPropertyChanged("CanUpdateTasks");
            }
        }
        private bool _canUpdate;

        private ObservableCollection<TaskImportant> _importances;
        private ObservableCollection<TaskCondition> _conditions;
        private ObservableCollection<Employee> _employees;
        private ObservableCollection<UserProject> _projects;

        private DateTime _toCompleteDate;

        private UserProject _selectedProject;
        private TaskImportant _selectedImportance;
        private TaskCondition _selectedCondition;
        private Employee _selectedEmployee;
        private RelayCommand _updateCommand;
        public TaskUpdateVM()
        {
            CanUpdateTasks = false;
            AddListeners();
            Tasks = new ObservableCollection<UserTask>();
            Employees = new ObservableCollection<Employee>();
            Importances = new ObservableCollection<TaskImportant>();
            Projects = new ObservableCollection<UserProject>();
            Conditions = new ObservableCollection<TaskCondition>();
            SelectedTask = new UserTask();
        }
        private void AddListeners()
        {
            if (MainVM.GetInstance().ServerClient == null)
                return;

            MainVM.GetInstance().ServerClient.GetTaskImportants += GetTaskImportants;
            MainVM.GetInstance().ServerClient.GetAllTasks += GetAllTasks;
            MainVM.GetInstance().ServerClient.GetTaskConditions += GetTaskConditions;
            MainVM.GetInstance().ServerClient.GetEmployees += GetEmployees;
            MainVM.GetInstance().ServerClient.GetProjects += GetProjects;
        }

        private void GetAllTasks(List<UserTask> obj)
        {
            CanUpdateTasks = true;
            Tasks = new ObservableCollection<UserTask>(obj);
            OnPropertyChanged("Tasks");
        }

        private void GetTaskConditions(List<TaskCondition> obj)
        {
            Conditions = new ObservableCollection<TaskCondition>(obj);
            OnPropertyChanged("Conditions");
        }

        private async void UpdateTask()
        {
            if (SelectedTask.Title.Equals(String.Empty) || SelectedTask.Description.Equals(String.Empty) || ToCompleteDate == null
                || SelectedImportance == null || SelectedProject == null)
                return;
            if (SelectedTask.ToComplete < DateTime.Now)
                return;

            SelectedTask.ProjectId = SelectedProject.Id;
            SelectedTask.ConditionId = SelectedCondition.Id;
            SelectedTask.ImportanceId = SelectedImportance.Id;
            if(SelectedEmployee != null)
            {
                SelectedTask.EmployeeId = SelectedEmployee.Id;
            }
            else
            {
                SelectedTask.EmployeeId = 0;
            }
                
            MainVM.GetInstance().ServerClient.UpdateTask(SelectedTask.Id,SelectedTask);
            CanUpdateTasks = false; 
            await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForAllTasks();
        }

        private void GetProjects(IEnumerable<UserProject> obj)
        {
            Projects = new ObservableCollection<UserProject>(obj);
            OnPropertyChanged("Projects");
        }

        private void GetEmployees(List<Employee> obj)
        {
            Employees = new ObservableCollection<Employee>(obj);
            OnPropertyChanged("Employees");
        }


        private void GetTaskImportants(List<TaskImportant> obj)
        {
            Importances = new ObservableCollection<TaskImportant>(obj);
            OnPropertyChanged("Importances");
        }
    }
}
