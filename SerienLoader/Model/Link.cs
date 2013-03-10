namespace SerienLoader.Model
{
   public class Link
   {
 

      public Link(Hoster hoster, string url)
      {
         Hoster = hoster;
         Url = url;
      }


      public Hoster Hoster { get; set; }
      public string Url { get; set; }
      public string Format { get; set; }
      public string Size { get; set; }

      public string CombinedFormat
      {
         get { return Format+" "+Size; }
      }
   }
}