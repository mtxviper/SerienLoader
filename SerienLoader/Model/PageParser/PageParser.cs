namespace SerienLoader.Model.PageParser
{
    public class PageParser
    {
        //public static Dictionary<DateTime,List<Episode>> ParseDoc(HtmlDocument doc)
        //{
        //    var dictionary = new Dictionary<DateTime, List<Episode>>();
        //    foreach (HtmlNode tdNode in doc.DocumentNode.SelectNodes("//td")) //get all td Nodes in document
        //    {
        //        if (tdNode.SelectSingleNode("./strong") == null) //nur <td> mit <strong> sind tage
        //        {
        //            continue;
        //        }
        //        HtmlNode aNode = tdNode.SelectSingleNode("./strong").SelectSingleNode("./a");

        //        if (aNode != null && aNode.Attributes["title"] != null)
        //        {
        //            string dayString = aNode.Attributes["title"].Value;
        //            Console.Out.WriteLine(dayString); //Datum 
        //            DateTime day;

        //            if (!TryParseDateString(dayString,out day))

        //                Console.Out.WriteLine("Error parsing " + dayString.Substring(dayString.IndexOf(' ')));

        //            var episodes = ParseEpisodes(tdNode);
        //            dictionary.Add(day,episodes);

        //        }
        //    }
        //    return dictionary;
        //}

        //private static bool TryParseDateString(string dateString,out DateTime date)
        //{
        //    string[] strings = dateString.Split(' ');
        //    int day = int.Parse(strings[1].Remove(strings[1].Length - 2));

        //    date= new DateTime(int.Parse(strings[3]), GetMonthNumber(strings[2]), day);
        //    return true;
        //}

        //private static int GetMonthNumber(string month)
        //{
        //    switch (month)
        //    {
        //        case "January":
        //            return 1;
                   
        //        case "February":
        //            return 2;
        //        case "March":
        //            return 3;
        //        case "April":
        //            return 4;
        //        case "May":
        //            return 5;
        //        case "June":
        //            return 6;
        //        case "July":
        //            return 7;
        //        case "August":
        //            return 8;
        //        case "September":
        //            return 9;
        //        case "October":
        //            return 10;
        //        case "November":
        //            return 11;
        //        case "December":
        //            return 12;


        //    }
        //    return 0;
        //}

        //private static List<Episode> ParseEpisodes(HtmlNode tdNode)
        //{
        //    List<Episode> episodes = new List<Episode>();
        //    foreach (HtmlNode divNode in (tdNode.SelectNodes("./div")))
        //    {
        //        var pNode = divNode.SelectSingleNode("./p");
        //        Episode episode = new Episode();
        //        int i = 0;
        //        foreach (HtmlNode aEpisodeNode in pNode.SelectNodes("./a"))
        //        {
        //            if (i == 0)
        //            {
        //                episode.ShowName = aEpisodeNode.WriteContentTo();
        //                i = 1;
        //            }
        //            else
        //            {
        //                foreach (string s in aEpisodeNode.WriteContentTo().Split('-'))
        //                {
        //                    if (i == 1)
        //                    {
        //                        episode.Season = int.Parse(s.Substring(2));
        //                        i = 2;
        //                    }
        //                    else
        //                    {
        //                        episode.Number = int.Parse(s.Substring(4));
        //                    }
        //                }
        //            }
        //        }
        //        episodes.Add(episode);
        //    }
        //    return episodes;
        //}
    }
}