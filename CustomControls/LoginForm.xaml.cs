using EmployeeManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManagement.CustomControls
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : UserControl
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { 
                ((dynamic)this.DataContext).PasswordInput = ((PasswordBox)sender).Password; 
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((dynamic)this.DataContext).AddListeners();
        }
    }
}
