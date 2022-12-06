using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            set { _selectedRole = value; }
        }

        private List<UserRole> _roles;

        public List<UserRole> Roles
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



        public CreateEmployeeRoleVM()
        {
            NewRole = new EmployeeRole();
            Roles = new List<UserRole>();
            MainVM.GetInstance().ServerClient.GetUserRoles += GetUserRoles;
            MainVM.GetInstance().ServerClient.SendQuerryForUserRoles();
        }

        private void GetUserRoles(IEnumerable<UserRole> obj)
        {
            Roles.Clear();
            foreach (var item in obj)
            {
                Roles.Add(item);
            }
        }

        private void AddRole()
        {
            if (SelectedRole == null || NewRole.Title == String.Empty || NewRole.Description == String.Empty)
                return;
            NewRole.UserRoleId = SelectedRole.Id;
            MainVM.GetInstance().ServerClient.
                SendMessageToServer($"--createEmployeeRole\nrole={JsonConvert.SerializeObject(NewRole)}\n");
            NewRole = new EmployeeRole();
        }
    }
}
