using EmployeeManagement.Model;
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
		private Task _listemMyTask;
        private CancellationTokenSource _tokenSourceListenTasks;
		private TimeSpan _querryDelay;


        private MainViewModel()
		{
			_querryDelay = new TimeSpan(0, 5, 0);
			ServerClient = new ServerClient();
			SelectedViewModel = new LoginWindow_VM();
			_tokenSourceListenTasks = new CancellationTokenSource();
            _listenAllTask = new Task(SendAllTasksQuerry, _tokenSourceListenTasks.Token);
			_listemMyTask = new Task(SendMyTasksQuerry, _tokenSourceListenTasks.Token);
            _listenAllTask.Start();
			//_listemMyTask.Start();
        }

		public void SetViewModel(BaseVM baseVM)
		{
			if (baseVM == null)
				return;
			_selectedVM = baseVM;
			OnPropertyChanged("SelectedViewModel");
		}

		public void GetAllTasks()
		{
			ServerClient.GetAllTasks();
		}


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
				await Task.Delay(_querryDelay);
			}
		}

		

		~MainViewModel()
		{
			_tokenSourceListenTasks.Cancel();
		}

    }
}
