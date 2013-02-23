using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerienLoader.Model
{
    public class MainModel
    {
        public MainModel()
        {
            SerienFolderReader = new SerienFolderReader();
        }

        public SerienFolderReader SerienFolderReader { get; set; }

        public void ReadExistingEpisodes(IEnumerable<string> folders)
        {
            SerienFolderReader.ReadFolders(folders);

        }
    }
}
