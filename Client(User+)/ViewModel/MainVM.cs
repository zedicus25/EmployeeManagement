

using Client_User__.Model;

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
    }
}
