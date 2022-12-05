using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client_User__.Model
{
    public class UserTask : INotifyPropertyChanged
    {
        public int Id
        {
            get => _id; 
            set 
            { 
                _id = value; 
                OnPropertyChanged("Id");
            }
        }

        public int EmployeeId
        {
            get => _employeeId;
            set
            {
                _employeeId = value;
                OnPropertyChanged("EmployeeId");
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
        
        public string ProjectTitle
        {
            get { return _projectTitle; }
            set 
            { 
                _projectTitle = value;
                OnPropertyChanged("ProjectTitle");
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public int ConditionId
        {
            get => _conditionId;
            set
            {
                _conditionId = value;
                OnPropertyChanged("ConditionId");
            }
        }
        public int ImportanceId
        {
            get => _importanceId;
            set
            {
                _importanceId = value;
                OnPropertyChanged("ImportanceId");
            }
        }
        public DateTime ToComplete
        {
            get => _toComplete;
            set
            {
                _toComplete = value;
                OnPropertyChanged("ToComplete");
            }
        }
        public DateTime CreationDate
        {
            get => _creationDate;
            set
            {
                _creationDate = value;
                OnPropertyChanged("CreationDate");
            }
        }

        private int _id;
        private int _employeeId;
        private int _projectId;
        private string _title;
        private string _description;
        private int _conditionId;
        private int _importanceId;
        private DateTime _toComplete;
        private DateTime _creationDate;
        private string _projectTitle;

        public UserTask()
        {
            ToComplete = DateTime.Now;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
