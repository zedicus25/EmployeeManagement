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
    public class DeleteTaskConditionVM : BaseVM
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
        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? new RelayCommand(RemoveConditon); }
        }

        public DeleteTaskConditionVM()
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

        private void RemoveConditon()
        {
            if (SelectedCondition == null)
                return;

            MainVM.GetInstance().ServerClient.DeleteTaskCondition(SelectedCondition.Id);
            TaskConditions.Remove(SelectedCondition);
            SelectedCondition = null;
        }
    }
}
