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
        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? new RelayCommand(RemoveConditon); }
        }
        private bool _canRemove;

        public bool CanRemove
        {
            get { return _canRemove; }
            set 
            {
                _canRemove = value;
                OnPropertyChanged("CanRemove");
            }
        }


        public DeleteTaskConditionVM()
        {
            CanRemove = false;
            TaskConditions = new ObservableCollection<TaskCondition>();
            SelectedCondition = new TaskCondition();
            MainVM.GetInstance().ServerClient.GetTaskConditions += GetTaskConditions;          
        }

        private void GetTaskConditions(IEnumerable<TaskCondition> obj)
        {
            CanRemove = true;
            TaskConditions = new ObservableCollection<TaskCondition>(obj);
            OnPropertyChanged("TaskConditions");
        }

        private async void RemoveConditon()
        {
            if (SelectedCondition == null)
                return;

            MainVM.GetInstance().ServerClient.DeleteTaskCondition(SelectedCondition.Id);
            TaskConditions.Remove(SelectedCondition);
            SelectedCondition = null;
            CanRemove = false;
            await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForTaskConditions();
        }
    }
}
