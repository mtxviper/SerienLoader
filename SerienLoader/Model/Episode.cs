using System.Collections.Generic;
using System.IO;

namespace SerienLoader.Model
{
    public class Episode
    {
        public Episode(Season season, int number, string title)
        {
            Season = season;
            Number = number;
            Title = title;          
            Links = new List<Link>();
        }

        public string Title { get; set; }
        
        public int Number { get; set; }

        public Season Season { get; set; }

        public string Language { get; set; }

        public FileInfo File { get; set; }

        public IList<Link> Links { get; set; }
    }
}
