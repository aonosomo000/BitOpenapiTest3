using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    class Book
    {
        #region 프로퍼티 호출
        public string Title
        {
            get; private set;
        }
        public string Link
        {
            get; private set;
        }
        public string Image
        {
            get; private set;
        }
        public string Author
        {
            get; private set;
        }
        public int Price
        {
            get; private set;
        }
        public int Discount
        {
            get; private set;
        }
        public string Publisher
        {
            get; private set;
        }
        public string ISBN
        {
            get; private set;
        }
        public string Description
        {
            get; private set;
        }
        public string Pubdate
        {
            get; private set;
        }
        #endregion


        static internal Book MakeBook(XmlNode xn)
        {
            string title = string.Empty;
            string link = string.Empty;
            string image = string.Empty;
            string author = string.Empty;
            int price = 0;
            int discount = 0;
            string publisher = string.Empty;
            string isbn = string.Empty;
            string description = string.Empty;
            string pubdate = string.Empty;

            XmlNode title_node = xn.SelectSingleNode("title");
            title = ConvertString(title_node.InnerText);
            XmlNode link_node = xn.SelectSingleNode("link");
            link = ConvertString(link_node.InnerText);
            XmlNode image_node = xn.SelectSingleNode("image");
            image = ConvertString(image_node.InnerText);
            XmlNode author_node = xn.SelectSingleNode("author");
            author = ConvertString(author_node.InnerText);
            XmlNode price_node = xn.SelectSingleNode("price");
            price = int.Parse(price_node.InnerText);
            XmlNode discount_node = xn.SelectSingleNode("discount");
            discount = int.Parse(discount_node.InnerText);
            XmlNode publisher_node = xn.SelectSingleNode("publisher");
            publisher = ConvertString(publisher_node.InnerText);
            XmlNode pubdate_node = xn.SelectSingleNode("pubdate");
            pubdate = ConvertString(pubdate_node.InnerText);
            XmlNode isbn_node = xn.SelectSingleNode("isbn");
            isbn = ConvertString(isbn_node.InnerText);
            XmlNode description_node = xn.SelectSingleNode("description");
            description = ConvertString(description_node.InnerText);

            
            return new Book(title, link, image, author, price, discount, publisher, pubdate, isbn, description);
        }
        internal Book(string t, string l, string img, string a,int pri, int d, string pub, string pud, string isbn, string des)
        {
            Title = t;
            Link = l;
            Image = img;
            Author = a;
            Price = pri;
            Discount = d;
            Publisher = pub;
            ISBN = isbn;
            Description = des;
            Pubdate = pud;
        }

        private static string ConvertString(string str)
        {
            return str;
        }
    }
}
