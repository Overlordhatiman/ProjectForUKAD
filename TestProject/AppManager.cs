using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace TestProject
{
    enum OutputMode
    {
        SkipLine,
        WithoutSkip
    }
    class AppManager
    {
        internal void PrintTextInConsole(List<LinkInfo> list)
        {
            Console.WriteLine("------------- OUTPUT START --------------");
            foreach (var item in list)
            {
                Console.WriteLine("{0,10}   |{1,10} ms", item.Link, item.Time);
            }
            Console.WriteLine("------------- OUTPUT END --------------");
        }
        internal void PrintTextInConsole(List<string> list)
        {
            Console.WriteLine("------------- OUTPUT START --------------");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("------------- OUTPUT END --------------");
        }
        internal void PrintTextInConsole(string message, OutputMode output)
        {
            switch (output)
            {
                case OutputMode.SkipLine:
                    Console.WriteLine();
                    Console.WriteLine(message);
                    Console.WriteLine();
                    break;
                case OutputMode.WithoutSkip:                    
                    Console.WriteLine(message);
                    break;
            }
        }

        internal List<String> GetLinks(ITakeLinks links, string link)
        {
            List<String> list = null;
            if (links != null)
            {
                list = links.GetLinks(link);
            }

            return list;
        }

        internal long Response(string link)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                timer.Stop();
                response.Close();
            }
            catch (Exception e)
            {

            }
            return timer.ElapsedMilliseconds;
        }
    }
}
