using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    class SearchHistory
    {
        XmlDocument xdoc = new XmlDocument();
        public void xmlSave(string bname, string publisher, string link, int i)
        {
            XmlNode root2 = xdoc.CreateElement("Baskets");
            if (i == 0)
            {
                
                xdoc.AppendChild(root2);
            }


            XmlNode root = xdoc.CreateElement("Basket");
            root2.AppendChild(root);
            
            XmlNode emp1 = xdoc.CreateElement("Name");
            emp1.InnerText = bname;
            root.AppendChild(emp1);

            XmlNode emp2 = xdoc.CreateElement("Publisher");
            emp2.InnerText = publisher;
            root.AppendChild(emp2);

            XmlNode emp3 = xdoc.CreateElement("Link");
            emp3.InnerText = link;
            root.AppendChild(emp3);

            // XML 파일 저장
            xdoc.Save(@"SearchHistory.xml");
        }
        internal void xmlLoad()
        {
            XmlReader reader = XmlReader.Create("SearchHistory.xml");

            while (reader.Read())
            {
                if (reader.IsStartElement("Basket"))
                {
                    //Member me = Member.remem(reader,memlist);
                    History hs = new History(string.Empty, string.Empty, string.Empty);
                    hs.makeHistory(reader, MainForm.hlist);
                }
            }
            reader.Close();
        }
    }
    public class History
    {
        public string BName
        {
            get; private set;
        }
        public string Publisher
        {
            get; private set;
        }
        public string Link
        {
            get; private set;
        }
        public History(string bname, string publisher, string link)
        {
            BName = bname;
            Publisher = publisher;
            Link = link;
        }
        internal void makeHistory(XmlReader xr, List<History> me)
        {
            string name = string.Empty;
            string publisher = string.Empty;
            string link = string.Empty;

            xr.ReadToDescendant("Name");
            name = xr.ReadElementString("Name");
            xr.ReadToNextSibling("Publisher");
            publisher = xr.ReadElementString("Publisher");
            xr.ReadToNextSibling("Link");
            link = xr.ReadElementString("Link");
            me.Add(AddHistory(name, publisher, link));
        }
        public History AddHistory(string n, string a, string l)
        {
            return new History(n, a, l);
        }
    }
}
