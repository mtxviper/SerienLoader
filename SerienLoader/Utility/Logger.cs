using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SerienLoader.Utility
{
public    static class Logger
    {
    public static IDictionary<DateTime,string> Entries { get; set; }
    public static string LogString { get; set; }
    private static StringBuilder stringBuilder;
    static Logger()
    {
        Entries = new Dictionary<DateTime, string>();
        stringBuilder = new StringBuilder();
    }

    public static void Log(string message)
    {
        DateTime now = DateTime.Now;
        stringBuilder.Insert(0, now.ToString(CultureInfo.InvariantCulture)+" "+message+"\r\n");
        LogString = stringBuilder.ToString();
       // Entries.Add(now,message);
    }

    }
}
