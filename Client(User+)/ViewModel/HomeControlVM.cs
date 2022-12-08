using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_User__.ViewModel
{
    public class HomeControlVM : BaseVM
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
                if (CurrentViewModel is TaskCreateVM)
                    return;
                CurrentViewModel = _allVMs[0];
            });
        }

        public RelayCommand ShowDeleteMenu
        {
            get => _showDeleteMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is TaskDeleteVM)
                    return;
                CurrentViewModel = _allVMs[1];
            });
        }

        public RelayCommand ShowUpdateMenu
        {
            get => _showUpdateMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is TaskUpdateVM)
                    return;
                CurrentViewModel = _allVMs[2];
            });
        }

        public RelayCommand ShowSetTaskMenu
        {
            get => _showSetTaskMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is TaskSetVM)
                    return;
                CurrentViewModel = _allVMs[3];
            });
        }

        private List<BaseVM> _allVMs;

        private RelayCommand _showCreateMenu;
        private RelayCommand _showDeleteMenu;
        private RelayCommand _showUpdateMenu;
        private RelayCommand _showSetTaskMenu;
        
        public HomeControlVM()
        {
            _allVMs = new List<BaseVM>();
            CreateVMs();
        }

        private void CreateVMs()
        {
            while (true)
            {
                if (MainVM.GetInstance().User != null)
                    break;
            }
                
            _allVMs.Add(new TaskCreateVM());
            _allVMs.Add(new TaskDeleteVM());
            _allVMs.Add(new TaskUpdateVM());
            _allVMs.Add(new TaskSetVM());
            SendQuerrys();
        }

        private async void SendQuerrys()
        {
            MainVM.GetInstance().ServerClient.SendQuerryForImportance();
            await Task.Delay(2000);
            MainVM.GetInstance().ServerClient.SendQuerryForConditions();
            await Task.Delay(2000);
            MainVM.GetInstance().ServerClient.SendQuerryForEmployees();
            await Task.Delay(2000);
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();
            await Task.Delay(2000);
            MainVM.GetInstance().ServerClient.SendQuerryForAllTasks();
        }
    }
}
