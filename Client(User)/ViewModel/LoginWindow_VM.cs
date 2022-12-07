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


        public bool LogingResult
        {
            get { return _loginigResult; }
            set 
            { 
                _loginigResult = value;
                OnPropertyChanged("LogingResult");
            }
        }


        public string PasswordInput
        {
            get => _passwordInput;
            set
            { 
                _passwordInput = value;
                OnPropertyChanged("PasswordInput");
            }
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
                        string msg = $"id={MainViewModel.GetInstance().ServerClient.IdOnServer}\nlogin={LoginInput}\npassword={PasswordInput}";
                        if (MainViewModel.GetInstance().ServerClient.IsConnected)
                        {
                            MainViewModel.GetInstance().ServerClient.SendMessageToServer(msg);
                            AddListeners();
                            LogingResult = false;
                        }   
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
            LogingResult = true;
            
        }

        public void AddListeners()
        {
            MainViewModel.GetInstance().ServerClient.GetServerMessage += UpdateServerMessages;
            MainViewModel.GetInstance().ServerClient.LoginingResult += SetLoginigResult;
        }

        private void UpdateServerMessages(string msg)
        {
            _serverMessage = msg;
            OnPropertyChanged("ServerMessages");
        }

        private void SetLoginigResult(bool res, User user)
        {
            LogingResult = !res;
            if (res)
            {
                MainViewModel.GetInstance().SetViewModel(new HomeWindow_VM());
                MainViewModel.GetInstance().User = user;
            }   
        }

    }
}
