using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace TestProject
{
    class WebLinks : ITakeLinks
    {
        /// <summary>
        /// Create list with links
        /// </summary>
        /// <param name="link">Link for website</param>
        /// <returns>List with links on website</returns>
        public List<String> GetLinks(string link)
        {
            WebClient client = new WebClient();
            Queue<string> links = new Queue<string>();
            List<string> resList = new List<string>();

            links.Enqueue(link);

            try
            {
                while (links.Count > 0)
                {
                    string sitemap = client.DownloadString(links.Peek());
                    links.Dequeue();

                    HtmlDocument html = new HtmlDocument();
                    html.OptionReadEncoding = false;
                    html.LoadHtml(sitemap);

                    string takeLink = "//a[starts-with(@href,'" + link + "/')]";

                    resList.AddRange(html.DocumentNode
                    .SelectNodes(takeLink)
                    .Select(node => node.Attributes["href"].Value)
                    .ToList());
                    foreach (var item in resList)
                    {
                        if (!links.Contains(item))
                            links.Enqueue(item);
                    }
                    links.Distinct();
                    resList = resList.Distinct().ToList();

                }
            }
            catch (Exception e)
            {

            }
            return resList;
        }
    }
}
