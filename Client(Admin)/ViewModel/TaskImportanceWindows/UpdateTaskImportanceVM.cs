using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.TaskImportanceWindows
{
    public class UpdateTaskImportanceVM : BaseVM
    {
        private TaskImportance _taskImportance;

        public TaskImportance SelectedImportance
        {
            get { return _taskImportance; }
            set
            {
                _taskImportance = value;
                OnPropertyChanged("SelectedImportance");
            }
        }

        private ObservableCollection<TaskImportance> _taskImportances;

        public ObservableCollection<TaskImportance> TaskImportances
        {
            get { return _taskImportances; }
            set
            {
                _taskImportances = value;
                OnPropertyChanged("TaskImportances");
            }
        }
        private RelayCommand _updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return _updateCommand ?? new RelayCommand(UpdateImportances); }
        }
        public UpdateTaskImportanceVM()
        {
            TaskImportances = new ObservableCollection<TaskImportance>();
            SelectedImportance = new TaskImportance();
            MainVM.GetInstance().ServerClient.GetTaskImortances += GetTaskImportances;
            MainVM.GetInstance().ServerClient.SendQuerryForTaskImportances();
        }

        private void GetTaskImportances(List<TaskImportance> obj)
        {
            if (TaskImportances.Count <= 0)
            {
                TaskImportances = new ObservableCollection<TaskImportance>(obj);
                return;
            }

            TaskImportances.Union(obj);
        }

        private void UpdateImportances()
        {
            if (SelectedImportance == null)
                return;
            if (SelectedImportance.Title == String.Empty || SelectedImportance.Description == String.Empty)
                return;

            MainVM.GetInstance().ServerClient.UpdateTaskImportance(SelectedImportance.Id, SelectedImportance);
            SelectedImportance = null;

        }
    }
}
