using Client_Admin_.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel
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

        private MainVM()
        {
            SelectedViewModel = new LoginFormVM();
            ServerClient = new ServerClient();
        }

        public void SetViewModel(BaseVM baseVM)
        {
            if (baseVM == null)
                return;
            SelectedViewModel = baseVM;
        }

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
