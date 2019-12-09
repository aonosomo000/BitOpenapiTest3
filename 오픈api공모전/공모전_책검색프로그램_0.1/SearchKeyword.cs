using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    class SearchKeyword
    {

    }
    public class Keyword
    {
        public string Keyname
        {
            get; set;
        }
        public int Keycount
        {
            get; set;
        }

        public Keyword(string name, int count)
        {
            Keyname = name;
            Keycount = count;
        }
        internal void DataParse(XmlReader xr, List<Keyword> keylist)
        {
            string keyword = string.Empty;

            int i = 1;

            //bool isLast = true;
            while (true)
            {
                string str = "Index" + i.ToString();

                if (i == 1)
                    xr.ReadToDescendant(str);
                else
                {
                    xr.ReadToNextSibling(str);
                }
                keyword = xr.ReadElementContentAsString();
                if (keyword.Length <= 0)
                    break;
                i++;

                keylist.Add(Keysplit(keyword));
            }
        }

        public Keyword Keysplit(string msg)
        {
            string[] sp = msg.Split('#');
            string keyname = sp[0];
            int kcount = int.Parse(sp[1]);

            return new Keyword(keyname, kcount);
        }

        //이건 지금 왜 필요하지 모르지만 우선 만듬
        public override string ToString()
        {
            return Keyname + Keycount;
        }
    }
}
