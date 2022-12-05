using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeManagement.Model
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

        public string First_Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("First_Name");
            }
        }

        public string Last_Name
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged("Last_Name");
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
        public string Street
        {
            get => _adress;
            set
            {
                _adress = value;
                OnPropertyChanged("Street");
            }
        }
        public string House_Number
        {
            get => _houseNumber;
            set
            {
                _houseNumber = value;
                OnPropertyChanged("House_Number");
            }
        }
        public string Full_Adress
        {
            get => _fullAdress;
            set
            {
                _fullAdress = value;
                OnPropertyChanged("Full_Adress");
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
        

        public List<UserPhoneNumber> PhoneNumbers
        {
            get => _phoneNumbers;
            set 
            { 
                _phoneNumbers = value;
                OnPropertyChanged("PhoneNumbers");
            }
        }


        public List<UserEmail> Emails
        {
            get => _emails;
            set
            {
                _emails = value;
                OnPropertyChanged("Emails");
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
       
        public UserProject Project
        {
            get => _project;
            set
            {
                _project = value;
                OnPropertyChanged("Project");
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
       
       

        private string _login;
        private string _password;
        private string _name;
        private string _lastName;
        private string _patronymic;
        private DateTime _birthday;
        private float _salary;
        private string _country;
        private string _city;
        private string _adress;
        private string _houseNumber;
        private string _fullAdress;
        private List<UserEmail> _emails;
        private List<UserPhoneNumber> _phoneNumbers;
        private int _userRoleId;
        private string _userRoleName;
        private string _employeeRoleName;
        private string _employeeRoleDescription;
        private UserProject _project;
        private int _id;

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
