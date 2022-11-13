using EmployeeManagement.ViewModel;
using System.Windows.Controls;

namespace EmployeeManagement.CustomControls
{
    /// <summary>
    /// Interaction logic for AboutProject.xaml
    /// </summary>
    public partial class AboutProject : UserControl
    {
        public AboutProject()
        {
            InitializeComponent();
            this.DataContext = new Project_VM();
        }
    }
}
