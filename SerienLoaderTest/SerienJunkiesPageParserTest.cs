using System;
using System.Diagnostics;
using System.IO;
using System.Net;
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
           var serienJunkiesPageParser = new SerienJunkiesPageParser();
           serienJunkiesPageParser.ParseHtmlDocument(htmlDocument);
           Assert.AreEqual(4,serienJunkiesPageParser.Seasons.Count);

           
        }


        [TestMethod]
        public void GetPageTest()
        {
           string showName = "How I met your mother";
           string url = SerienJunkiesPageParser.GetUrlFromShowName(showName);
           HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
           myRequest.Method = "GET";
           WebResponse myResponse = myRequest.GetResponse();
           StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
           string result = sr.ReadToEnd();
           sr.Close();
           myResponse.Close();
           Console.Out.WriteLine(result);


        }

    }
}
