

using Client_User__.Model;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace Client_User__.ViewModel
{
    public class MainVM : BaseVM
    {

        public static MainVM GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MainVM();
            }
            return _instance;
        }
        private static MainVM _instance;

        

		public BaseVM SelectedViewModel
		{
			get => _selectedVM; 
			set 
			{ 
				_selectedVM = value; 
				OnPropertyChanged("SelectedViewModel");
			}
		}

        public User User
        {
            get => _user;
            set
            {
                if (value != null)
                    _user = value;
            }
        }

        public ServerClient ServerClient { get; private set; }

        private BaseVM _selectedVM;
        private User _user;
        public bool IsLoginig { get; private set; }

        private MainVM()
        {
            SelectedViewModel = new LoginFormVM();
            IsLoginig = false;
        }

        public void SetViewModel(BaseVM baseVM)
        {
            if (baseVM == null)
                return;
            SelectedViewModel = baseVM;
        }

        public void ConnectToServer()
        {
            if (ServerClient != null && ServerClient.IsConnected)
                return;

            ServerClient = new ServerClient();

        }

        public void SetLogining(bool res) => IsLoginig = res;

        public void CreateTask(UserTask newTask) =>
            ServerClient.SendMessageToServer($"--createTask\n{JsonConvert.SerializeObject(newTask)}");


        public void DeleteTask(int taskId) => ServerClient.DeleteTask(taskId);

        public void LogOut()
        {
            if (ServerClient.CanSendMessagesToServer)
                ServerClient.SendMessageToServer($"--disconnect\nid={ServerClient.IdOnServer}\ntrue");
            ServerClient.Disconnect();
            ServerClient = new ServerClient();
            SetViewModel(new LoginFormVM());

        }
    }
}
