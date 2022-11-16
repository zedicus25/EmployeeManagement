using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace EmployeeManagement.Model
{
    public class UserPhoneNumber : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            { 
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
