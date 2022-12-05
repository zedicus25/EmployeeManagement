using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client_Admin_.Model
{
    [Serializable]
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Password
        {
            get => _password;
            set 
            { 
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public string Login
        {
            get => _login; 
            set 
            { 
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public int UserRoleId
        {
            get => _userRoleId;
            set
            {
                _userRoleId = value;
                OnPropertyChanged("UserRoleId");
            }
        }
        public string UserRoleName
        {
            get => _userRoleName;
            set
            {
                _userRoleName = value;
                OnPropertyChanged("UserRoleName");
            }
        }
        public string EmployeeRoleName
        {
            get => _employeeRoleName;
            set
            {
                _employeeRoleName = value;
                OnPropertyChanged("EmployeeRoleName");
            }
        }
        public string EmployeeRoleDescription
        {
            get => _employeeRoleDescription;
            set
            {
                _employeeRoleDescription = value;
                OnPropertyChanged("EmployeeRoleDescription");
            }
        }

        private string _login;
        private string _password;
        private int _userRoleId;
        private string _userRoleName;
        private string _employeeRoleName;
        private string _employeeRoleDescription;
        private int _id;

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
