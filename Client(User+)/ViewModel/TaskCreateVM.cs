using Client_User__.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_User__.ViewModel
{
    public class TaskCreateVM : BaseVM
    {
        public List<TaskImportant> Importances
        {
            get => _importances;
            set
            {
                _importances = value;
                OnPropertyChanged("Importances");
            }
        }
        public List<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged("Employees");
            }
        }
        public List<UserProject> Projects
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
                OnPropertyChanged("SelectedCondition");
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


        private List<TaskImportant> _importances;
        private List<Employee> _employees;
        private List<UserProject> _projects;

        private UserTask _newTask;
        private DateTime _toCompleteDate;
        private DateTime _toCompleteTime;

        private UserProject _selectedProject;
        private TaskImportant _selectedImportance;
        private Employee _selectedEmployee;


        public TaskCreateVM()
        {
            NewTask = new UserTask();
            SendQuerrys();
            ToCompleteDate = DateTime.Now;
            Employees = new List<Employee>();
            Importances = new List<TaskImportant>();
            Projects = new List<UserProject>();
            AddListeners();
        }

        private void CreateTask()
        {
            if (NewTask.Title.Equals(String.Empty) || NewTask.Description.Equals(String.Empty) || ToCompleteDate == null
                || SelectedEmployee == null || SelectedImportance == null 
                || SelectedProject == null)
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
            MainVM.GetInstance().CreateTask(NewTask);
            ToCompleteDate = DateTime.Now;
            SelectedEmployee = null;
            SelectedImportance = null;
            SelectedProject = null;
            NewTask = new UserTask();
        }

        private void AddListeners()
        {
            if (MainVM.GetInstance().ServerClient == null)
                return;

            MainVM.GetInstance().ServerClient.GetTaskImportants += GetTaskImportants;
            MainVM.GetInstance().ServerClient.GetEmployees += GetEmployees;
            MainVM.GetInstance().ServerClient.GetProjects += GetProjects; ;
        }

        private void GetProjects(IEnumerable<UserProject> obj)
        {
            Projects.Clear();
            foreach (var item in obj)
            {
                Projects.Add(item);
            }
        }

        private void GetEmployees(IEnumerable<Employee> obj)
        {
            Employees.Clear();
            foreach (var item in obj)
            {
                Employees.Add(item);
            }
        }


        private void GetTaskImportants(IEnumerable<TaskImportant> obj)
        {
            Importances.Clear();
            foreach (var item in obj)
            {
                Importances.Add(item);
            }
        }

        private async void SendQuerrys()
        {
            MainVM.GetInstance().ServerClient.SendQuerryForImportance();
            await Task.Delay(2000);
            MainVM.GetInstance().ServerClient.SendQuerryForEmployees();
            await Task.Delay(2000);
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();
        }
    }
}
