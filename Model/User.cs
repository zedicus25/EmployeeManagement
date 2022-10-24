using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeManagement.Model
{
    [Serializable]
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public uint Id
        {
            get => _dataBaseId;
            set 
            { 
                _dataBaseId = value; 
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

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }
        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged("Birthday");
            }
        }
        public float Salary
        {
            get => _salary;
            set
            {
                _salary = value;
                OnPropertyChanged("Salary");
            }
        }
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged("CompanyName");
            }
        }
        public int CurrentProject
        {
            get => _currentProject;
            set
            {
                _currentProject = value;
                OnPropertyChanged("CurrentProject");
            }
        }
        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }
        public string Adress
        {
            get => _adress;
            set
            {
                _adress = value;
                OnPropertyChanged("Adress");
            }
        }
        public string Avatar
        {
            get => _avatar;
            set
            {
                _avatar = value;
                OnPropertyChanged("Avatar");
            }
        }
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }
        public bool IsMainAdmin
        {
            get => _isMainAdmin;
            set
            {
                _isMainAdmin = value;
                OnPropertyChanged("IsMainAdmin");
            }
        }

        private uint _dataBaseId;
        private string _login;
        private string _password;
        private string _name;
        private string _lastName;
        private string _patronymic;
        private DateTime _birthday;
        private float _salary;
        private string _companyName;
        private int _currentProject;
        private string _position;
        private string _email;
        private string _phoneNumber;
        private string _country;
        private string _city;
        private string _adress;
        private string _avatar;
        private bool _isAdmin;
        private bool _isMainAdmin;
        


        public User(uint id, string password, string login, string name, string lastName, string patronymic, DateTime birthday,
            float salary, string companyName, int currentProject, string position, string email, string phoneNumber, string country,
            string city, string adress, string avatar, bool isAdmin, bool isMainAdmin)
        {
            _dataBaseId = id;
            _password = password;
            _login = login;
            _name = name;
            _lastName = lastName;
            _patronymic = patronymic;
            _birthday = birthday;
            _salary = salary;
            _companyName = companyName;
            _currentProject = currentProject;
            _position = position;
            _email = email;
            _phoneNumber = phoneNumber;
            _country = country;
            _city = city;
            _adress = adress;
            _avatar = avatar;
            _isAdmin = isAdmin;
            _isMainAdmin = isMainAdmin;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
