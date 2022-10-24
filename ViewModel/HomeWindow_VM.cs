using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Documents;

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
                    SetViewModel(new AllTask_VM());
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
                    SetViewModel(new MyTasks_VM());
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
                    SetViewModel(new Account_VM());
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
                    SetViewModel(new Project_VM());
                }));
            }
        }

        private RelayCommand _goToAllTasks;
        private RelayCommand _goToMyTasks;
        private RelayCommand _goToAccount;
        private RelayCommand _goToProject;

        private BaseVM _baseVM;


        public HomeWindow_VM()
		{
		}

		private void SetViewModel(BaseVM viewModel)
        {
            if (viewModel == null)
                return;
            _baseVM = viewModel;
            OnPropertyChanged("HomePageVM");
        }

		


	}
}
