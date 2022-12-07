using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.EmployeeWindows
{
    public class UpdateEmployeeVM : BaseVM
    {
        private List<Employee> _employees;

        public List<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged("Employees");
            }
        }

        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
                if(SelectedEmployee != null)
                {
                    SelectedProject = Projects.FirstOrDefault(x => x.Id == _selectedEmployee.ProjectId);
                    SelectedRole = Roles.FirstOrDefault(x => x.Id == _selectedEmployee.EmployeeRoleId);
                }
            }
               
        }
        private ObservableCollection<EmployeeRole> _roles;

        public ObservableCollection<EmployeeRole> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                OnPropertyChanged("Roles");
            }
        }
        private EmployeeRole _selectedRole;

        private string _selectedEmail;

        public string SelectedEmail
        {
            get { return _selectedEmail; }
            set 
            { 
                _selectedEmail = value;
                OnPropertyChanged("SelectedEmail");
            }
        }
        private string _selectedPhone;

        public string SelectedPhone
        {
            get { return _selectedPhone; }
            set
            {
                _selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }


        public EmployeeRole SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                OnPropertyChanged("SelectedRole");
            }
        }
        private Project _selectedProject;

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }
        private ObservableCollection<Project> _projects;

        public ObservableCollection<Project> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                OnPropertyChanged("Projects");
            }
        }
        private RelayCommand _updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return _updateCommand ?? new RelayCommand(UpdateUser); }
        }
        private RelayCommand _deleteEmailCommand;

        public RelayCommand DeleteEmail
        {
            get { return _deleteEmailCommand ?? new RelayCommand(() =>
            {
                if (SelectedEmail == null || !SelectedEmployee.Emails.Contains(SelectedEmail))
                    return;
                SelectedEmployee.Emails.Remove(SelectedEmail);
                SelectedEmail = null;
            }); }
        }
        private RelayCommand _deletePhoneCommand;

        public RelayCommand DeleteNumber
        {
            get
            {
                return _deletePhoneCommand ?? new RelayCommand(() =>
                {
                    if (SelectedPhone == null || !SelectedEmployee.PhoneNumbers.Contains(SelectedPhone))
                        return;
                    SelectedEmployee.PhoneNumbers.Remove(SelectedPhone);
                    SelectedPhone = null;
                });
            }
        }
        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set 
            { 
                _newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }



        public UpdateEmployeeVM()
        {
            Projects = new ObservableCollection<Project>();
            Roles = new ObservableCollection<EmployeeRole>();
            Employees = new List<Employee>();
            MainVM.GetInstance().ServerClient.GetAllEmployees += GetEmployees;
            MainVM.GetInstance().ServerClient.GetEmployeeRoles += GetEmployeeRoles;
            MainVM.GetInstance().ServerClient.GetProjects += GetProjects;
            SendQuerrys();
        }

        private async void SendQuerrys()
        {
            await Task.Delay(5000);
            MainVM.GetInstance().ServerClient.SendQuerryForEmployeeRoles();
            await Task.Delay(8000);
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();
            await Task.Delay(8000);
            MainVM.GetInstance().ServerClient.SendQuerryForAllEmployees();
            await Task.Delay(5000);
        }

        private void UpdateUser()
        {
            if (SelectedEmployee == null || SelectedProject == null || SelectedRole == null)
                return;
            if (NewPassword != String.Empty)
                SelectedEmployee.Password = NewPassword;
            SelectedEmployee.EmployeeRoleId = SelectedRole.Id;
            SelectedEmployee.ProjectId = SelectedProject.Id;
            MainVM.GetInstance().ServerClient.
                SendMessageToServer($"--updateEmployee\nempId={SelectedEmployee.Id}\nnewEmp={JsonConvert.SerializeObject(SelectedEmployee)}\n");
            SelectedEmployee = null;
            SelectedEmail = null;
            SelectedPhone = null;
            SelectedProject = null;
            SelectedRole = null;
        }

        private void GetEmployees(IEnumerable<Employee> obj)
        {
            Employees.Clear();
            foreach (var item in obj)
            {
                Employees.Add(item);
            }
        }
        private void GetProjects(IEnumerable<Project> obj)
        {
            Projects.Clear();
            foreach (var item in obj)
            {
                Projects.Add(item);
            }
        }

        private void GetEmployeeRoles(IEnumerable<EmployeeRole> obj)
        {
            Roles.Clear();
            foreach (var item in obj)
            {
                Roles.Add(item);
            }
        }
    }
}
