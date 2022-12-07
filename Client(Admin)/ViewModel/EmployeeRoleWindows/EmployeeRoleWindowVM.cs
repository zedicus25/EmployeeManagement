using Client_Admin_.ViewModel.EmployeeWindows;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.EmployeeRoleWindows
{
    public class EmployeeRoleWindowVM : BaseVM
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
                if (CurrentViewModel is CreateEmployeeRoleVM || _allVMs.Count < 1)
                    return;
                CurrentViewModel = _allVMs[0];
            });
        }

        public RelayCommand ShowDeleteMenu
        {
            get => _showDeleteMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is DeleteEmployeeRoleVM || _allVMs.Count < 2)
                    return;
                CurrentViewModel = _allVMs[1];
            });
        }

        public RelayCommand ShowUpdateMenu
        {
            get => _showUpdateMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is UpdateEmployeeRoleVM || _allVMs.Count < 3)
                    return;
                CurrentViewModel = _allVMs[2];
            });
        }
        public RelayCommand ShowLinkMenu
        {
            get => _showLinkMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is LinkEmployeeRoleVM || _allVMs.Count < 4)
                    return;
                CurrentViewModel = _allVMs[3];
            });
        }
        private List<BaseVM> _allVMs;

        private RelayCommand _showCreateMenu;
        private RelayCommand _showDeleteMenu;
        private RelayCommand _showUpdateMenu;
        private RelayCommand _showLinkMenu;

        public EmployeeRoleWindowVM()
        {
            _allVMs = new List<BaseVM>();
            CreateVMs();
        }

        private async void CreateVMs()
        {
            while (true)
            {
                if (MainVM.GetInstance().User != null)
                    break;
            }

            _allVMs.Add(new CreateEmployeeRoleVM());
            await Task.Delay(3000);
            _allVMs.Add(new DeleteEmployeeRoleVM());
            await Task.Delay(3000);
            _allVMs.Add(new UpdateEmployeeRoleVM());
            await Task.Delay(3000);
            _allVMs.Add(new LinkEmployeeRoleVM());
        }
    }
}
