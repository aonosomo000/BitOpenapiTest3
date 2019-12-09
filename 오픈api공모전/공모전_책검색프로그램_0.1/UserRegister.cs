using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    public partial class UserRegister : Form
    {
        private XmlWriter xWriter;
        private XmlWriterSettings xSettings;
        XmlDocument doc = SearchUser.doc;
        public UserRegister()
        {
            InitializeComponent();
            xSettings = new XmlWriterSettings();
            xSettings.Indent = true;
            ComboInit();
            //ComboInitTwo();
        }
        public void AddUser()
        {
            try
            {
                string filename = "asdf.xml";

                xWriter = XmlWriter.Create(filename, xSettings);
                this.Text = filename;
                string name = string.Empty;
                int age = 0;
                string gender = string.Empty;
                string job = string.Empty;
                name = textBox1.Text;
               //  textBox2.Text = "0";
               // age = int.Parse(textBox2.Text);
                job = "Null";

                string hobby = string.Empty;
                string speciality = string.Empty;
                string full = string.Empty;
                string movie = string.Empty;
                string food = string.Empty;
                string trip = string.Empty;
                full = textBox7.Text; // 요즘 빠져 있는 것
                movie = textBox8.Text;
                food = textBox9.Text;
                trip = textBox10.Text;

                //xWriter.WriteComment("주석");
                xWriter.WriteStartElement("Userinfo");
                if ((textBox1.Text == ""))
                {
                    MessageBox.Show("필수정보 항목은 비울수 없습니다");
                    #region
                    xWriter.WriteStartElement("Name");
                    xWriter.WriteValue("Null");
                    xWriter.WriteEndElement();

                    xWriter.WriteStartElement("Age");
                    xWriter.WriteValue("0");
                    xWriter.WriteEndElement();
                    if (radioButton1.Checked == true)
                    {
                        gender = "남자";
                        xWriter.WriteStartElement("Gender");
                        xWriter.WriteValue(gender);
                        xWriter.WriteEndElement();
                    }
                    else if (radioButton2.Checked == true)
                    {
                        gender = "여자";
                        xWriter.WriteStartElement("Gender");
                        xWriter.WriteValue(gender);
                        xWriter.WriteEndElement();
                    }
                    xWriter.WriteStartElement("Job");
                    xWriter.WriteValue("Null");
                    xWriter.WriteEndElement();

                    
                    xWriter.WriteStartElement("Hobby");
                    xWriter.WriteValue("Null");
                    xWriter.WriteEndElement();
                    xWriter.WriteStartElement("Speciality");
                    xWriter.WriteValue("Null");
                    xWriter.WriteEndElement();
                    xWriter.WriteStartElement("Full");
                    xWriter.WriteValue("Null");
                    xWriter.WriteEndElement();
                    xWriter.WriteStartElement("Movie");
                    xWriter.WriteValue("Null");
                    xWriter.WriteEndElement();
                    xWriter.WriteStartElement("Food");
                    xWriter.WriteValue("Null");
                    xWriter.WriteEndElement();
                    xWriter.WriteStartElement("Trip");
                    xWriter.WriteValue("Null");
                    xWriter.WriteEndElement();
                    #endregion

                }
                else
                {
                    xWriter.WriteStartElement("Name");
                    xWriter.WriteValue(name);
                    xWriter.WriteEndElement();

                    xWriter.WriteStartElement("Age");
                    xWriter.WriteValue(age);
                    xWriter.WriteEndElement();
                    if (radioButton1.Checked == true)
                    {
                        gender = "남자";
                        xWriter.WriteStartElement("Gender");
                        xWriter.WriteValue(gender);
                        xWriter.WriteEndElement();
                    }
                    else if (radioButton2.Checked == true)
                    {
                        gender = "여자";
                        xWriter.WriteStartElement("Gender");
                        xWriter.WriteValue(gender);
                        xWriter.WriteEndElement();
                    }

                    xWriter.WriteStartElement("Job");
                    xWriter.WriteValue(job);
                    xWriter.WriteEndElement();

                    #region

                        xWriter.WriteStartElement("Hobby");
                        xWriter.WriteValue("Null");
                        xWriter.WriteEndElement();
                    xWriter.WriteStartElement("Speciality");
                    xWriter.WriteValue("Null");
                    xWriter.WriteEndElement();
                //if (textBox2.Text.Length > 0)
                //{
                //    xWriter.WriteStartElement("Hobby");
                //    xWriter.WriteValue(hobby);
                //    xWriter.WriteEndElement();
                //}
                //else

                //if (textBox3.Text.Length > 0)
                //{
                //    xWriter.WriteStartElement("Speciality");
                //    xWriter.WriteValue(speciality);
                //    xWriter.WriteEndElement();
                //}
                //else
                //{
                if (textBox7.Text.Length > 0)
                    {
                        xWriter.WriteStartElement("Full");
                        xWriter.WriteValue(full);
                        xWriter.WriteEndElement();
                    }
                    else
                    {
                        xWriter.WriteStartElement("Full");
                        xWriter.WriteValue("Null");
                        xWriter.WriteEndElement();
                    }
                    if (textBox8.Text.Length > 0)
                    {
                        xWriter.WriteStartElement("Movie");
                        xWriter.WriteValue(movie);
                        xWriter.WriteEndElement();
                    }
                    else
                    {
                        xWriter.WriteStartElement("Movie");
                        xWriter.WriteValue("Null");
                        xWriter.WriteEndElement();
                    }
                    if (textBox9.Text.Length > 0)
                    {
                        xWriter.WriteStartElement("Food");
                        xWriter.WriteValue(food);
                        xWriter.WriteEndElement();
                    }
                    else
                    {
                        xWriter.WriteStartElement("Food");
                        xWriter.WriteValue("Null");
                        xWriter.WriteEndElement();
                    }
                    if (textBox10.Text.Length > 0)
                    {
                        xWriter.WriteStartElement("Trip");
                        xWriter.WriteValue(trip);
                        xWriter.WriteEndElement();
                    }
                    else
                    {
                        xWriter.WriteStartElement("Trip");
                        xWriter.WriteValue("Null");
                        xWriter.WriteEndElement();
                    }
                    #endregion
                }
                xWriter.WriteEndElement();
            }
            catch (Exception)
            {
                //Console.WriteLine("{0} 추가 실패", accnumber);
                //Console.WriteLine("이유:{0}", e.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddUser();
            xWriter.Close();
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ComboInit()
        {
            for (int i = 1; i < 27; i++)
            {
                string str = MainForm.categoryList[i];
                str.Substring(5);
                comboBox2.Items.Add(str);
            }
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
  
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
