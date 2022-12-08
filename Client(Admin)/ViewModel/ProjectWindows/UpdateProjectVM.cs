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
    public class UpdateProjectVM : BaseVM
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
            get { return _selectedProject; ; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }
        private RelayCommand _updateCommand;


        private bool _canUpdateProject;

        public bool CanUpdateProject
        {
            get { return _canUpdateProject; }
            set
            {
                _canUpdateProject = value;
                OnPropertyChanged("CanUpdateProject");
            }
        }

        public RelayCommand UpdateCommand
        {
            get { return _updateCommand ?? new RelayCommand(UpdateProject); }
        }
        public UpdateProjectVM()
        {
            CanUpdateProject = false;
            Projects = new ObservableCollection<Project>();
            MainVM.GetInstance().ServerClient.GetProjects += GetProjects;
        }
        
        private void GetProjects(IEnumerable<Project> obj)
        {
            CanUpdateProject = true;
            Projects = new ObservableCollection<Project>(obj);
            OnPropertyChanged("Projects");
        }

        private async void UpdateProject()
        {
            if (SelectedProject == null)
                return;
            MainVM.GetInstance().ServerClient.UpdateProject(SelectedProject.Id, SelectedProject);
            SelectedProject = new Project();
            CanUpdateProject = false;
            await Task.Delay(800);
            MainVM.GetInstance().ServerClient.SendQuerryForProjects();

        }

    }
}
