using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace SerienLoader.Model.PageParser
{
   public class SerienJunkiesPageParser:IPageParser
   {
      public SerienJunkiesPageParser(HtmlDocument doc)
      {
         HtmlNodeCollection divNodeCollection = doc.DocumentNode.SelectNodes("//div");
        
         foreach (HtmlNode postNode in divNodeCollection.Where(x => x.GetAttributeValue("class", "") == "post-content"))
         {
            //postNode contains Season
            foreach (HtmlNode pNode in postNode.ChildNodes.Where(x=>x.Name=="p"))
            {

               if (pNode.ChildNodes.Any(x => x.Name == "strong") && pNode.ChildNodes.Any(x => x.Name == "a"))
               {
                  //pNode contains 
                  Console.Out.WriteLine(pNode.ChildNodes);
               }
               
            }
           
         }
        
      }

      public string Name { get; set; }
      public void AddUrls(Episode episode)
      {
         

      }
   }
}
