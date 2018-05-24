using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace MagicBox.Models
{
  public  class Model : INotifyPropertyChanged
    {
        private DateTimeOffset _date;
        private Uri _songUri;
        private Uri _photoUri;
        private String _mood;
        private String _feedback;
        private String _id;
        private String _diary;

        public String getId() {
            return _id;
        }

        public DateTimeOffset date {
            get {
                return _date;
            }
            set {
                _date = value;
                RaisePropertyChanged("date");
            }
        }

        public Uri songUri {
            get {
                return _songUri;
            }
            set {
                _songUri = value;
                RaisePropertyChanged("songUri");
            }
        }

        public Uri photoUri
        {
            get
            {
                return _photoUri;
            }
            set
            {
                _photoUri = value;
                RaisePropertyChanged("photoUri");
            }
        }
        public String mood
        {
            get
            {
                return _mood;
            }
            set
            {
                _mood = value;
                RaisePropertyChanged("mood");
            }
        }
        public String feedback
        {
            get
            {
                return _feedback;
            }
            set
            {
                _feedback = value;
                RaisePropertyChanged("feedback");
            }
        }
        public String diary
        {
            get
            {
                return _diary;
            }
            set
            {
                _diary = value;
                RaisePropertyChanged("diary");
            }
        }

        public Model() {
            this._id = "0";
            this.date = DateTime.Now.Date;
            //this.songUri = ;//new Uri("Assets/");
            //this.photoUri =;// new Uri("Assets/");
            this.mood = "";
            this.diary = "";
            this.feedback = "";
        }

        public Model(Uri songUri, Uri photoUri, String mood, String diary, String feedback)
        {
            this._id = Guid.NewGuid().ToString();
            this.date = DateTime.Now.Date;
            this.songUri = songUri;
            this.photoUri = photoUri;
            this.mood = mood;
            this.diary = diary;
            this.feedback = feedback;
        }

        public Model(String id, DateTimeOffset date, Uri songUri, Uri photoUri, String mood, String diary, String feedback)
        {
            this._id = id;
            this.date = date;
            this.songUri = songUri;
            this.photoUri = photoUri;
            this.mood = mood;
            this.diary = diary;
            this.feedback = feedback;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(String _str) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(_str));
            }
        }
    }
}
