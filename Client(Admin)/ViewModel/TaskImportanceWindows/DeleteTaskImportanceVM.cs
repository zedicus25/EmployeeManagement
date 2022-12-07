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
    public class DeleteTaskImportanceVM : BaseVM
    {
        private TaskImportance _importance;

        public TaskImportance SelectedImportance
        {
            get { return _importance; }
            set
            {
                _importance = value;
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
        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? new RelayCommand(RemoveImportances); }
        }

        public DeleteTaskImportanceVM()
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

        private void RemoveImportances()
        {
            if (SelectedImportance == null)
                return;

            MainVM.GetInstance().ServerClient.DeleteTaskImportance(SelectedImportance.Id);
            TaskImportances.Remove(SelectedImportance);
            SelectedImportance = null;
        }
    }
}
