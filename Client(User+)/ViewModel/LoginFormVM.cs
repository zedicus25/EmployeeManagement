﻿using Client_User__.Model;
using GalaSoft.MvvmLight.Command;
using System;

namespace Client_User__.ViewModel
{
    public class LoginFormVM : BaseVM
    {
		public string PasswordInput
        {
			get => _passwordInput; 
			set 
			{ 
				_passwordInput = value;
				OnPropertyChanged("PasswordInput");
			}
		}
        public string LoginInput
        {
            get => _loginInput;
            set
            {
                _loginInput = value;
                OnPropertyChanged("LoginInput");
            }
        }
        public string ServerMessages
        {
            get => _serverMessage;
            set 
            { 
                _serverMessage = value;
                OnPropertyChanged("ServerMessages");
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(() =>
                {
                    if (LoginInput != String.Empty && PasswordInput != String.Empty)
                    {
                        string msg = $"id={MainVM.GetInstance().ServerClient.IdOnServer}\nlogin={LoginInput}\npassword={PasswordInput}";
                        MainVM.GetInstance().ServerClient.SendMessageToServer(msg);
                        AddListeners();
                    }
                }));
            }
        }

        private RelayCommand _loginCommand;
        private string _passwordInput;
        private string _loginInput;
        private string _serverMessage;
        private bool _loginigResult;
        

        public LoginFormVM()
        {

        }

        public void AddListeners()
        {
            MainVM.GetInstance().ServerClient.GetServerMessage += UpdateServerMessages;
            MainVM.GetInstance().ServerClient.LoginingResult += SetLoginigResult;
        }

        private void UpdateServerMessages(string msg)
        {
            _serverMessage = msg;
            OnPropertyChanged("ServerMessages");
        }

        private void SetLoginigResult(bool res, User user)
        {
            if (MainVM.GetInstance().IsLoginig)
                return;
            _loginigResult = res;
            if (_loginigResult && user.UserRoleId != 1)
            {
                MainVM.GetInstance().User = user;
                MainVM.GetInstance().SetViewModel(new HomeControlVM());
                MainVM.GetInstance().SetLogining(true);
            }

        }
    }
}