using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    class SearchBook
    {
        public static List<InterBook> abld = new List<InterBook>();
        public string BookName
        {
            get; private set;
        }
        public string XmlString
        {
            get; private set;
        }
        XmlDocument doc;
        public void BookSearch(string str, int i)
        {
            abld.Clear();
            XmlString = InterBookFind(i, str);
            doc = new XmlDocument();
            doc.LoadXml(XmlString);
            //doc.Save("search.xml");

            XmlNode node = doc.SelectSingleNode("channel");
            InterBook book = null;
            foreach (XmlNode el in node.SelectNodes("item"))
            {
                book = InterBook.MakeBook(el, 0);
                abld.Add(book);
            }
        }
        public string Find(string str, int idx)
        {
            try
            {
                string funstr = "d_titl=";
                string query = str ; // 검색할 문자열
                                     //string url = "https://openapi.naver.com/v1/search/image?query=" + query; // 결과가 JSON 포맷
                switch (idx)
                {
                    case 0:
                        funstr = "d_titl="; break;
                    case 1:
                        funstr = "d_auth="; break;
                    case 2:
                        funstr = "d_isbn="; break;
                    case 3:
                        funstr = "d_publ="; break;
                }
                string url = "https://openapi.naver.com/v1/search/book_adv.xml?" + funstr + query  + "&display=100";  // 결과가 XML 포맷
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
            catch(Exception)
            {
                throw new Exception();
            }
        }
        public string InterBookFind(int ct, string str)
        {
            try
            {
                string queryType = "title";
                switch (ct)
                {
                    case 0:
                        queryType = "title"; break;
                    case 1:
                        queryType = "author"; break;
                    case 2:
                        queryType = "publisher"; break;
                    case 3:
                        queryType = "isbn"; break;
                    case 4:
                        queryType = "productNumber"; break;
                }
                string url = string.Empty;
                string query = str;
                url = "http://book.interpark.com/api/search.api?key=EDA9C96D20E5EFFEC91F77102185D3A1CD0E6D5ECAF03F803B1379BAD46DA57A&queryType=" + queryType + "&maxResults=20&sort=price&query=" + str; ;  // 결과가 XML 포맷
 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string status = response.StatusCode.ToString();
                if (status == "OK")
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    string text = reader.ReadToEnd();
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
