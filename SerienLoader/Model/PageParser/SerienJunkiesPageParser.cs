using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace SerienLoader.Model.PageParser
{
   public class SerienJunkiesPageParser : IPageParser
   {
      public SerienJunkiesPageParser()
      {
         Seasons= new List<Season>();
      }

      public SerienJunkiesPageParser(string showName):base()
      {
          Seasons= new List<Season>();
         string url = GetUrlFromShowName(showName);
         var htmlDocument = GetHtmlDocumentFromUrl(url);

         ParseHtmlDocument(htmlDocument);

         
         int i=2;
         while (i<10)
         {
            string nextUrl = url+"page/"+i + "/";
            HtmlNode navigationNode = htmlDocument.DocumentNode.SelectNodes("//div").FirstOrDefault(x => x.GetAttributeValue("class", "") == "navigation");
            if (navigationNode != null && navigationNode.InnerHtml.Contains(nextUrl))
            {
               htmlDocument = GetHtmlDocumentFromUrl(nextUrl);

               ParseHtmlDocument(htmlDocument);
               i++;
            }
            else
            {
               break;
            }
            
         }
      }

      private static HtmlDocument GetHtmlDocumentFromUrl(string url)
      {
         var myRequest = (HttpWebRequest) WebRequest.Create(url);
         myRequest.Method = "GET";
         WebResponse myResponse = myRequest.GetResponse();
         var streamReader = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
         string result = streamReader.ReadToEnd();
         streamReader.Close();
         myResponse.Close();

         HtmlDocument htmlDocument = new HtmlDocument();
         htmlDocument.LoadHtml(result);
         return htmlDocument;
      }

      public void ParseHtmlDocument(HtmlDocument doc)
      {
         IEnumerable<HtmlNode> postNodes =
            doc.DocumentNode.SelectNodes("//div").Where(x => x.GetAttributeValue("class", "") == "post").ToList();

         foreach (HtmlNode postNode in postNodes)
         {
            Language language = Language.Unknown;
            int seasonNumber = int.MinValue;
            //postNode contains Season
            string seasonTitle = postNode.SelectSingleNode("./h2").InnerText;
            if (seasonTitle.Contains(Season.EnglishSeason))
            {
               language = Language.English;
               seasonNumber = ParseSeasonNumber(seasonTitle, Season.EnglishSeason);
            }
            else if (seasonTitle.Contains(Season.GermanSeason))
            {
               language = Language.German;
               seasonNumber = ParseSeasonNumber(seasonTitle, Season.GermanSeason);
            }
            if (seasonNumber == int.MinValue)
            {
               Console.Out.WriteLine("ERROR READING SEASONNUMBER");
               break;
            }
            string format = "";
            string size = "";
            HtmlNode postContentNode =
               postNode.SelectNodes("./div").First(x => x.GetAttributeValue("class", "") == "post-content");
            //postContentNode contains List of episodes
            foreach (HtmlNode pNode in postContentNode.ChildNodes.Where(x => x.Name == "p"))
            {
               if (pNode.InnerText.Contains("Dauer:"))
               {
                  bool nextIsSize = false;
                  bool nextIsFormat = false;
                  foreach (HtmlNode node in pNode.ChildNodes)
                  {
                     if (node.Name == "strong" && node.InnerText == "Format:")
                     {
                        nextIsFormat = true;
                     }
                     else if (node.Name == "strong" && node.InnerText.StartsWith("Gr") && node.InnerText.EndsWith("e:"))
                     {
                        nextIsSize = true;
                     }
                     else if (nextIsFormat && node.Name == "#text")
                     {
                        format = node.InnerText.Replace("|", "").Trim();
                        nextIsFormat = false;
                     }
                     else if (nextIsSize && node.Name == "#text")
                     {
                        size = node.InnerText.Replace("|", "").Trim();
                        nextIsSize = false;
                     }
                  }
               }
               if (pNode.ChildNodes.Any(x => x.Name == "strong") && pNode.ChildNodes.Any(x => x.Name == "a"))
               {
                  if (pNode.InnerText.Contains("Dauer:"))
                  {
                     continue;
                  }
                  //pNode Contains epidsodeTitle and links
                  HtmlNode episodeTitleNode = pNode.ChildNodes.First(x => x.Name == "strong");
                  string episodeTitle = episodeTitleNode.InnerText;
                  Console.Out.WriteLine(episodeTitle);
                  string upper = episodeTitle.ToUpperInvariant();
                  int localSeasonNumber;
                  int episodeNumber;
                  ParseSeasonAndEpisodeNumber(upper, out localSeasonNumber, out episodeNumber);
                 
                  if (localSeasonNumber == int.MinValue)
                  {
                     //TODO error
                     continue;
                  }
                  if (localSeasonNumber != seasonNumber)
                  {
                     throw new Exception("SeasonNumber " + seasonNumber + " found in EpisodeTitle " + seasonNumber +
                                         " differs from SeasonNumber found in SeasonTitle " + localSeasonNumber);
                  }
                  //Console.Out.WriteLine("" + seasonNumber + " " + episodeNumber + " " + language);
                  Season season;
                  if (Seasons.Any(x => x.Number == seasonNumber && x.Language == language))
                  {
                     season = Seasons.First(x => x.Number == seasonNumber && x.Language == language);
                  }
                  else
                  {
                     season = new Season(null, seasonNumber, language);
                     Seasons.Add(season);
                  }
                  Episode episode;
                  if (!season.Episodes.TryGetValue(episodeNumber, out episode))
                  {
                     episode = new Episode(season, episodeNumber, "");
                     season.Episodes.Add(episodeNumber, episode);
                  }


                  string url = "";
                  foreach (HtmlNode node in pNode.ChildNodes)
                  {
                     if (node.Name == "#text" && url != "")
                     {
                        string hosterName = node.InnerText.Replace("|", "").Trim();
                        Link link = new Link(Hoster.GetHoster(hosterName), url);
                        link.Format = format;
                        link.Size = size;
                        episode.Links.Add(link);

                        //Console.Out.WriteLine(url + " " + hosterName + " " + format + " " + size);
                        url = "";
                     }
                     else if (node.Name == "a")
                     {
                        url = node.GetAttributeValue("href", "");
                     }
                     else
                     {
                        url = "";
                     }
                  }
               }
            }
         }
      }

      private static int ParseSeasonNumber(string seasonTitle, string seasonWord)
      {
         string seasonNumberString = seasonTitle.Substring(seasonTitle.IndexOf(seasonWord) + seasonWord.Length).Remove(3);
         int seasonNumber = Convert.ToInt32(seasonNumberString);
         return seasonNumber;
      }

      public static void ParseSeasonAndEpisodeNumber(string upper, out int seasonNumber, out int episodeNumber)
      {
         seasonNumber = episodeNumber = int.MinValue;
         for (int i = 0; i < upper.Length - 6; i++)
         {
            if (upper[i] == 'S' && char.IsNumber(upper[i + 1]) && char.IsNumber(upper[i + 2])
                && upper[i + 3] == 'E' && char.IsNumber(upper[i + 4]) && char.IsNumber(upper[i + 5]))
            {
               seasonNumber = Convert.ToInt32(upper.Substring(i + 1).Remove(2));
               episodeNumber = Convert.ToInt32(upper.Substring(i + 4).Remove(2));
               break;
            }
         }
      }

 
      public static string GetUrlFromShowName(string name)
      {
         name = name.ToLowerInvariant();
         string url = "http://serienjunkies.org/" + name.Replace(" ", "-")+"/";
         return url;
      }

      public IList<Season> Seasons { get; set; }

      public string Name { get; set; }

      public void AddUrls(Episode episode)
      {
      }
   }
}