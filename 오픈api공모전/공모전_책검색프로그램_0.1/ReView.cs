using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    class ReView
    {
        public List<Blog> bloglist = new List<Blog>();


        public string XmlString
        {
            get; private set;
        }

        XmlDocument doc;

        public void BlogSearch(string str)
        {
            bloglist.Clear();
            XmlString = Find(str);
            doc = new XmlDocument();
            doc.LoadXml(XmlString);

            XmlNode node = doc.SelectSingleNode("rss");
            XmlNode n = node.SelectSingleNode("channel");
            Blog blog = null;
            foreach (XmlNode el in n.SelectNodes("item"))
            {
                blog = Blog.MakeBlog(el);
                bloglist.Add(blog);
            }
        }

        public string Find(string str)
        {
            try
            {
                string query = str; // 검색할 문자열
                string riveiw = "독후감, 감상문, 리뷰";
                // string riveiw1 = "";

                string url = "https://openapi.naver.com/v1/search/blog.xml?query="  + query + riveiw;  // 결과가 XML 포맷
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("X-Naver-Client-Id", "zZJhDkO2j5iOpHylEe04"); // 클라이언트 아이디
                request.Headers.Add("X-Naver-Client-Secret", "eeoms4kfL5");       // 클라이언트 시크릿
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string status = response.StatusCode.ToString();
                if (status == "OK")
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    string text = reader.ReadToEnd();
                    Console.WriteLine(text);
                    return text;
                }
                else
                {
                    return string.Format("Error 발생={0}", status);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

    }
}
