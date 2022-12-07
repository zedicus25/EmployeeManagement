using Client_Admin_.View;
using Client_Admin_.View.EmployeeWindows;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Client_Admin_.ViewModel
{
    public class HomePageVM : BaseVM
    {

        private RelayCommand _showEmployeeWindow;

        public RelayCommand ShowEmployeeWindow
        {
            get { return _showEmployeeWindow ?? new RelayCommand(() => ShowWindow(new EmployeeWindow())); }
        }

        private RelayCommand _showEmployeeRoleWindow;

        public RelayCommand ShowEmployeeRoleWindow
        {
            get { return _showEmployeeRoleWindow ?? new RelayCommand(() => ShowWindow(new EmployeeRoleWindow())); }
        }
    
        private RelayCommand _showProjectWindow;

        public RelayCommand ShowProjectWindow
        {
            get { return _showProjectWindow ?? new RelayCommand(() => ShowWindow(new ProjectsWindow())); }
        }
        private RelayCommand _showTaskConditionWindow;

        public RelayCommand ShowTaskConditionWindow
        {
            get { return _showTaskConditionWindow ?? new RelayCommand(() => ShowWindow(new TaskConditionWindow())); }
        }
        private RelayCommand _showTaskImportanceWindow;

        public RelayCommand ShowTaskImportanceWindow
        {
            get { return _showTaskImportanceWindow ?? new RelayCommand(() => ShowWindow(new TaskImportanceWindow())); }
        }

        public HomePageVM()
        {

        }

        private void ShowWindow(Window window) => window.ShowDialog();


    }
}
