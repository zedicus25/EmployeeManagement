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
    public class CreateEmployeeVM : BaseVM
    {
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

		private Employee _newEmployee;

		public Employee NewEmployee
		{
			get { return _newEmployee; }
			set 
			{ 
				_newEmployee = value; 
				OnPropertyChanged("NewEmployee");
			}
		}
		private string _phoneNumber;

		public string PhoneNumner
		{
			get { return _phoneNumber; }
			set 
			{ 
				_phoneNumber = value;
				OnPropertyChanged("PhoneNumner");
			}
		}
        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }


        private RelayCommand _addPhoneNumber;

		public RelayCommand AddPhoneNumber
		{
			get { return _addPhoneNumber ?? new RelayCommand(() =>
			{
				if (NewEmployee.PhoneNumbers == null)
					NewEmployee.PhoneNumbers = new List<string>();

				if (PhoneNumner.StartsWith("+"))
					NewEmployee.PhoneNumbers.Add(PhoneNumner);
				PhoneNumner = String.Empty;
			});}
		}
        private RelayCommand _addEmail;

        public RelayCommand AddEmail
        {
            get
            {
                return _addEmail ?? new RelayCommand(() =>
                {
                    if (NewEmployee.Emails == null)
                        NewEmployee.Emails = new List<string>();

					if(Email.Contains('@') )
						NewEmployee.Emails.Add(Email);

                    Email = String.Empty;
                });
            }
        }

		private RelayCommand _addCommand;

		public RelayCommand AddCommand
		{
			get { return _addCommand ?? new RelayCommand(AddUser); }
		}


		public CreateEmployeeVM()
		{
			NewEmployee = new Employee();
			Roles = new ObservableCollection<EmployeeRole>();
			Projects = new ObservableCollection<Project>();
			MainVM.GetInstance().ServerClient.GetEmployeeRoles += GetEmployeeRoles;
			MainVM.GetInstance().ServerClient.GetProjects += GetProjects;
			SendQuerrys();
			
		}
		private async void SendQuerrys()
		{
            MainVM.GetInstance().ServerClient.SendQuerryForEmployeeRoles();
			await Task.Delay(3000);
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();
        }
		private void AddUser()
		{
			if (SelectedRole == null || SelectedProject == null)
				return;
			NewEmployee.EmployeeRoleId = SelectedRole.Id;
			NewEmployee.ProjectId = SelectedProject.Id;
			if (NewEmployee.IsValid())
			{
				MainVM.GetInstance().ServerClient.
					SendMessageToServer($"--addNewEmployee\nemp={JsonConvert.SerializeObject(NewEmployee)}");
				NewEmployee = new Employee();
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
