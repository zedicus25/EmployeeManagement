using EmployeeManagement.ViewModel;
using System.Windows.Controls;


namespace EmployeeManagement.CustomControls
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : UserControl
    {
        public Account()
        {
            InitializeComponent();
            this.DataContext = new Account_VM();
        }
    }
}
