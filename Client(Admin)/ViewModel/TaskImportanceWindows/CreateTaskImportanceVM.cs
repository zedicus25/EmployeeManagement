using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.TaskImportanceWindows
{
    public class CreateTaskImportanceVM : BaseVM
    {
        private TaskImportance _importance;

        public TaskImportance Importance
        {
            get { return _importance; }
            set
            {
                _importance = value;
                OnPropertyChanged("Importance");
            }
        }
        private RelayCommand _addCommand;

        public RelayCommand AddCommand
        {
            get { return _addCommand ?? new RelayCommand(AddCondition); }
        }

        public CreateTaskImportanceVM()
        {
            Importance = new TaskImportance();
        }

        private void AddCondition()
        {
            if (Importance.Title == String.Empty || Importance.Description == String.Empty)
                return;

            MainVM.GetInstance().ServerClient.AddTaskImportance(Importance);
            Importance = new TaskImportance();
        }
    }
}
