using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                if(SelectedEmployeeRole != null)
                {
                    SelectedUserRole = UserRoles.FirstOrDefault(x => x.Id == SelectedEmployeeRole.UserRoleId);
                }
            }
        }
        private ObservableCollection<EmployeeRole> _employeeRoles;

        public ObservableCollection<EmployeeRole> EmployeeRoles
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

        private ObservableCollection<UserRole> _roles;

        public ObservableCollection<UserRole> UserRoles
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
        private bool _canUpdateRole;

        public bool CanUpdateRole
        {
            get { return _canUpdateRole; }
            set 
            { 
                _canUpdateRole = value;
                OnPropertyChanged("CanUpdateRole");
            }
        }


        public UpdateEmployeeRoleVM()
        {
            UserRoles = new ObservableCollection<UserRole>();
            EmployeeRoles = new ObservableCollection<EmployeeRole>();
            SelectedEmployeeRole = new EmployeeRole();
            SelectedUserRole = new UserRole();
            MainVM.GetInstance().ServerClient.GetUserRoles += GetUserRoles;
            MainVM.GetInstance().ServerClient.GetEmployeeRoles += GetEmployeeRoles;
        }

        private async void UpdateRole()
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
            CanUpdateRole = false;
            SelectedUserRole = null;
            SelectedEmployeeRole = null;
            await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForEmployeeRoles();
        }

        private void GetUserRoles(IEnumerable<UserRole> obj)
        {
            UserRoles = new ObservableCollection<UserRole>(obj);
            OnPropertyChanged("UserRoles");
        }
        private void GetEmployeeRoles(IEnumerable<EmployeeRole> obj)
        {
            CanUpdateRole = true;
            EmployeeRoles = new ObservableCollection<EmployeeRole>(obj);
            OnPropertyChanged("EmployeeRoles");
        }
    }
}
