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
    class SearchBookList
    {
        private static SearchBookList instance = new SearchBookList();
        public static List<InterBook> iblbs = new List<InterBook>();
        public static List<InterBook> iblnb = new List<InterBook>();
        public static List<InterBook> iblad = new List<InterBook>();
        public static List<AladinBook> ablbs = new List<AladinBook>();
        public static List<AladinBook> ablnb = new List<AladinBook>();

        private SearchBookList()
        {

        }
        public static SearchBookList getInstance()
        {
            return instance;
        }
        public string XmlString
        {
            get; private set;
        }
        XmlDocument doc;
        public void ListSearch(int ct, int list)
        {
            iblbs.Clear();
            iblnb.Clear();
            iblad.Clear();
            XmlString = this.InterFind(ct, list);
            doc = new XmlDocument();
            doc.LoadXml(XmlString);
            XmlNode node = doc.SelectSingleNode("channel");
            InterBook book = null;
            foreach (XmlNode el in node.SelectNodes("item"))
            {
                book = InterBook.MakeBook(el, list);
                if (list == 0)
                    iblbs.Add(book);
                else if (list == 1)
                    iblnb.Add(book);
                else if (list == 2)
                {
                    iblad.Add(book);
                }

            }
        }
        public string AladinFind(int ct)
        {
            try
            {

                //int category = ct;
                //int category = 1;
                string url = "http://www.aladin.co.kr/ttb/api/ItemSearch.aspx?ttbkey=ttbjinhee00921400001&Query=aladdin&QueryType=Title&SearchTarget=Book&start=1&maxResults=5&sort=salespoint&cover=MidBig&categoryId=1&output=xml&Version=20131101";  // 결과가 XML 포맷
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string status = response.StatusCode.ToString();
                if (status == "OK")
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    string text = reader.ReadToEnd();
                    Console.WriteLine(text);
                    MessageBox.Show(text);
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
        public string InterFind(int ct, int list)
        {
            try
            {
                string url = string.Empty;
                if (list == 0)
                {
                    url = "http://book.interpark.com/api/bestSeller.api?key=EDA9C96D20E5EFFEC91F77102185D3A1CD0E6D5ECAF03F803B1379BAD46DA57A&categoryId=" + ct + "&Cover=Big&MaxResults=10";  // 결과가 XML 포맷
                    //url = "http://www.aladin.co.kr/ttb/api/ItemList.aspx?ttbkey=ttbjinhee00921400001&QueryType=Bestseller&SearchTarget=Book&MaxResults=5&Cover=MidBig&CategoryId=&Version=20131101";
                    
                }
                else if (list == 1)
                {
                    url = "http://book.interpark.com/api/newBook.api?key=EDA9C96D20E5EFFEC91F77102185D3A1CD0E6D5ECAF03F803B1379BAD46DA57A&categoryId=" + ct + "&Cover=Big&MaxResults=10";  // 결과가 XML 포맷
                    //url = "http://www.aladin.co.kr/ttb/api/ItemList.aspx?ttbkey=ttbjinhee00921400001&QueryType=ItemNewAll&SearchTarget=Book&MaxResults=5&Cover=MidBig&CategoryId=1&Version=20131101";

                }
                else if (list == 2)
                {
                    url = "http://book.interpark.com/api/recommend.api?key=EDA9C96D20E5EFFEC91F77102185D3A1CD0E6D5ECAF03F803B1379BAD46DA57A&categoryId=" + ct + "&Cover=Big&MaxResults=5";  // 결과가 XML 포맷
                }
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
