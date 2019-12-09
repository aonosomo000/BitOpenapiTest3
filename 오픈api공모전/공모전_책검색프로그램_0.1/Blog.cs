using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    class Blog
    {
        #region 프로퍼티
        public string Title
        {
            get; private set;
        }
        public string Link
        {
            get; private set;
        }
        public string Description
        {
            get; private set;
        }
        public string Dloggername
        {
            get; private set;
        }
        public string Dloggerlink
        {
            get; private set;
        }
        #endregion

        #region 생성자
        internal Blog(string title, string link, string description, string dloggername, string dloggerlink)
        {
            Title = title;
            Link = link;
            Description = description;
            Dloggername = dloggername;
            Dloggerlink = dloggerlink;
        }
        #endregion

        #region 파서(xml문서 -> 객체화)
        static internal Blog MakeBlog(XmlNode xn)
        {
            string title = string.Empty;
            string link = string.Empty;
            string description = string.Empty;
            string dloggername = string.Empty;
            string dloggerlink = string.Empty;


            XmlNode title_node = xn.SelectSingleNode("title");
            title = ConverString(title_node.InnerText);

            XmlNode link_node = xn.SelectSingleNode("link");
            link = ConverString(link_node.InnerText);

            XmlNode description_node = xn.SelectSingleNode("description");
            description = ConverString(description_node.InnerText);

            XmlNode dloggername_node = xn.SelectSingleNode("bloggername");
            dloggername = ConverString(dloggername_node.InnerText);

            XmlNode dloggerlink_node = xn.SelectSingleNode("bloggerlink");
            dloggerlink = ConverString(dloggerlink_node.InnerText);

            return new Blog(title, link, description, dloggername, dloggerlink);
        }

        static private string ConverString(string str)
        {
            return str;
        }
        #endregion
    }
}