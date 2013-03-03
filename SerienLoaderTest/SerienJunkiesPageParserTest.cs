using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using SerienLoader.Model.PageParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SerienLoaderTest
{
    [TestClass]
    public class SerienJunkiesPageParserTest

    {
        [TestMethod]
        public void ParseTest()
        {

           string readAllText = File.ReadAllText("TestData\\2-broke-girls.htm");
           HtmlDocument htmlDocument = new HtmlDocument();
           htmlDocument.LoadHtml(readAllText);
           var serienJunkiesPageParser = new SerienJunkiesPageParser(htmlDocument);

           
        }
    }
}
