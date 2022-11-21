using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeManagement.Model
{
    [Serializable]
    public class UserEmail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _email;

		public string Email
		{
			get => _email;
			set 
			{ 
				_email = value;
				OnPropertyChanged("Email");
			}
		}
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
