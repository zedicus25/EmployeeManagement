using Client_Admin_.ViewModel.ProjectWindows;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.TaskConditionWindows
{
    public class TaskConditionVM : BaseVM
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
                if (CurrentViewModel is CreateTaskConditionVM || _allVMs.Count < 1)
                    return;
                CurrentViewModel = _allVMs[0];
            });
        }

        public RelayCommand ShowDeleteMenu
        {
            get => _showDeleteMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is DeleteTaskConditionVM || _allVMs.Count < 2)
                    return;
                CurrentViewModel = _allVMs[1];
            });
        }

        public RelayCommand ShowUpdateMenu
        {
            get => _showUpdateMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is UpdateTaskConditionVM || _allVMs.Count < 3)
                    return;
                CurrentViewModel = _allVMs[2];
            });
        }
        private List<BaseVM> _allVMs;

        private RelayCommand _showCreateMenu;
        private RelayCommand _showDeleteMenu;
        private RelayCommand _showUpdateMenu;

        public TaskConditionVM()
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

            _allVMs.Add(new CreateTaskConditionVM());
            await Task.Delay(3000);
            _allVMs.Add(new DeleteTaskConditionVM());
            await Task.Delay(3000);
            _allVMs.Add(new UpdateTaskConditionVM());
        }
    }
}
