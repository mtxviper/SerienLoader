using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SerienLoader.Annotations;
using SerienLoader.Model;
using SerienLoader.Utility;

namespace SerienLoader.ViewModel
{
   internal class MainViewModel : INotifyPropertyChanged
   {
      private string _log;

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

      private ObservableCollection<Show> _shows;

      public ObservableCollection<Show> Shows
      {
         get { return _shows; }
         set
         {
            if (Equals(value, _shows)) return;
            _shows = value;
            OnPropertyChanged("Shows");
         }
      }

      public ICommand ReadExistingEpisodesCommand { get; set; }
      public MainModel Model { get; set; }

      public MainViewModel()
      {
         Model = new MainModel();
         Shows=new ObservableCollection<Show>();
         ReadExistingEpisodesCommand = new RelayCommand(ReadExistingEpisodes);
      }

      private void ReadExistingEpisodes(object obj)
      {
         Model.ReadExistingEpisodes(new List<string> {@"E:\Serien\", @"F:\Serien\"});
         Log = Logger.LogString;
         foreach (KeyValuePair<string, Show> keyValuePair in Model.SerienFolderReader.Shows)
         {
            Shows.Add(keyValuePair.Value);
         }
      }

      

      #region PropertyChanged implementation

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged(string propertyName)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }

      #endregion
   }
}