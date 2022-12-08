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

		private bool _canAddProject;

		public bool CanAddProject
		{
			get { return _canAddProject; }
			set 
			{ 
				_canAddProject = value;
				OnPropertyChanged("CanAddProject");
			}
		}


		public CreateProjectVM()
		{
			CanAddProject = false;
			Project = new Project();
            MainVM.GetInstance().ServerClient.GetProjects += GetProjects;
        }

		private void GetProjects(IEnumerable<Project> obj) => CanAddProject = true;

        private async void AddProject()
		{
			if (Project.Title == String.Empty || Project.Description == String.Empty)
				return;

			MainVM.GetInstance().ServerClient.AddProject(Project);
			CanAddProject = false;
            Project = new Project();
			await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();
		}


	}
}
