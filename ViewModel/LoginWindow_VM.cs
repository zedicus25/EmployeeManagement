using EmployeeManagement.Model;
using GalaSoft.MvvmLight.CommandWpf;
using System;


namespace EmployeeManagement.ViewModel
{
    public class LoginWindow_VM : BaseVM
    {
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
            set { _serverMessage = value; }
        }

        public bool LoginingResult 
        {
            get => _loginigResult;
            set 
            { 
                _loginigResult = value;
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
        private bool _loginigResult;

        private RelayCommand _loginCommand;

        public LoginWindow_VM()
        {
            _serverClient = new ServerClient();
            _serverClient.GetServerMessage += UpdateServerMessages;
            _serverClient.LoginingResult += SetLoginigResult;
        }


        private void UpdateServerMessages(string msg)
        {
            _serverMessage = msg;
            OnPropertyChanged("ServerMessages");
        }

        private void SetLoginigResult(bool res)
        {
            _loginigResult = res;
            OnPropertyChanged("LoginingResult");
            if (_loginigResult)
                MainViewModel.Instance.SetViewModel(new HomeWindow_VM());
        }
    }
}
