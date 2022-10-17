using EmployeeManagement.Model;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace EmployeeManagement.ViewModel
{
    public class LoginWindow_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public string LoginInput
        {
            get => _loginInput;
            set { _loginInput = value; }
        }

        public string PasswordInput
        {
            get => _passwordInput;
            set { _passwordInput = value; }
        }
        public string ServerMessages
        {
            get => _serverMessage;
            set
            {
                _serverMessage = value;
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(() =>
                {
                    if(LoginInput != String.Empty && PasswordInput != String.Empty)
                    {
                        string msg = $"id={_serverClient.IdOnServer}\nlogin={LoginInput}\npassword={PasswordInput}";
                        _serverClient.SendMessageToServer(msg);
                    }
                }));
            }
        }


        private string _loginInput;
        private string _passwordInput;
        private string _serverMessage;
        private ServerClient _serverClient;

        private RelayCommand _loginCommand;

        public LoginWindow_VM()
        {
            _serverClient = new ServerClient();
            _serverClient.GetServerMessage += UpdateServerMessages;
        }


        private void UpdateServerMessages(string msg)
        {
            _serverMessage = msg;
            OnPropertyChanged("ServerMessages");
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
