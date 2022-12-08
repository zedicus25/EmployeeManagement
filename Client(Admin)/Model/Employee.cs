using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.Model
{
    [Serializable]
    public class Employee : INotifyPropertyChanged
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
        public int ProjectId
        {
            get => _projectId;
            set
            {
                _projectId = value;
                OnPropertyChanged("ProjectId");
            }
        }
        public int EmployeeRoleId
        {
            get => _employeeRoleId;
            set
            {
                _employeeRoleId = value;
                OnPropertyChanged("EmployeeRoleId");
            }
        }

        public string FirstName
        {
            get => _fisrstName;
            set
            {
                _fisrstName = value;
                OnPropertyChanged("FirstName");
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

        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }
        public ObservableCollection<string> Emails
        {
            get => _emails;
            set
            {
                _emails = value;
                OnPropertyChanged("Emails");
            }
        }
        public ObservableCollection<string> PhoneNumbers
        {
            get => _phoneNumbers;
            set
            {
                _phoneNumbers = value;
                OnPropertyChanged("PhoneNumbers");
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
            get => _street;
            set
            {
                _street = value;
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
        private string _fisrstName;
        private string _lastName;
        private string _patronymic;
        private ObservableCollection<string> _emails = new ObservableCollection<string>();
        private ObservableCollection<string> _phoneNumbers = new ObservableCollection<string>();
        private int _userRoleId;
        private int _projectId;
        private int _employeeRoleId;
        private DateTime _birthday;
        private float _salary;
        private string _country;
        private string _city;
        private string _street;
        private string _houseNumber;
        private string _fullAdress;

        private int _id;

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));


        public bool IsValid()
        {
            if (Login == String.Empty || Password == String.Empty || FirstName == String.Empty || LastName == String.Empty ||
                Patronymic == String.Empty || Emails.Count == 0 || PhoneNumbers.Count == 0 || 
                ProjectId == 0 || EmployeeRoleId == 0 || Birthday == null || Salary == 0.0f || Country == String.Empty ||
                City == String.Empty || Street == String.Empty || House_Number == String.Empty || Full_Adress == String.Empty)
                return false;
            return true;
        }
    }
}

