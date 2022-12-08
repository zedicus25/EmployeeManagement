using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.ProjectWindows
{
    public class DeleteProjectVM :BaseVM
    {
		private ObservableCollection<Project> _projects;

		public ObservableCollection<Project> Projects
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
		private bool _canDeleteProject;

		public bool CanDeleteProject
		{
			get { return _canDeleteProject; }
			set 
			{ 
				_canDeleteProject = value;
				OnPropertyChanged("CanDeleteProject");
			}
		}



		public DeleteProjectVM()
		{
			CanDeleteProject = false;
			Projects = new ObservableCollection<Project>();
			MainVM.GetInstance().ServerClient.GetProjects += GetProjects;
		}

		private void GetProjects(IEnumerable<Project> obj)
		{
			CanDeleteProject = true;
			Projects = new ObservableCollection<Project>(obj);
			OnPropertyChanged("Projects");
		}

		private async void RemoveProject()
		{
			if (SelectedProject == null)
				return;
			MainVM.GetInstance().ServerClient.RemoveProject(SelectedProject.Id);
			CanDeleteProject = false;	
			Projects.Remove(SelectedProject);
			SelectedProject = new Project();
			await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();
        }
	}
}
