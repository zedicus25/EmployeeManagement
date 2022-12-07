using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.ProjectWindows
{
    public class UpdateProjectVM : BaseVM
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
            get { return _selectedProject; ; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }
        public UpdateProjectVM()
        {
            Projects = new List<Project>();
            MainVM.GetInstance().ServerClient.GetProjects += GetProjects;
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();
        }
        private RelayCommand _updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return _updateCommand ?? new RelayCommand(UpdateProject); }
        }
        private void GetProjects(IEnumerable<Project> obj)
        {
            Projects.Clear();
            foreach (var item in obj)
            {
                Projects.Add(item);
            }
        }

        private void UpdateProject()
        {
            if (SelectedProject == null)
                return;
            MainVM.GetInstance().ServerClient.UpdateProject(SelectedProject.Id, SelectedProject);
            SelectedProject = new Project();
        }

    }
}
