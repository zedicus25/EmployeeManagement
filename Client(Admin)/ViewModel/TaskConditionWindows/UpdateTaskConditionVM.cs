using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
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

        private List<TaskCondition> _taskConditions;

        public List<TaskCondition> TaskConditions
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
        public UpdateTaskConditionVM()
        {
            TaskConditions = new List<TaskCondition>();
            SelectedCondition = new TaskCondition();
            MainVM.GetInstance().ServerClient.GetTaskConditions += GetTaskConditions;
            MainVM.GetInstance().ServerClient.SendQuerryForTaskConditions();
        }

        private void GetTaskConditions(IEnumerable<TaskCondition> obj)
        {
            TaskConditions.Clear();
            foreach (var item in obj)
            {
                TaskConditions.Add(item);
            }
        }

        private void UpdateCondition()
        {
            if (SelectedCondition == null)
                return;
            if (SelectedCondition.Title == String.Empty || SelectedCondition.Description == String.Empty)
                return;

            MainVM.GetInstance().ServerClient.UpdateTaskCondition(SelectedCondition.Id, SelectedCondition);
            SelectedCondition = null;   

        }
    }
}
