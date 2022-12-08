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
        private ObservableCollection<UserRole> _roles;

        public ObservableCollection<UserRole> UserRoles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                OnPropertyChanged("UserRoles");
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

        private RelayCommand _linkCommand;

        public RelayCommand LinkCommand
        {
            get { return _linkCommand ?? new RelayCommand(LinkRole); }
        }

        private bool _canLinkRole;

        public bool CanLinkRole
        {
            get { return _canLinkRole; }
            set 
            { 
                _canLinkRole = value;
                OnPropertyChanged("CanLinkRole");
            }
        }

        public LinkEmployeeRoleVM()
        {
            CanLinkRole = false;
            UserRoles = new ObservableCollection<UserRole>();
            EmployeeRoles = new ObservableCollection<EmployeeRole>();
            SelectedEmployeeRole = new EmployeeRole();
            SelectedUserRole = new UserRole();
            MainVM.GetInstance().ServerClient.GetUserRoles += GetUserRoles;
            MainVM.GetInstance().ServerClient.GetEmployeeRoles += GetEmployeeRoles;
        }

        private async void LinkRole()
        {
            if (SelectedEmployeeRole == null || SelectedUserRole == null)
                return;
            MainVM.GetInstance().ServerClient.SetUserRoleForEmployeeRole(SelectedUserRole.Id, SelectedEmployeeRole.Id);
            CanLinkRole = false;
            SelectedEmployeeRole = null;
            SelectedUserRole = null;
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
            CanLinkRole = true;
            EmployeeRoles = new ObservableCollection<EmployeeRole>(obj);
            OnPropertyChanged("EmployeeRoles");
        }
    }
}
