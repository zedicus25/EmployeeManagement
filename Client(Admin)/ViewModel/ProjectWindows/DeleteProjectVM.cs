using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.ProjectWindows
{
    public class DeleteProjectVM :BaseVM
    {
		private List<Project> _projects;

		public List<Project> Projects
		{
			get { return _projects; }
			set 
			{ 
				_projects = value;
				OnPropertyChanged("Projects");
			}
		}

		private Project _selectedProject;

		public Project SelectedProject
		{
			get { return _selectedProject;; }
			set 
			{ 
				_selectedProject = value;
				OnPropertyChanged("SelectedProject");
			}
		}

		private RelayCommand _removeCommand;

		public RelayCommand RemoveCommand
		{
			get { return _removeCommand ?? new RelayCommand(RemoveProject); }
		}


		public DeleteProjectVM()
		{
			Projects = new List<Project>();
			MainVM.GetInstance().ServerClient.GetProjects += GetProjects;
			MainVM.GetInstance().ServerClient.SendQuerryForProjects();
		}

		private void GetProjects(IEnumerable<Project> obj)
		{
			Projects.Clear();
			foreach (var item in obj)
			{
				Projects.Add(item);
			}
		}

		private void RemoveProject()
		{
			if (SelectedProject == null)
				return;
			MainVM.GetInstance().ServerClient.RemoveProject(SelectedProject.Id);
			Projects.Remove(SelectedProject);
			SelectedProject = new Project();
		}
	}
}
