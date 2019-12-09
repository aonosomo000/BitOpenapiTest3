using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    class SearchUser
    {
        public static List<UserInfo> userlist = new List<UserInfo>();
        public static List<FCId> fcidlist = new List<FCId>();
        public static XmlDocument doc;
        public void userSearch()
        {
            doc = new XmlDocument();
            XmlReader reader = XmlReader.Create("asdf.xml");
            while (reader.Read())
            {
                if (reader.IsStartElement("Userinfo"))
                {
                    UserInfo ui = UserInfo.MakeUserInfo(reader);
                    userlist.Add(ui);
                }
            }
            reader.Close();
        }
        //public void fcidinitxmlSave(int count, int i)
        //{
        //    XmlNode root2 = doc.CreateElement("Search");
        //    XmlNode root = doc.CreateElement("CategoryId");
        //    if (i == 0)
        //    {
        //       doc.AppendChild(root2);
        //        root2.AppendChild(root);
        //    }



        //    XmlNode emp1 = doc.CreateElement("C" + i.ToString());
        //    emp1.InnerText = count.ToString();
        //    root.AppendChild(emp1);

        //    // XML 파일 저장
        //    doc.Save(@"SearchCategory.xml");
        //}
        public void fcidxmlSave(List<FCId> fcidlist)
        {
            try
            {
                XmlNode root = doc.CreateElement("Search");
                doc.AppendChild(root);

                XmlNode emp1 = doc.CreateElement("CategoryCount");
                for (int i = 100; i < 130; i++)
                {
                    // XmlNode emp1 = xdoc.CreateElement("CategoryCount");
                    var Count = doc.CreateElement("C" + i.ToString());
                    
                        Count.InnerText = fcidlist[i - 100].CCount.ToString();
                    //emp1.InnerText = count.ToString();
                    emp1.AppendChild(Count);
                    //root.AppendChild(emp1);
                    
                }
                for (int i = 200; i < 219; i++)
                {
                    // XmlNode emp1 = xdoc.CreateElement("CategoryCount");
                    var Count = doc.CreateElement("C" + i.ToString());
                    Count.InnerText = fcidlist[i - 170].CCount.ToString();
                    emp1.AppendChild(Count);
                }
                root.AppendChild(emp1);

                // XML 파일 저장
                doc.Save(@"SearchCategory.xml");

            }
            catch(Exception)
            {
            }

        }
        public void categorySearch()
        {
            doc = new XmlDocument();
            XmlReader reader = XmlReader.Create("SearchCategory.xml");
            while (reader.Read())
            {
                if (reader.IsStartElement("Search"))
                {
                        FCId.makeFcidlist(reader, fcidlist);
                }
            }
            reader.Close();
        }
        public void UserPrint()
        {
            foreach (UserInfo ui in userlist)
            {
                
                MessageBox.Show(ui.CName + "," + ui.Age);
            }
        }
    }
}
