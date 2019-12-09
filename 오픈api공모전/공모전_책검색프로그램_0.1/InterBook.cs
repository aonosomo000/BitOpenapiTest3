using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    class InterBook
    {
        #region 프로퍼티 호출
        public string ItemId
        {
            get; private set;
        }
        public string Title
        {
            get; private set;
        }
        public string ISBN
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
        public string Description
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
        public string DiscountRate
        {
            get; private set;
        }
        public string Cover
        {
            get; private set;
        }
        public string CategoryId
        {
            get; private set;
        }
        public string CategoryName
        {
            get; private set;
        }
        public string Publisher
        {
            get; private set;
        }
        public string Pubdate
        {
            get; private set;
        }
        public string SaleStatus
        {
            get; private set;
        }
        public int Mileage
        {
            get; private set;
        }
        public string CustomerReviewRank
        {
            get; private set;
        }
        public int ReviewCount
        {
            get; private set;
        }
        #endregion
        static internal InterBook MakeBook(XmlNode xn, int sel)
        {
            string title = string.Empty;
            string isbn = string.Empty;
            string link = string.Empty;
            string author = string.Empty;
            string description = string.Empty;
            string itemId = string.Empty;
            int price = 0;
            int discount = 0;
            string discountRate = string.Empty;
            string cover = string.Empty;
            string categoryId = string.Empty;
            string categoryName = string.Empty;
            string publisher = string.Empty;
            string pubdate = string.Empty;
            string saleStatus = string.Empty;
            int mileage = 0;
            string custromReviewRank = string.Empty;
            int reviewCount = 0;

            XmlNode mileage_node = xn.SelectSingleNode("mileage");
            mileage = int.Parse(mileage_node.InnerText);
            XmlNode reviewCount_node = xn.SelectSingleNode("reviewCount");
            reviewCount = int.Parse(reviewCount_node.InnerText);
            XmlNode customerReviewRank_node = xn.SelectSingleNode("customerReviewRank");
            custromReviewRank = ConvertString(customerReviewRank_node.InnerText);
            XmlNode saleStatus_node = xn.SelectSingleNode("saleStatus");
            saleStatus = ConvertString(saleStatus_node.InnerText);
            XmlNode itemId_node = xn.SelectSingleNode("itemId");
            itemId = ConvertString(itemId_node.InnerText);
            XmlNode title_node = xn.SelectSingleNode("title");
            title = ConvertString(title_node.InnerText);
            XmlNode isbn_node = xn.SelectSingleNode("isbn");
            isbn = ConvertString(isbn_node.InnerText);
            XmlNode url_node = xn.SelectSingleNode("link");
            link = ConvertString(url_node.InnerText);
            XmlNode author_node = xn.SelectSingleNode("author");
            author = ConvertString(author_node.InnerText);
            XmlNode description_node = xn.SelectSingleNode("description");
            description = ConvertString(description_node.InnerText);
            XmlNode priceStandard_node = xn.SelectSingleNode("priceStandard");
            price = int.Parse(priceStandard_node.InnerText);
            XmlNode priceSales_node = xn.SelectSingleNode("priceSales");
            discount = int.Parse(priceSales_node.InnerText);
            XmlNode discountRate_node = xn.SelectSingleNode("discountRate");
            discountRate = ConvertString(discountRate_node.InnerText);
            XmlNode cover_node = xn.SelectSingleNode("coverLargeUrl");
            cover = ConvertString(cover_node.InnerText);
            XmlNode categoryId_node = xn.SelectSingleNode("categoryId");
            categoryId = ConvertString(categoryId_node.InnerText);
            XmlNode categoryName_node = xn.SelectSingleNode("categoryName");
            categoryName = ConvertString(categoryName_node.InnerText);
            XmlNode publisher_node = xn.SelectSingleNode("publisher");
            publisher = ConvertString(publisher_node.InnerText);
            XmlNode pubDate_node = xn.SelectSingleNode("pubDate");
            pubdate = ConvertString(pubDate_node.InnerText);

            return new InterBook(itemId, title, isbn, link, author, description, price, discount, discountRate, cover, categoryId, categoryName, publisher, pubdate, saleStatus, mileage, custromReviewRank, reviewCount);
        }
        internal InterBook(string itemId, string title, string isbn, string link, string author, string description, int price, int discount, string discountRate, string cover,  string categoryId, string categoryName, string publisher, string pubdate, string salestatus, int mileage, string crr, int rc)
        {
            ItemId = itemId;
            Title = title;
            ISBN = isbn;
            Link = link;
            Author = author;
            Description = description;
            Discount = discount;
            DiscountRate = discountRate;
            Price = price;
            Cover = cover;
            Publisher = publisher;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Pubdate = pubdate;
            SaleStatus = salestatus;
            Mileage = mileage;
            CustomerReviewRank = crr;
            ReviewCount = rc;
        }
        private static string ConvertString(string str)
        {
            return str;
        }
    }
}