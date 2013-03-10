using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerienLoader.Model
{
   public class Hoster
   {
      private static readonly IDictionary<string,Hoster> Hosters = new Dictionary<string, Hoster>();

      private Hoster(string hosterName)
      {
         Name = hosterName;
      }

      private Hoster(){}

      public string Name { get; set; }

      public static Hoster GetHoster(string name)
      {
         Hoster result;
         if (Hosters.ContainsKey(name))
         {
            result = Hosters[name];
         }
         else
         {
            result = new Hoster(name);
         }
         return result;
      }
   }
}
