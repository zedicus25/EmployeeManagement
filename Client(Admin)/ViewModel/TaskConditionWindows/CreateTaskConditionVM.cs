using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.TaskConditionWindows
{
    public class CreateTaskConditionVM : BaseVM
    {
		private TaskCondition _condition;

		public TaskCondition TaskCondition
		{
			get { return _condition; }
			set 
			{ 
				_condition = value;
				OnPropertyChanged("TaskCondition");
			}
		}
        private RelayCommand _addCommand;

        public RelayCommand AddCommand
        {
            get { return _addCommand ?? new RelayCommand(AddCondition); }
        }

		public CreateTaskConditionVM()
		{
			TaskCondition = new TaskCondition();
		}

		private void AddCondition()
		{
			if (TaskCondition.Title == String.Empty || TaskCondition.Description == String.Empty)
				return;

			MainVM.GetInstance().ServerClient.AddTaskCondition(TaskCondition);
			TaskCondition = new TaskCondition();
		}

    }
}
