using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.EmployeeRoleWindows
{
    public class DeleteEmployeeRoleVM : BaseVM
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

        private UserRole _selectedRole;

      
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
        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? new RelayCommand(RemoveRole); }
        }

        public DeleteEmployeeRoleVM()
        {
            EmployeeRoles = new List<EmployeeRole>();
            SelectedEmployeeRole = new EmployeeRole();
            MainVM.GetInstance().ServerClient.GetEmployeeRoles += GetEmployeeRoles;
            MainVM.GetInstance().ServerClient.SendQuerryForEmployeeRoles();
        }

        private void GetEmployeeRoles(IEnumerable<EmployeeRole> obj)
        {
            EmployeeRoles.Clear();
            foreach (var item in obj)
            {
                EmployeeRoles.Add(item);
            }
        }

        private void RemoveRole()
        {
            if (SelectedEmployeeRole == null)
                return;

            MainVM.GetInstance().ServerClient.SendMessageToServer($"--removeEmployeeRole\nid={SelectedEmployeeRole.Id}");
            EmployeeRoles.Remove(SelectedEmployeeRole);
            SelectedEmployeeRole = null;
        }
    }
}
