using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SerienLoader.Utility;

namespace SerienLoader.Model
{
    public class SerienFolderReader
    {
        public SerienFolderReader()
        {
            Shows=new Dictionary<string, Show>();
        }

        public IDictionary<string,Show> Shows { get; set; }

        public void ReadFolders(IEnumerable<string> folders)
        {
            
            foreach (string folder in folders)
            {
                if (Directory.Exists(folder))
                {
                   AddShows(new DirectoryInfo(folder));
                }

            }
            Logger.Log("ReadFolders called");
        }

        public void AddShows(DirectoryInfo folder)
        {
            foreach (DirectoryInfo subFolder in folder.EnumerateDirectories())
            {
                if (Shows.ContainsKey(subFolder.Name))
                {
                    Shows[subFolder.Name].AddSeasons(subFolder);
                }
                else
                {
                    var show = new Show(subFolder.Name);
                    show.AddSeasons(subFolder);
                    Shows.Add(subFolder.Name,show);
                }
            }
        }
    }
}
