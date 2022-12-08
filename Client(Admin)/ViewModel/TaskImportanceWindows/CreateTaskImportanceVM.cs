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

        private bool _canAdd;

        public bool CanAdd
        {
            get { return _canAdd; }
            set 
            { 
                _canAdd = value;
                OnPropertyChanged("CanAdd");
            }
        }


        public CreateTaskImportanceVM()
        {
            CanAdd = false;
            Importance = new TaskImportance();
            MainVM.GetInstance().ServerClient.GetTaskImortances += GetTaskImportances;
        }

        private void GetTaskImportances(List<TaskImportance> obj) => CanAdd = true;

        private async void AddCondition()
        {
            if (Importance.Title == String.Empty || Importance.Description == String.Empty)
                return;

            MainVM.GetInstance().ServerClient.AddTaskImportance(Importance);
            Importance = new TaskImportance();
            CanAdd = false;
            await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForTaskImportances();
        }
    }
}
