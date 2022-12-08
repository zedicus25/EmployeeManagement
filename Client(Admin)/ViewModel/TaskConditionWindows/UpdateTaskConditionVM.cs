using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.TaskConditionWindows
{
    public class UpdateTaskConditionVM : BaseVM
    {
        private TaskCondition _taskCondition;

        public TaskCondition SelectedCondition
        {
            get { return _taskCondition; }
            set
            {
                _taskCondition = value;
                OnPropertyChanged("SelectedCondition");
            }
        }

        private ObservableCollection<TaskCondition> _taskConditions;

        public ObservableCollection<TaskCondition> TaskConditions
        {
            get { return _taskConditions; }
            set
            {
                _taskConditions = value;
                OnPropertyChanged("TaskConditions");
            }
        }
        private RelayCommand _updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return _updateCommand ?? new RelayCommand(UpdateCondition); }
        }

        private bool _canUpdate;

        public bool CanUpdate
        {
            get { return _canUpdate; }
            set 
            { 
                _canUpdate = value;
                OnPropertyChanged("CanUpdate");
            }
        }

        public UpdateTaskConditionVM()
        {
            CanUpdate = false;
            TaskConditions = new ObservableCollection<TaskCondition>();
            SelectedCondition = new TaskCondition();
            MainVM.GetInstance().ServerClient.GetTaskConditions += GetTaskConditions;
        }

        private void GetTaskConditions(IEnumerable<TaskCondition> obj)
        {
            CanUpdate = true;
            TaskConditions = new ObservableCollection<TaskCondition>(obj);
            OnPropertyChanged("TaskConditions");
        }

        private async void UpdateCondition()
        {
            if (SelectedCondition == null)
                return;

            MainVM.GetInstance().ServerClient.UpdateTaskCondition(SelectedCondition.Id, SelectedCondition);
            SelectedCondition = null;
            CanUpdate = false;
            await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForTaskConditions();
        }
    }
}
