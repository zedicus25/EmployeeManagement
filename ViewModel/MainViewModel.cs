
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

        private BaseVM _selectedVM;


		public MainViewModel()
		{
			_selectedVM = new LoginWindow_VM();
			if(Instance == null)
			{
				Instance = this;
				return;
			}
			else
			{
				return;
			}
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
