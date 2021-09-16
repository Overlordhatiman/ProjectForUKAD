using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject
{
    class App
    {
        AppManager app;
        List<String> listFromWebsite;
        List<String> listFromSitemap;
        string link;

        public App()
        {
            app = new AppManager();
        }

        public void Run()
        {
            app.PrintTextInConsole("Enter the link", OutputMode.SkipLine);

            link = Console.ReadLine();

            this.listFromWebsite = app.GetLinks(new WebLinks(), link);
            this.listFromSitemap = app.GetLinks(new XmlLinks(), link + "/sitemap.xml");

            app.PrintTextInConsole("URLs in sitemap but not after crawling", OutputMode.SkipLine);
            
            var mapNotWeb = listFromSitemap.Except(listFromWebsite).ToList();

            app.PrintTextInConsole(mapNotWeb);

            app.PrintTextInConsole("URLs by crawling but not in sitemp", OutputMode.SkipLine);

            var webNotMap = listFromWebsite.Except(listFromSitemap).ToList();

            app.PrintTextInConsole(webNotMap);

            app.PrintTextInConsole("URLs with response time", OutputMode.SkipLine);

            string[] allArr = listFromWebsite.Union(listFromSitemap).ToArray();
            
            List<LinkInfo> sortArr = new List<LinkInfo>(allArr.Length);

            Parallel.For(0, allArr.Length, i =>
            {
                sortArr.Add(new LinkInfo(allArr[i], app.Response(allArr[i])));
            });

            sortArr.Sort((x, y) => x.Time.CompareTo(y.Time));

            app.PrintTextInConsole(sortArr);

            app.PrintTextInConsole("Number of URLs by crawling " + listFromWebsite.Count, OutputMode.SkipLine);
            app.PrintTextInConsole("Number of URLs in sitemp " + listFromSitemap.Count, OutputMode.SkipLine);

            Console.ReadLine();
        }
    }
}
