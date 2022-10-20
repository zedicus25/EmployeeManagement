
using EmployeeManagement.Model;
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

		private BaseVM _selectedVM;


        public MainViewModel()
		{
            if (Instance == null)
            {
                Instance = this;
            }
            ServerClient = new ServerClient();
            _selectedVM = new LoginWindow_VM();
			
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
