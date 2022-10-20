using EmployeeManagement.ViewModel;
using System.Windows.Controls;

namespace EmployeeManagement.CustomControls
{
    /// <summary>
    /// Interaction logic for AllTasks.xaml
    /// </summary>
    public partial class AllTasks : UserControl
    {
        public AllTasks()
        {
            InitializeComponent();
            this.DataContext = new AllTask_VM();
        }
    }
}
