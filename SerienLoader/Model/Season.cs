using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SerienLoader.Utility;

namespace SerienLoader.Model
{
   public class Season
   {
      private static readonly IList<string> VideoFileExtensions = new List<string> {".avi", ".mkv", ".mpg", ".mp4"};

      public Season(Show show, int number, Language language)
      {
         Show = show;
         Number = number;
         Language = language;
         Episodes = new Dictionary<int, Episode>();
      }

      public int Number { get; set; }
      public Show Show { get; set; }
      public IDictionary<int, Episode> Episodes { get; set; }

      public Language Language { get; set; }

      public void AddEpisodes(DirectoryInfo folder)
      {
         foreach (FileInfo file in folder.EnumerateFiles())
         {
            if (VideoFileExtensions.Any(x => x == file.Extension))
            {
               string pre = "S" + Number.ToString("00") + "E";
               if (file.Name.IndexOf(pre, StringComparison.InvariantCultureIgnoreCase) >= 0)
               {
                  string episodeNumberString =
                     file.Name.Substring(file.Name.IndexOf(pre, StringComparison.InvariantCultureIgnoreCase) + 4)
                         .Remove(2);
                  int episodeNumber = Convert.ToInt32(episodeNumberString);
                  string title =
                     file.Name.Substring(file.Name.IndexOf(pre, StringComparison.InvariantCultureIgnoreCase) + 7);
                  foreach (string videoFileExtension in VideoFileExtensions)
                  {
                     title = title.Replace(videoFileExtension, "");
                  }
                  var episode = new Episode(this, episodeNumber, title);
                  episode.File = file;
                  if (Episodes.ContainsKey(episodeNumber))
                  {
                     Logger.Log(file.Name + " found twice!");
                  }
                  else
                  {
                     Episodes.Add(episodeNumber, episode);
                  }
               }
               else
               {
                  Logger.Log("Could not parse "+file.FullName);
               }
            }
         }
      }

      public const string EnglishSeason = "Season";
      public const string GermanSeason = "Staffel";
   }
}