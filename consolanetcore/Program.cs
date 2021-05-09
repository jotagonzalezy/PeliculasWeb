using System;
using System.IO;
using System.Net;

namespace PeliculasWeb
{
  class Program
  {
    static void Main(string[] args)
    {
      string html = string.Empty;
      string web = "https://mati.seireshd.com/?v=";


      string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      for (var i = 1; i < 5; i++)
      {

        string url = web + i;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new(stream))
        {
          html = reader.ReadToEnd();
        }

        if (html.Length == 0)
          continue;

        int pFrom = html.IndexOf("<h3>");

        if (pFrom == -1) continue;

        int pTo = html.LastIndexOf("</h3>");

        string result = html.Substring(pFrom + 4, pTo - pFrom - 4);
        Console.WriteLine(i);
        File.AppendAllText(Path.Combine(docPath, "WriteFile.txt"), url + "\t" + result + Environment.NewLine);

      }
    }
  }
}