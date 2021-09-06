using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestProject
{
    class XmlLinks : ITakeLinks
    {
        /// <summary>
        /// Create list with links
        /// </summary>
        /// <param name="link">Link for website</param>
        /// <returns>List with links in sitemap</returns>
        public List<string> GetLinks(string link)
        {

            WebClient client = new WebClient();
            XmlDocument document = new XmlDocument();
            List<string> resList = new List<string>();
            string sitemap = client.DownloadString(link);

            document.LoadXml(sitemap);

            XmlNodeList xmlList = document.GetElementsByTagName("loc");

            foreach (XmlNode item in xmlList)
            {
                resList.Add(item.InnerText);
            }
            return resList;
        }
    }
}
