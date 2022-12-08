using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.EmployeeWindows
{
    public class EmployeeWindowVM : BaseVM
    {
        private BaseVM _currentViewModel;

        public BaseVM CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        public RelayCommand ShowCreateMenu
        {
            get => _showCreateMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is CreateEmployeeVM)
                    return;
                CurrentViewModel = _allVMs[0];
            });
        }

        public RelayCommand ShowDeleteMenu
        {
            get => _showDeleteMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is DeleteEmployeeVM)
                    return;
                CurrentViewModel = _allVMs[1];
                MainVM.GetInstance().ServerClient.SendQuerryForEmployees();
            });
        }

        public RelayCommand ShowUpdateMenu
        {
            get => _showUpdateMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is UpdateEmployeeVM)
                    return;
                CurrentViewModel = _allVMs[2];
            });
        }
        private List<BaseVM> _allVMs;

        private RelayCommand _showCreateMenu;
        private RelayCommand _showDeleteMenu;
        private RelayCommand _showUpdateMenu;

        public EmployeeWindowVM()
        {
            _allVMs = new List<BaseVM>();
            CreateVMs();
        }

        private  void CreateVMs()
        {
            while (true)
            {
                if (MainVM.GetInstance().User != null)
                    break;
            }
                
            _allVMs.Add(new CreateEmployeeVM());
            _allVMs.Add(new DeleteEmployeeVM());
            _allVMs.Add(new UpdateEmployeeVM());
            SendQuerrys();
        }

        private async void SendQuerrys()
        {
            MainVM.GetInstance().ServerClient.SendQuerryForEmployeeRoles();
            await Task.Delay(3000);
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();
            await Task.Delay(3000);
            MainVM.GetInstance().ServerClient.SendQuerryForEmployees();
            await Task.Delay(3000);
            MainVM.GetInstance().ServerClient.SendQuerryForAllEmployees();
        }

    }
}
