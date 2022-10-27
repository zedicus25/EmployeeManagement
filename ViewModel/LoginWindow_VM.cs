using EmployeeManagement.Model;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Threading.Tasks;

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

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(() =>
                {
                    if(LoginInput != String.Empty && PasswordInput != String.Empty)
                    {
                        string msg = $"id={MainViewModel.Instance.ServerClient.IdOnServer}\nlogin={LoginInput}\npassword={PasswordInput}";
                        MainViewModel.Instance.ServerClient.SendMessageToServer(msg);
                    }
                }));
            }
        }


        private string _loginInput;
        private string _passwordInput;
        private string _serverMessage;
        
        private bool _loginigResult;

        private RelayCommand _loginCommand;

        public LoginWindow_VM()
        {
            MainViewModel.Instance.ServerClient.GetServerMessage += UpdateServerMessages;
            MainViewModel.Instance.ServerClient.LoginingResult += SetLoginigResult;
        }


        private void UpdateServerMessages(string msg)
        {
            _serverMessage = msg;
            OnPropertyChanged("ServerMessages");
        }

        private void SetLoginigResult(bool res, User user)
        {
            _loginigResult = res;
            if (_loginigResult)
            {
                MainViewModel.Instance.SetViewModel(new HomeWindow_VM());
                MainViewModel.Instance.User = user;
            }
               
        }

    }
}
