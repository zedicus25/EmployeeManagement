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
        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? new RelayCommand(RemoveRole); }
        }

        private bool _canRemoveRole;

        public bool CanRemoveRole
        {
            get { return _canRemoveRole; }
            set 
            { 
                _canRemoveRole = value;
                OnPropertyChanged("CanRemoveRole");
            }
        }


        public DeleteEmployeeRoleVM()
        {
            CanRemoveRole = false;
            EmployeeRoles = new ObservableCollection<EmployeeRole>();
            SelectedEmployeeRole = new EmployeeRole();
            MainVM.GetInstance().ServerClient.GetEmployeeRoles += GetEmployeeRoles; 
        }

        private void GetEmployeeRoles(IEnumerable<EmployeeRole> obj)
        {
            CanRemoveRole = true;
            EmployeeRoles = new ObservableCollection<EmployeeRole>(obj);
            OnPropertyChanged("EmployeeRoles");
        }

        private async void RemoveRole()
        {
            if (SelectedEmployeeRole == null)
                return;

            MainVM.GetInstance().ServerClient.SendMessageToServer($"--removeEmployeeRole\nid={SelectedEmployeeRole.Id}");
            EmployeeRoles.Remove(SelectedEmployeeRole);
            CanRemoveRole = false;
            SelectedEmployeeRole = null;
            await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForEmployeeRoles();
        }
    }
}
