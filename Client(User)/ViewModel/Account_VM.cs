using EmployeeManagement.Model;
using System;


namespace EmployeeManagement.ViewModel
{
    public class Account_VM : BaseVM
    {
        public User User 
        {
            get => _user;
            set 
            { 
                _user = value;
                OnPropertyChanged("User");
            }
        }

        private User _user;

        public Account_VM()
        {
            _user = MainViewModel.GetInstance().User;
        }
    }
}
