using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    class AladinBook
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
        public string Author
        {
            get; private set;
        }
        public string Pubdate
        {
            get; private set;
        }
        public string Description
        {
            get; private set;
        }
        public string ISBN
        {
            get; private set;
        }
        public int Discount
        {
            get; private set;
        }
        public int Price
        {
            get; private set;
        }
        public string Cover
        {
            get; private set;
        }
        public string Publisher
        {
            get; private set;
        }
        public string CategoryName
        {
            get; private set;
        }
        public int CategoryId
        {
            get; private set;
        }
        public int BestRank
        {
            get; private set;
        }
        #endregion
        static internal AladinBook MakeBook(XmlNode xn)
        {
            string title = string.Empty;
            string link = string.Empty;
            string author = string.Empty;
            string pubdate = string.Empty;
            string description = string.Empty;
            string isbn = string.Empty;
            int discount = 0;
            int price = 0;
            string cover = string.Empty;
            int categoryId = 0;
            string categoryName = string.Empty;
            string publisher = string.Empty;
            int bestRank = 0;

            XmlNode title_node = xn.SelectSingleNode("title");
            title = ConvertString(title_node.InnerText);
            XmlNode link_node = xn.SelectSingleNode("link");
            link = ConvertString(link_node.InnerText);
            XmlNode author_node = xn.SelectSingleNode("author");
            author = ConvertString(author_node.InnerText);
            XmlNode pubdate_node = xn.SelectSingleNode("pubDate");
            pubdate = ConvertString(pubdate_node.InnerText);
            XmlNode description_node = xn.SelectSingleNode("description");
            description = ConvertString(description_node.InnerText);
            XmlNode isbn_node = xn.SelectSingleNode("isbn");
            isbn = ConvertString(isbn_node.InnerText);
            XmlNode discount_node = xn.SelectSingleNode("priceSales");
            discount = int.Parse(discount_node.InnerText);
            XmlNode price_node = xn.SelectSingleNode("priceStandard");
            price = int.Parse(price_node.InnerText);
            XmlNode cover_node = xn.SelectSingleNode("cover");
            cover = ConvertString(cover_node.InnerText);
            XmlNode CategoryId_node = xn.SelectSingleNode("categoryId");
            categoryId = int.Parse(CategoryId_node.InnerText);
            XmlNode CategoryName_node = xn.SelectSingleNode("categoryName");
            categoryName = ConvertString(CategoryName_node.InnerText);
            XmlNode publisher_node = xn.SelectSingleNode("publisher");
            publisher = ConvertString(publisher_node.InnerText);
            XmlNode bestRank_node = null;
            if (xn.SelectSingleNode("customerReviewRank") != null)
            {
                bestRank = int.Parse(bestRank_node.InnerText);
            }

            return new AladinBook(title, link, pubdate, author, description, isbn, discount, price, cover, categoryId, categoryName, publisher, bestRank);
        }
        internal AladinBook(string title, string link,  string pubdate, string author, string description, string isbn, int discount, int price, string cover, int categoryId, string categoryName, string publisher, int bestRank)
        {
            Title = title;
            Link = link;
            Author = author;
            Pubdate = pubdate;
            Description = description;
            ISBN = isbn;
            Discount = discount;
            Price = price;
            Cover = cover;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Publisher = publisher;
            BestRank = bestRank;
        }
        private static string ConvertString(string str)
        {
            return str;
        }
    }
}
