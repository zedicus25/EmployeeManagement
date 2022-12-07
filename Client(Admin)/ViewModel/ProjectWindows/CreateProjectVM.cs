using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.ProjectWindows
{
    public class CreateProjectVM : BaseVM
    {
		private Project _project;

		public Project Project
		{
			get { return _project; }
			set 
			{ 
				_project = value;
				OnPropertyChanged("Project");
			}
		}

		private RelayCommand _addCommand;

		public RelayCommand AddCommand
		{
			get { return _addCommand ?? new RelayCommand(AddProject); }
		}

		public CreateProjectVM()
		{
			Project = new Project();
		}

		private void AddProject()
		{
			if (Project.Title == String.Empty || Project.Description == String.Empty)
				return;

			MainVM.GetInstance().ServerClient.AddProject(Project);
			Project = new Project();
		}


	}
}
