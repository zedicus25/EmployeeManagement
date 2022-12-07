using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.EmployeeRoleWindows
{
    public class LinkEmployeeRoleVM : BaseVM
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
                OnPropertyChanged("UserRoles");
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

        private RelayCommand _linkCommand;

        public RelayCommand LinkCommand
        {
            get { return _linkCommand ?? new RelayCommand(LinkRole); }
        }

        public LinkEmployeeRoleVM()
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

        private void LinkRole()
        {
            if (SelectedEmployeeRole == null || SelectedUserRole == null)
                return;
            MainVM.GetInstance().ServerClient.SetUserRoleForEmployeeRole(SelectedUserRole.Id, SelectedEmployeeRole.Id);
            SelectedEmployeeRole = null;
            SelectedUserRole = null;
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
