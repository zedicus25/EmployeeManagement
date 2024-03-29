﻿using Client_Admin_.ViewModel.EmployeeWindows;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.ProjectWindows
{
    public class ProjectWindowVM : BaseVM
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
                if (CurrentViewModel is CreateProjectVM)
                    return;
                CurrentViewModel = _allVMs[0];
            });
        }

        public RelayCommand ShowDeleteMenu
        {
            get => _showDeleteMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is DeleteProjectVM)
                    return;
                CurrentViewModel = _allVMs[1];
            });
        }

        public RelayCommand ShowUpdateMenu
        {
            get => _showUpdateMenu ?? new RelayCommand(() =>
            {
                if (CurrentViewModel is UpdateProjectVM)
                    return;
                CurrentViewModel = _allVMs[2];
            });
        }
        private List<BaseVM> _allVMs;

        private RelayCommand _showCreateMenu;
        private RelayCommand _showDeleteMenu;
        private RelayCommand _showUpdateMenu;

        public ProjectWindowVM()
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

            _allVMs.Add(new CreateProjectVM());
            _allVMs.Add(new DeleteProjectVM());
            _allVMs.Add(new UpdateProjectVM());
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();
        }
    }
}
