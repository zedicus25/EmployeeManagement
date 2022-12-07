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
        public UserTask NewTask
        {
            get => _newTask;
            set
            {
                _newTask = value;
                OnPropertyChanged("NewTask");
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

        private ObservableCollection<TaskImportant> _importances;
        private ObservableCollection<TaskCondition> _conditions;
        private ObservableCollection<Employee> _employees;
        private ObservableCollection<UserProject> _projects;

        private UserTask _newTask;
        private DateTime _toCompleteDate;

        private UserProject _selectedProject;
        private TaskImportant _selectedImportance;
        private TaskCondition _selectedCondition;
        private Employee _selectedEmployee;
        private RelayCommand _updateCommand;
        public TaskUpdateVM()
        {
            AddListeners();
            SendQuerrys();
            Tasks = new ObservableCollection<UserTask>();
            Employees = new ObservableCollection<Employee>();
            Importances = new ObservableCollection<TaskImportant>();
            Projects = new ObservableCollection<UserProject>();
            Conditions = new ObservableCollection<TaskCondition>();
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
            MainVM.GetInstance().ServerClient.GetTaskById += GetTaskById;
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

        private void GetTaskConditions(List<TaskCondition> obj)
        {
            if (Conditions.Count <= 0)
            {
                Conditions = new ObservableCollection<TaskCondition>(obj);
                return;
            }

            Conditions.Union(obj);
        }

        private void GetTaskById(UserTask obj)
        {
            NewTask.Id = obj.Id;
            NewTask.Title = obj.Title;
            NewTask.Description = obj.Description;
            ToCompleteDate = obj.ToComplete;
            if (obj.EmployeeId != 0)
                SelectedEmployee = Employees.FirstOrDefault(x => x.Id == obj.EmployeeId);
            SelectedProject = Projects.FirstOrDefault(x => x.Id == obj.ProjectId);
            SelectedImportance = Importances.FirstOrDefault(x => x.Id == obj.ImportanceId);
            SelectedCondition = Conditions.FirstOrDefault(x => x.Id == obj.ConditionId);
        }

        private void UpdateTask()
        {
            if (NewTask.Title.Equals(String.Empty) || NewTask.Description.Equals(String.Empty) || ToCompleteDate == null
                || SelectedImportance == null || SelectedProject == null)
                return;
           

            NewTask.ToComplete = ToCompleteDate;
            NewTask.ProjectId = SelectedProject.Id;
            NewTask.ConditionId = SelectedCondition.Id;
            NewTask.ImportanceId = SelectedImportance.Id;
            if (NewTask.ConditionId == 3)
            {
                NewTask.EmployeeId = SelectedEmployee.Id;
            }

            MainVM.GetInstance().ServerClient.UpdateTask(NewTask.Id,NewTask);
        }

        private void GetProjects(IEnumerable<UserProject> obj)
        {
            if (Projects.Count <= 0)
            {
                Projects = new ObservableCollection<UserProject>(obj);
                return;
            }

            Projects.Union(obj);
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


        private void GetTaskImportants(List<TaskImportant> obj)
        {
            if (Importances.Count <= 0)
            {
                Importances = new ObservableCollection<TaskImportant>(obj);
                return;
            }

            Importances.Union(obj);
        }

        private async void SendQuerrys()
        {
            MainVM.GetInstance().ServerClient.SendQuerryForImportance();
            await Task.Delay(2000);
            MainVM.GetInstance().ServerClient.SendQuerryForConditions();
            await Task.Delay(2000);
            MainVM.GetInstance().ServerClient.SendQuerryForEmployees();
            await Task.Delay(2000);
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();
            await Task.Delay(2000);
            MainVM.GetInstance().ServerClient.SendQuerryForAllTasks();
        }
    }
}
