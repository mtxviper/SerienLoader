using System;
using System.Collections.Generic;
using System.IO;

namespace SerienLoader.Model
{
   public class Show
   {
      public Show(string name)
      {
         Name = name;
         Seasons = new Dictionary<int, Season>();
      }

      public string Name { get; set; }
      public IDictionary<int, Season> Seasons { get; set; }


      public void AddSeasons(DirectoryInfo folder)
      {
         foreach (DirectoryInfo subFolder in folder.EnumerateDirectories())
         {
            int number = 0;
            var language = Language.Unknown;
            if (subFolder.Name.Contains(Season.EnglishSeason))
            {
               number = Convert.ToInt32(subFolder.Name.Substring(Season.EnglishSeason.Length));
               language = Language.English;
            }
            else if (subFolder.Name.Contains(Season.GermanSeason))
            {
               number = Convert.ToInt32(subFolder.Name.Substring(Season.GermanSeason.Length));
               language = Language.German;
            }
            
            if (number != 0)
            {
               if (Seasons.ContainsKey(number))
               {
                  Seasons[number].AddEpisodes(subFolder);
               }
               else
               {
                  var season = new Season(this, number, language);
                  season.AddEpisodes(subFolder);

                  Seasons.Add(number, season);
               }
            }
         }
      }
   }
}