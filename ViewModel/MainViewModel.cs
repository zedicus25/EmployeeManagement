
using EmployeeManagement.Model;
using EmployeeManagement.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeManagement.ViewModel
{
    public class MainViewModel : BaseVM
    {
		public static MainViewModel Instance { get; private set; }

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
		private Parser _parser;
		private User _user;


        public MainViewModel()
		{
            if (Instance == null)
            {
                Instance = this;
            }
            ServerClient = new ServerClient();
            _selectedVM = new LoginWindow_VM();
			_parser = new Parser();
			
		}

		public void SetViewModel(BaseVM baseVM)
		{
			if (baseVM == null)
				return;
			_selectedVM = baseVM;
			OnPropertyChanged("SelectedViewModel");
		}

    }
}
