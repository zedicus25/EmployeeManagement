using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.EmployeeRoleWindows
{
    public class CreateEmployeeRoleVM : BaseVM
    {
        private EmployeeRole _role;

        public EmployeeRole NewRole
        {
            get { return _role; }
            set 
            { 
                _role = value;
                OnPropertyChanged("NewRole");
            }
        }

        private UserRole _selectedRole;

        public UserRole SelectedRole
        {
            get { return _selectedRole; }
            set 
            { 
                _selectedRole = value;
                OnPropertyChanged("SelectedRole");
            }
        }

        private ObservableCollection<UserRole> _roles;

        public ObservableCollection<UserRole> Roles
        {
            get { return _roles; }
            set 
            { 
                _roles = value;
                OnPropertyChanged("Roles");
            }
        }

        private RelayCommand _addCommand;

        public RelayCommand AddCommand
        {
            get { return _addCommand ?? new RelayCommand(AddRole); }
        }

        private bool _canAddRole;

        public bool CanAddRole
        {
            get { return _canAddRole; }
            set 
            { 
                _canAddRole = value;
                OnPropertyChanged("CanAddRole");
            }
        }

        public CreateEmployeeRoleVM()
        {
            CanAddRole = false;
            NewRole = new EmployeeRole();
            Roles = new ObservableCollection<UserRole>();
            MainVM.GetInstance().ServerClient.GetUserRoles += GetUserRoles;
            MainVM.GetInstance().ServerClient.GetEmployeeRoles += GetEmployeeRoles;

        }

        private void GetEmployeeRoles(IEnumerable<EmployeeRole> obj) => CanAddRole = true;


        private void GetUserRoles(IEnumerable<UserRole> obj)
        {
            Roles = new ObservableCollection<UserRole>(obj);
            OnPropertyChanged("Roles");
        }

        private async void AddRole()
        {
            if (SelectedRole == null || NewRole.Title == String.Empty || NewRole.Description == String.Empty)
                return;
            NewRole.UserRoleId = SelectedRole.Id;
            MainVM.GetInstance().ServerClient.
                SendMessageToServer($"--createEmployeeRole\nrole={JsonConvert.SerializeObject(NewRole)}\n");
            CanAddRole = false;
            SelectedRole = null;
            NewRole = new EmployeeRole();
            await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForEmployeeRoles();
        }
    }
}
