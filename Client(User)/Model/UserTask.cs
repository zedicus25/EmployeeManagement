using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    [Serializable]
    public class UserTask : INotifyPropertyChanged
    {

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public int ConditionId
        {
            get { return _conditionId; }
            set
            {
                _conditionId = value;
                OnPropertyChanged("ConditionId");
            }
        }
        public string ConditionName
        {
            get { return _conditionName; }
            set
            {
                _conditionName = value;
                OnPropertyChanged("ConditionName");
            }
        }
        public int ImportanceId
        {
            get { return _importanceId; }
            set
            {
                _importanceId = value;
                OnPropertyChanged("ImportanceId");
            }
        }

        public string ImportanceName
        {
            get { return _importanceName; }
            set
            {
                _importanceName = value;
                OnPropertyChanged("ImportanceName");
            }
        }
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                _creationDate = value;
                OnPropertyChanged("CreationDate");
            }
        }
        public DateTime ToComplete
        {
            get { return _toComplete; }
            set
            {
                _toComplete = value;
                OnPropertyChanged("ToComplete");
            }
        }
        private int _id;
        private string _title;
        private string _description;
        private int _conditionId;
        private string _conditionName;
        private int _importanceId;
        private string _importanceName;
        private DateTime _creationDate;
        private DateTime _toComplete;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
