using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeManagement.Model
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public uint Id
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


        private string _login;
        private string _password;
        private uint _id;

        public User(uint id, string password, string login)
        {
            _id = id;
            _password = password;
            _login = login;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
