﻿using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModel
{
    public class HomeWindow_VM : BaseVM
    {
        public BaseVM HomePageVM
        {
            get => _baseVM;
            set
            {
                _baseVM = value;
            }
        }

        public RelayCommand GoToAllTask
        {
            get
            {
                return _goToAllTasks ?? (_goToAllTasks = new RelayCommand(() =>
                {
                    if (_baseVM is AllTask_VM)
                        return;
                    SetCurrentVM(_allVMs[0]);
                }));
            }
        }

        public RelayCommand GoToMyTasks
        {
            get
            {
                return _goToMyTasks ?? (_goToMyTasks = new RelayCommand(() =>
                {
                    if (_baseVM is MyTasks_VM)
                        return;
                    SetCurrentVM(_allVMs[1]);
                }));
            }
        }

        public RelayCommand GoToAccount
        {
            get
            {
                return _goToAccount ?? (_goToAccount = new RelayCommand(() =>
                {
                    if (_baseVM is Account_VM)
                        return;
                    SetCurrentVM(_allVMs[2]);
                }));
            }
        }

        public RelayCommand GoToProject
        {
            get
            {
                return _goToProject ?? (_goToProject = new RelayCommand(() =>
                {
                    if (_baseVM is Project_VM)
                        return;
                    SetCurrentVM(_allVMs[3]);
                }));
            }
        }

        private RelayCommand _goToAllTasks;
        private RelayCommand _goToMyTasks;
        private RelayCommand _goToAccount;
        private RelayCommand _goToProject;

        private BaseVM _baseVM;
        private List<BaseVM> _allVMs;
        private Task _vmCreation;
        private CancellationTokenSource _cancellationToken;


        public HomeWindow_VM()
		{
            _cancellationToken = new CancellationTokenSource();
            _allVMs = new List<BaseVM>();
            _vmCreation = new Task(CreateVMs, _cancellationToken.Token);
            _vmCreation.Start();
		}

        private async void CreateVMs()
        {
            while (MainViewModel.GetInstance().User == null)
                await Task.Delay(10);
            _allVMs.Add(new AllTask_VM());
            _allVMs.Add(new MyTasks_VM());
            _allVMs.Add(new Account_VM());
            _allVMs.Add(new Project_VM());
            _cancellationToken.Cancel();
        }

        private void SetCurrentVM(BaseVM vm)
        {
            if (vm == null)
                return;
            _baseVM = vm;
            OnPropertyChanged("HomePageVM");
        }
	}
}