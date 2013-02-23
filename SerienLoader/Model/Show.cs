using System;
using System.Collections.Generic;
using System.IO;

namespace SerienLoader.Model
{
    public class Show
    {
        private const string EnglishSeason = "Season";
        private const string GermanSeason = "Staffel";

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
                if (subFolder.Name.Contains(EnglishSeason))
                {
                    number = Convert.ToInt32(subFolder.Name.Substring(EnglishSeason.Length));
                }
                if (subFolder.Name.Contains(GermanSeason))
                {
                    number = Convert.ToInt32(subFolder.Name.Substring(GermanSeason.Length));
                }
                if (number != 0)
                {
                    if (Seasons.ContainsKey(number))
                    {
                        Seasons[number].AddEpisodes(subFolder);
                    }
                    else
                    {
                        var season = new Season(this, number);
                        season.AddEpisodes(subFolder);

                        Seasons.Add(number, season);
                    }
                   
                }
            }
        }
    }
}