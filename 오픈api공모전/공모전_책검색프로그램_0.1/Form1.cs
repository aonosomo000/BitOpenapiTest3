using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 공모전_책검색프로그램_0._1
{
    public partial class Form1 : Form
    {
        string bookLink = string.Empty;
        SearchBook sb = new SearchBook();
        ReView rv = new ReView();
        public Form1()
        {
            InitializeComponent();
            this.textBox1.Text = "";
            listView1.Columns.Add("책제목", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("출판사", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("저자", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("출간일", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("정가", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("할인가", 100, HorizontalAlignment.Left);
            comboBox1.SelectedIndex = 0;
        }
        private void TitlePrint()
        {
            ListViewItem lvi;

            listView1.Items.Clear();
            foreach (InterBook book in SearchBook.abld)
            {
                #region <b></b> 제거
                string titleA = book.Title.Replace("<b>", "");
                string titleB = titleA.Replace("</b>", "");
                string publisherA = book.Publisher.Replace("<b>", "");
                string publisherB = publisherA.Replace("</b>", "");
                string authorA = book.Author.Replace("<b>", "");
                string authorB = authorA.Replace("</b>", "");
                #endregion


                lvi = new ListViewItem(titleB);
                lvi.SubItems.Add(publisherB);
                lvi.SubItems.Add(authorB);
                lvi.SubItems.Add(book.Pubdate);
                lvi.SubItems.Add(book.Price.ToString());
                lvi.SubItems.Add(book.Discount.ToString());
                listView1.Items.Add(lvi);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    sb.BookSearch(textBox1.Text, 0);
                    break;
                case 1:
                    sb.BookSearch(textBox1.Text, 1);
                    break;
                case 2:
                    sb.BookSearch(textBox1.Text, 2);
                    break;
                case 3:
                    sb.BookSearch(textBox1.Text, 3);
                    break;
            }

            
            //리스트박스에 제목 출력
            TitlePrint();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = 0;
            if (listView1.SelectedIndices.Count > 0)
            {
                idx = listView1.SelectedIndices[0];
            }
            //책정보 출력
            string des1 = SearchBook.abld[idx].Description.Replace("<b>", "");
            textBox2.Text = des1.Replace("</b>", "");

            rv.BlogSearch(Selectbook());
            //리뷰출력
            if (rv.bloglist.Count >0 )
            {
                string des2 = rv.bloglist[0].Description.Replace("<b>", "");
                textBox3.Text = des2.Replace("&gt;", "");
                textBox3.Text = des2.Replace("&lt;", "");
                textBox3.Text = des2.Replace("&quot;", "");
                textBox3.Text = des2.Replace("</b>", "");
            }
            //이미지를 출력
            string filepath = SearchBook.abld[idx].Cover;
            bookLink = SearchBook.abld[idx].Link;
            WebClient Downloader = new WebClient();
            if (filepath.Length > 0)
            {
                try
                {
                    Stream ImageStream = Downloader.OpenRead(filepath);

                    Image DownloadImage = Image.FromStream(ImageStream);

                    pictureBox1.Image = DownloadImage;
                    label2.Hide();
                }
                catch (Exception)
                {

                }
            }
            else
            {
                label2.Show();
            }

            //Graphics gp = this.CreateGraphics();
            //gp.DrawImage(DownloadImage, 400, 10);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process process = new System.Diagnostics.Process();

            process.StartInfo = new System.Diagnostics.ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Internet Explorer\\iexplore.exe", bookLink);

            process.Start();

        }
        public string Selectbook()
        {
            int idx = 0;

            if (listView1.SelectedIndices.Count > 0)
            {
                idx = listView1.SelectedIndices[0];
            }

            string str = SearchBook.abld[idx].Title;

            return str;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //xml저장코드
        }
    }
}
