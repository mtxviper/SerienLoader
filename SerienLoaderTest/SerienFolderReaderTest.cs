using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SerienLoaderTest
{
    [TestClass]
    public class SerienFolderReaderTest

    {
        [TestMethod]
        public void TestReadFolder()
        {
            var reader = new SerienLoader.Model.SerienFolderReader();
            reader.AddShows(new DirectoryInfo("E:/Serien/"));

            Assert.IsTrue(reader.Shows.ContainsKey("24"));
            Assert.IsTrue(reader.Shows["24"].Seasons.Count==2);
        }
    }
}
