using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.EmployeeRoleWindows
{
    public class UpdateEmployeeRoleVM : BaseVM
    {
        private EmployeeRole _role;

        public EmployeeRole SelectedEmployeeRole
        {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged("SelectedEmployeeRole");
            }
        }
        private List<EmployeeRole> _employeeRoles;

        public List<EmployeeRole> EmployeeRoles
        {
            get { return _employeeRoles; }
            set
            {
                _employeeRoles = value;
                OnPropertyChanged("EmployeeRoles");
            }
        }

        private UserRole _selectedRole;

        public UserRole SelectedUserRole
        {
            get { return _selectedRole; }
            set 
            { 
                _selectedRole = value;
                OnPropertyChanged("SelectedUserRole");
            }
        }

        private List<UserRole> _roles;

        public List<UserRole> UserRoles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                OnPropertyChanged("Roles");
            }
        }

        private RelayCommand _updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return _updateCommand ?? new RelayCommand(UpdateRole); }
        }

        public UpdateEmployeeRoleVM()
        {

            UserRoles = new List<UserRole>();
            EmployeeRoles = new List<EmployeeRole>();
            SelectedEmployeeRole = new EmployeeRole();
            SelectedUserRole = new UserRole();
            MainVM.GetInstance().ServerClient.GetUserRoles += GetUserRoles;
            MainVM.GetInstance().ServerClient.GetEmployeeRoles += GetEmployeeRoles;
            MainVM.GetInstance().ServerClient.SendQuerryForUserRoles();
            MainVM.GetInstance().ServerClient.SendQuerryForEmployeeRoles();
        }

        private void UpdateRole()
        {
            if (SelectedEmployeeRole == null || SelectedUserRole == null)
                return;

            EmployeeRole newRole = new EmployeeRole()
            {
                Title = SelectedEmployeeRole.Title,
                Description = SelectedEmployeeRole.Description,
                UserRoleId = SelectedUserRole.Id
            };
            MainVM.GetInstance().ServerClient.UpdateEmployeeRole(SelectedEmployeeRole.Id, newRole);
        }

        private void GetUserRoles(IEnumerable<UserRole> obj)
        {
            UserRoles.Clear();
            foreach (var item in obj)
            {
                UserRoles.Add(item);
            }
        }
        private void GetEmployeeRoles(IEnumerable<EmployeeRole> obj)
        {
            EmployeeRoles.Clear();
            foreach (var item in obj)
            {
                EmployeeRoles.Add(item);
            }
        }
    }
}
