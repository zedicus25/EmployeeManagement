using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace EmployeeManagement.Model
{
    [Serializable]
    public class ProjectTask: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string TaskTitle
        {
            get => _taskTitle;
            set
            {
                _taskTitle = value;
                OnPropertyChanged("TaskTitle");
            }
        }
        public string TaskDescription
        {
            get => _taskDescription;
            set
            {
                _taskDescription = value;
                OnPropertyChanged("TaskDescription");
            }
        }
        public int Importance
        {
            get => _importance;
            set
            {
                _importance = value;
                OnPropertyChanged("Importance");
            }
        }
        public DateTime Term
        {
            get => _term;
            set
            {
                _term = value;
                OnPropertyChanged("Term");
            }
        }
        public int UserId 
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public int ProjectId
        {
            get => _projectId;
            set
            {
                _projectId = value;
                OnPropertyChanged("ProjectId");
            }
        }
        public bool IsComplete
        {
            get => _isComplete;
            set
            {
                _isComplete = value;
                OnPropertyChanged("IsComplete");
            }
        }
        private string _taskTitle;
        private string _taskDescription;
        private int _importance;
        private DateTime _term;
        private int _userId;
        private int _id;
        private int _projectId;
        private bool _isComplete;

        public ProjectTask(string taskTitle, string taskDescription, int importance, DateTime term, int userId, int id, int projectId, bool isComplete)
        {
            this._taskTitle = taskTitle;
            this._taskDescription = taskDescription;
            this._importance = importance;
            this._term = term;
            this._userId = userId;
            this._id = id;
            this._projectId = projectId;
            this._isComplete = isComplete;
            this._taskTitle = taskTitle;
            this._taskDescription = taskDescription;
            this._importance = importance;
            this._term = term;
            this._userId = userId;
            this._id = id;
            this._projectId = projectId;
            this._isComplete = isComplete;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
