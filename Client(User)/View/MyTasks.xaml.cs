using EmployeeManagement.ViewModel;
using System.Windows.Controls;


namespace EmployeeManagement.CustomControls
{
    /// <summary>
    /// Interaction logic for MyTasks.xaml
    /// </summary>
    public partial class MyTasks : UserControl
    {
        public MyTasks()
        {
            InitializeComponent();
            this.DataContext = new MyTasks_VM();
        }
    }
}
