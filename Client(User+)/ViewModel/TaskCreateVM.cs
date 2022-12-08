using Client_User__.Model;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_User__.ViewModel
{
    public class TaskCreateVM : BaseVM
    {
        public ObservableCollection<TaskImportant> Importances
        {
            get => _importances;
            set
            {
                _importances = value;
                OnPropertyChanged("Importances");
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
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        private RelayCommand _createTask;

        public RelayCommand CreateTaskCommand
        {
            get { return _createTask ?? new RelayCommand(CreateTask); }
        }
        public bool CanAddTasks
        {
            get => _canAddTasks;
            set
            {
                _canAddTasks = value;
                OnPropertyChanged("CanAddTasks");
            }
        }
        private bool _canAddTasks;


        private ObservableCollection<TaskImportant> _importances;
        private ObservableCollection<Employee> _employees;
        private ObservableCollection<UserProject> _projects;

        private UserTask _newTask;
        private DateTime _toCompleteDate;

        private UserProject _selectedProject;
        private TaskImportant _selectedImportance;
        private Employee _selectedEmployee;

        


        public TaskCreateVM()
        {
            _canAddTasks = false;
            NewTask = new UserTask()
            {
                Description = String.Empty,
                Title = String.Empty
            };
            AddListeners();
            SelectedEmployee = new Employee(); ;
            SelectedImportance = new TaskImportant();
            SelectedProject = new UserProject();
            ToCompleteDate = DateTime.Now;
            Employees = new ObservableCollection<Employee>();
            Importances = new ObservableCollection<TaskImportant>();
            Projects = new ObservableCollection<UserProject>();
        }

        private async void CreateTask()
        {
            if (NewTask.Title.Equals(String.Empty) || NewTask.Description.Equals(String.Empty) || ToCompleteDate == null
                 || SelectedImportance == null || SelectedProject == null)
                return;
            if (ToCompleteDate < DateTime.Now)
                return;
            NewTask.ToComplete = ToCompleteDate;
            NewTask.CreationDate = DateTime.Now;
            NewTask.ProjectId = SelectedProject.Id;
            NewTask.ConditionId = 4;
            NewTask.ImportanceId = SelectedImportance.Id;
            if (SelectedEmployee != null)
            {
                NewTask.EmployeeId = SelectedEmployee.Id;
                NewTask.ConditionId = 3;
            }
            MainVM.GetInstance().ServerClient.SendMessageToServer($"--createTask\n{JsonConvert.SerializeObject(NewTask  )}");
            ToCompleteDate = DateTime.Now;
            SelectedEmployee = null;
            SelectedImportance = null;
            SelectedProject = null;
            NewTask = new UserTask();
            await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForAllTasks();
        }
            
        private void AddListeners()
        {
            if (MainVM.GetInstance().ServerClient == null)
                return;

            MainVM.GetInstance().ServerClient.GetTaskImportants += GetTaskImportants;
            MainVM.GetInstance().ServerClient.GetEmployees += GetEmployees;
            MainVM.GetInstance().ServerClient.GetProjects += GetProjects;
            MainVM.GetInstance().ServerClient.GetAllTasks += GetAllTasks; ;
        }

        private void GetAllTasks(List<UserTask> obj) => CanAddTasks = true;
        

        private void GetProjects(List<UserProject> obj)
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
