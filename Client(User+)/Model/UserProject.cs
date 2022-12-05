using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client_User__.Model
{
    [Serializable]
    public class UserProject : INotifyPropertyChanged
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

        public List<string> Images
        {
            get => _images;
            set
            {
                _images = value;
                OnPropertyChanged("Images");
            }
        }

        private int _id;
        private string _title;
        private string _description;
        private List<string> _images;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

