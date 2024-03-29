﻿using EmployeeManagement.Model;
using EmployeeManagement.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModel
{
    public class MainViewModel : BaseVM
    {

        public static MainViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MainViewModel();
            }
            return _instance;
        }
        private static MainViewModel _instance;

        public BaseVM SelectedViewModel
		{
			get => _selectedVM;
            set 
			{ 
				_selectedVM = value; 
				OnPropertyChanged("SelectedViewModel");
			}
		}


		public ServerClient ServerClient { get; private set; }
		public User User 
		{
			get => _user;
			set
			{
				if(value != null)
					_user = value;
			} 
		}

		private BaseVM _selectedVM;
		private User _user;

        private Task _listenAllTask;
        private CancellationTokenSource _tokenSourceListenTasks;
        private Task _listenMyTask;
        private CancellationTokenSource _tokenSourceListenMyTasks;
        private TimeSpan _querryDelay;


        private MainViewModel()
		{
			_querryDelay = new TimeSpan(0, 5, 0);
            SelectedViewModel = new LoginWindow_VM();
            _tokenSourceListenTasks = new CancellationTokenSource();
			_tokenSourceListenMyTasks = new CancellationTokenSource();
            _listenAllTask = new Task(SendAllTasksQuerry, _tokenSourceListenTasks.Token);
            _listenMyTask = new Task(SendMyTasksQuerry, _tokenSourceListenMyTasks.Token);

        }

		public void ConnectToServer()
		{
			if (ServerClient != null && ServerClient.IsConnected)
				return;

            ServerClient = new ServerClient();

            
            _listenAllTask.Start();
            _listenMyTask.Start();
        }

		

		public void SetViewModel(BaseVM baseVM)
		{
			if (baseVM == null)
				return;
			_selectedVM = baseVM;
			OnPropertyChanged("SelectedViewModel");
		}

		public void GetAllTasks() => ServerClient.GetAllTasks();

		public void GetMyTask() => ServerClient.GetMyTask();

		public void SubmitTask(int taskId, string branchName, string message) => 
			ServerClient.SubmitTask(taskId, branchName, message);


        private async void SendAllTasksQuerry()
        {
            while (_tokenSourceListenTasks.Token.IsCancellationRequested == false)
            {
                ServerClient.GetAllTasks();
                await Task.Delay(_querryDelay);
            }
        }
        private async void SendMyTasksQuerry()
        {
            while (_tokenSourceListenTasks.Token.IsCancellationRequested == false)
            {
                ServerClient.GetMyTask();
                await Task.Delay(_querryDelay);
            }
        }



        public void LogOut()
		{
			if(ServerClient.CanSendMessagesToServer)
				ServerClient.SendMessageToServer($"--disconnect\nid={ServerClient.IdOnServer}\ntrue");
			ServerClient.Disconnect();
			_tokenSourceListenTasks.Cancel();
			_tokenSourceListenTasks = new CancellationTokenSource();
            ServerClient = new ServerClient();
            SetViewModel(new LoginWindow_VM());
            
        }
		~MainViewModel()
		{
			_tokenSourceListenTasks?.Cancel();
		}

    }
}
