using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SerienLoader.Annotations;
using SerienLoader.Model;
using SerienLoader.Utility;

namespace SerienLoader.ViewModel
{
    class MainViewModel:INotifyPropertyChanged
    {
        private string _log;


        public MainViewModel()
        {
            Model= new MainModel();
            ReadExistingEpisodesCommand = new RelayCommand(ReadExistingEpisodes);
        }

        private void ReadExistingEpisodes(object obj)
        {
            Model.ReadExistingEpisodes(new List<string> {@"E:\Serien\",@"F:\Serien\"});
            Log = Logger.LogString;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainModel Model { get; set; }

        public string Log
        {
            get { return _log; }
            set
            {
                if (value == _log) return;
                _log = value;
                OnPropertyChanged("Log");
            }
        }

        public ICommand ReadExistingEpisodesCommand { get; set; }
    }

    
}
