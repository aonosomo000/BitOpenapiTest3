using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{

    class UserInfo
    {
        
        #region 프로퍼티
        public string CName
        {
            get; set;
        }
        public int Age
        {
            get; set;
        }
        public string Gender
        {
            get; set;
        }
        public string Job
        {
            get; set;
        }
        public string Hobby
        {
            get; set;
        }
        public string Speciality
        {
            get; set;
        }
        public string Full
        {
            get; set;
        }
        public string Movie
        {
            get; set;
        }
        public string Food
        {
            get; set;
        }
        public string Trip
        {
            get; set;
        }
        #endregion
        public UserInfo(string cname, int age, string gender, string job, string hobby, string speciality, string full, string movie, string food, string trip)
        {
            CName = cname;
            Age = age;
            Gender = gender;
            Job = job;
            Hobby = hobby;
            Speciality = speciality;
            Full = full;
            Movie = movie;
            Food = food;
            Trip = trip;
        }
        static internal UserInfo MakeUserInfo(XmlReader xr)
        {
            string cname = string.Empty;
            int age = 0;
            string gender = string.Empty;
            string job = string.Empty;
            string hobby = string.Empty;
            string speciality = string.Empty;
            string full = string.Empty;
            string movie = string.Empty;
            string food = string.Empty;
            string trip = string.Empty;

            xr.ReadToDescendant("Name");
            cname = xr.ReadElementContentAsString();

            xr.ReadToNextSibling("Age");
            string str = xr.ReadElementContentAsString();
            age = int.Parse(str);

            xr.ReadToNextSibling("Gender");
            gender = xr.ReadElementContentAsString();

            xr.ReadToNextSibling("Job");
            job = xr.ReadElementContentAsString();

            xr.ReadToNextSibling("Hobby");
            hobby = xr.ReadElementContentAsString();

            xr.ReadToNextSibling("Speciality");
            speciality = xr.ReadElementContentAsString();

            xr.ReadToNextSibling("Full");
            full = xr.ReadElementContentAsString();

            xr.ReadToNextSibling("Movie");
            movie = xr.ReadElementContentAsString();

            xr.ReadToNextSibling("Food");
            food = xr.ReadElementContentAsString();

            xr.ReadToNextSibling("Trip");
            trip = xr.ReadElementContentAsString();


            return new UserInfo(cname, age, gender, job, hobby, speciality, full, movie, food, trip);
        }
        private static string ConvertString(string str)
        {
            return str;
        }
    }
    class FKW
    {
        public string Keyword
        {
            get; set;
        }
        public int KCount
        {
            get; set;
        }
        public FKW(string keyword, int kcount)
        {
            Keyword = keyword;
            KCount = kcount;
        }
        internal FKW makeFKW(XmlReader xr)
        {
            string keyword = string.Empty;
            int kcount = 0;

            return new FKW(keyword, kcount);
        }
    }
    class FCId
    {
        public int CCount
        {
            get; set;
        }
        public FCId(int ccount)
        {
            CCount = ccount;
        }
        internal FCId makeFCId(XmlReader xr, int i)
        {
            int ccount = 0;

            string categoryId = "C" + i.ToString();

            xr.ReadToDescendant("CategoryCount");
            ccount = xr.ReadElementContentAsInt();

            return new FCId(ccount);
        }
        static internal void makeFcidlist(XmlReader xr, List<FCId> fcidlist)
        {
            string keyword = string.Empty;


            //bool isLast = true;
            for (int i = 100; i < 130; i++ )
            {
                string str = "C" + i.ToString();

                if (i == 100)
                    xr.ReadToDescendant(str);
                else if (i == 106)
                {
                    str = "C" + (i + 1).ToString();
                    xr.ReadToNextSibling(str);
                    i++;
                }
                else if (i == 121)
                {
                    str = "C" + (i + 1).ToString();
                    xr.ReadToNextSibling(str);
                    i++;
                }
                else if (i == 127)
                {
                    str = "C" + (i + 1).ToString();
                    xr.ReadToNextSibling(str);
                    i++;
                }
                else
                {
                    xr.ReadToNextSibling(str);
                }
                keyword = xr.ReadElementContentAsString();
                FCId fcid = new FCId(int.Parse(keyword));
                fcidlist.Add(fcid);
            }
            for (int i = 200; i < 219; i++)
            {
                string str = "C" + i.ToString();

                if (i == 200)
                    xr.ReadToNextSibling(str);
                else
                {
                    xr.ReadToNextSibling(str);
                }
                keyword = xr.ReadElementContentAsString();
                if (i == 219)
                    break;
                FCId fcid = new FCId(int.Parse(keyword));
                fcidlist.Add(fcid);
            }
        }
    }
}
