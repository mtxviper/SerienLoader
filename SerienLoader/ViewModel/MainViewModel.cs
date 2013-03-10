using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SerienLoader.Annotations;
using SerienLoader.Model;
using SerienLoader.Model.PageParser;
using SerienLoader.Utility;

namespace SerienLoader.ViewModel
{
   internal class MainViewModel : INotifyPropertyChanged
   {
     

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
      private string _log;




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
      private ObservableCollection<Show> _shows;

      public ObservableCollection<string> ShowNameList { get; set; }
      public string SelectedShowName
      {
         get { return _selectedShowName; }
         set
         {
            if (value == _selectedShowName) return;
            _selectedShowName = value;
            OnPropertyChanged("SelectedShowName");
           
         }
      }
      private string _selectedShowName;

      public ObservableCollection<string> SeasonList { get; set; }
      public string SelectedSeason
      {
         get { return _selectedSeason; }
         set
         {
            if (value == _selectedSeason) return;
            _selectedSeason = value;

            OnPropertyChanged("SelectedSeason");
            SelectedSeasonChanged();
         }
      }

      private void SelectedSeasonChanged()
      {
         HosterList.Clear();
      
         FormatList.Clear();
         foreach (Episode episode in _seasonNameToSeason[SelectedSeason].Episodes.Values)
         {
            foreach (Link link in episode.Links)
            {
               if (!HosterList.Contains(link.Hoster.Name))
               {
                  HosterList.Add(link.Hoster.Name);
               }
        
               if (!FormatList.Contains(link.CombinedFormat))
               {
                  FormatList.Add(link.CombinedFormat);
               }

            }
         }
      }

      private string _selectedSeason;

      public ObservableCollection<string> FormatList { get; set; }
      public string SelectedFormat { get; set; }

      public ObservableCollection<string> HosterList { get; set; }
      public string SelectedHoster { get; set; }

      public ICommand ReadExistingEpisodesCommand { get; set; }
      public ICommand ParseSelectedShowCommand { get; set; }
      public MainModel Model { get; set; }

      private Dictionary<string,Season> _seasonNameToSeason=new Dictionary<string, Season>();
 

      public MainViewModel()
      {
         Model = new MainModel();
         Shows=new ObservableCollection<Show>();
         ShowNameList=new ObservableCollection<string>();
         SeasonList=new ObservableCollection<string>();
         FormatList= new ObservableCollection<string>();
         HosterList = new ObservableCollection<string>();




         ReadExistingEpisodesCommand = new RelayCommand(ReadExistingEpisodes);
         ParseSelectedShowCommand = new RelayCommand(ParseSelectedShow,x=>SelectedShowName!=null);

         ShowNameList.Add("Californication");
         ShowNameList.Add("Dexter");
         SelectedShowName = "Dexter";
      }

      private void ParseSelectedShow(object obj)
      {
         SeasonList.Clear();

         var serienJunkiesPageParser = new SerienJunkiesPageParser(SelectedShowName);

         foreach (Season season in serienJunkiesPageParser.Seasons)
         {
            string seasonName=null;
            if (season.Language == Language.English)
            {
               seasonName = Season.EnglishSeason + " " + season.Number;
            }
            else if (season.Language == Language.German)
            {
               seasonName = Season.GermanSeason + " " + season.Number;
            }
            if (seasonName != null)
            {
               SeasonList.Add(seasonName);
               _seasonNameToSeason[seasonName] = season;
            }
         }
           
         
      }

      private void ReadExistingEpisodes(object obj)
      {
         //Model.ReadExistingEpisodes(new List<string> {@"E:\Serien\", @"F:\Serien\"});
         //Log = Logger.LogString;
         //foreach (KeyValuePair<string, Show> keyValuePair in Model.SerienFolderReader.Shows)
         //{
         //   Shows.Add(keyValuePair.Value);

         //}
         
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