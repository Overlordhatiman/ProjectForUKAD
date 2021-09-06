using System.Collections;

namespace TestProject
{
    class LinkInfo
    {
        public string Link { get; set; }
        public long Time { get; set; }

        public LinkInfo()
        {

        }
        public LinkInfo(string link, long time)
        {
            this.Link = link;
            this.Time = time;
        }

        public override string ToString()
        {
            return Link + " " + Time + " ms";
        }
    }
    class LinkInfoComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return (new CaseInsensitiveComparer()).Compare(((LinkInfo)x).Time, ((LinkInfo)y).Time);
        }
    }
}
