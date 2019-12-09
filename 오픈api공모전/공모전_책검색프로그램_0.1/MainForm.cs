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
using System.Xml;

namespace 공모전_책검색프로그램_0._1
{
    public partial class MainForm : Form
    {
        #region 데이터분석용 
        string k1 = string.Empty;
        string k2 = string.Empty;
        string k3 = string.Empty;
        string kn1 = string.Empty;
        string kn2 = string.Empty;
        string kn3 = string.Empty;
        string kn4 = string.Empty;
        int kb1 = 0;
        int kb2 = 0;
        int kb3 = 0;
        int kb4 = 0;
        string c1 = string.Empty;
        string c2 = string.Empty;
        string c3 = string.Empty;
        string cn1 = string.Empty;
        string cn2 = string.Empty;
        string cn3 = string.Empty;
        string cn4 = string.Empty;
        int cb1 = 0;
        int cb2 = 0;
        int cb3 = 0;
        int cb4 = 0;
        #endregion
        #region
        public static string[] categoryList = new string[]
            {
                "국내도서"
                ,"국내도서>소설"
                ,"국내도서>시/에세이"
                ,"국내도서>예술/대중문화"
                ,"국내도서>사회과학"
                ,"국내도서>역사와 문화"
                ,"국내도서>잡지"
                ,"국내도서>만화"
                ,"국내도서>유아"
                ,"국내도서>아동"
                ,"국내도서>가정과 생활"
                ,"국내도서>청소년"
                ,"국내도서>초등학습서"
                ,"국내도서>고등학습서"
                ,"국내도서>국어/외국어/사전"
                ,"국내도서>자연과 과학"
                ,"국내도서>경제경영"
                ,"국내도서>자기계발"
                ,"국내도서>인문"
                ,"국내도서>종교/역학"
                ,"국내도서>컴퓨터/인터넷"
                ,"국내도서>자격서/수험서"
                ,"국내도서>취미/레저"
                ,"국내도서>전공도서/대학교재"
                ,"국내도서>건강/뷰티"
                ,"국내도서>여행"
                ,"국내도서>중등학습서"
                ,"외국도서"
                ,"외국도서>어린이"
                ,"외국도서>ELT/사전"
                ,"외국도서>문학"
                ,"외국도서>경영/인문"
                ,"외국도서>예술/디자인"
                ,"외국도서>실용"
                ,"외국도서>해외잡지"
                ,"외국도서>대학교재/전문서적"
                ,"외국도서>컴퓨터"
                ,"외국도서>일본도서"
                ,"외국도서>프랑스도서"
                ,"외국도서>중국도서"
                ,"외국도서>해외주문원서"
            };
        #endregion
        int lv2idx1 = 0;
        int lv2idx = 0;
        string bookCategory = string.Empty;
        string bookName = string.Empty;
        string bookPublisher = string.Empty;
        string bookLink = string.Empty;
        SearchBook sb = new SearchBook();
        ReView rv = new ReView();
        Process process = new System.Diagnostics.Process();
        WebClient Downloader = new WebClient();
        SearchBookList sbl = SearchBookList.getInstance();
        private List<Image> bimageList = new List<Image>();
        private List<Image> nimageList = new List<Image>();
        private List<Image> rimageList = new List<Image>();
        public static List<History> hlist = new List<History>();
        public static List<Keyword> keylist = new List<Keyword>();
        private XmlWriterSettings xSettings;
        private XmlWriter xWriter;
        SearchUser su = new SearchUser();
        SearchHistory sh = new SearchHistory();
       
        AdBook adbook;
        public int GetCategoryId
        {
            get; set;
        }
        public MainForm()
        {
            InitializeComponent();
            xSettings = new XmlWriterSettings();
            xSettings.Indent = true;
            su.categorySearch();
            FileLoadKey();
            sh.xmlLoad();
            list();
            tabInit();
            dataAnalyze();
            ComboboxInit();
            this.tabPage1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            //GetCategoryId = 102;
            //sbl.ListSearch(GetCategoryId, 0); //베스트셀러
            //BestPrint();
            //sbl.ListSearch(GetCategoryId, 1); //신간도서
            //NewPrint();
            //sbl.ListSearch(GetCategoryId, 2);
            //RecommentPrint();
            datainb();
            //MessageBox.Show(SearchBookList.iblbs.Count.ToString());
            datainn();
            datainr();
            su.userSearch();
        }
        public void list()
        {
            listView1.Items.Clear();

            ListViewItem lvi;

            foreach (History hs in hlist)
            {
                lvi = new ListViewItem(hs.BName);
                lvi.SubItems.Add(hs.Publisher);
                lvi.SubItems.Add(hs.Link);

                listView1.Items.Add(lvi);
            }
        }
        void topInit()
        {
        }
        void ComboboxInit()
        {
            comboBox1.Items.Add(cn1);
            comboBox1.Items.Add(cn2);
            comboBox1.Items.Add(cn3);
            comboBox2.Items.Add(cn1);
            comboBox2.Items.Add(cn2);
            comboBox2.Items.Add(cn3);
            comboBox3.Items.Add(cn1);
            comboBox3.Items.Add(cn2);
            comboBox3.Items.Add(cn3);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedItem = 1;
            comboBox3.SelectedItem = 2;
        }
        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            panel2.Location = new System.Drawing.Point(panel2.Location.X, tabPage1.VerticalScroll.Value);
            panel2.Invalidate();
        }
        private void GetAdCover()
        {
            int i=0;
            foreach (InterBook ib in SearchBookList.iblad)
            {
                string filepath = ib.Cover;
                switch (i)
                {
                    case 0:
                        adbook = new AdBook(filepath, ib.Link);
                        adbook.TopLevel = true;
                        adbook.StartPosition = FormStartPosition.CenterParent;
                        adbook.Show(this);
                        break;
                    case 1:
                        adbook = new AdBook(filepath, ib.Link);
                        adbook.TopLevel = true;
                        adbook.StartPosition = FormStartPosition.CenterParent;
                        adbook.Show(this);
                        break;
                }
                i++;
            }
        }
        public void SetPrintImage(string cname, string textboxname, string bookname, Image filepath, int i)

        {
            
            bool bContinue= true;
            string groupName = string.Empty;
            groupName = "groupBox" + i;

            for (int j = 0; j < this.panel1.Controls.Count; j++)   // Panel안에 Button들을 찾는다.
            {
                if (this.panel1.Controls[j].Name.Equals(groupName))
                {
                    for (int s = 0; s < this.panel1.Controls[j].Controls.Count; s++)
                    {
                        if (this.panel1.Controls[j].Controls[s].Name.Equals(textboxname))
                        {
                            this.panel1.Controls[j].Controls[s].Text = bookname;
                        }
                        if (this.panel1.Controls[j].Controls[s].Name.Equals(cname))
                        {
                            this.panel1.Controls[j].Controls[s].BackgroundImage = filepath; //

                            bContinue = false;

                            return;
                        }
                    }

                    if (!bContinue) return;
                }
            }

        }
        private void BestPrint()
        {
            int i = 1;
            bimageList.Clear();
            foreach (InterBook book in SearchBookList.iblbs)
            {
                
                string filepath = book.Cover;
                Stream ImageStream = Downloader.OpenRead(filepath);
                Image DownloadImage = Image.FromStream(ImageStream);
                bimageList.Add(DownloadImage);
                string cname = string.Empty;
                cname = "pictureBox" + i;
                string tname = string.Empty;
                tname = "textBox" + i;
                switch(i)
                {
                    case 1:
                        textBox1.Text = book.Title;break;
                    case 2:
                        textBox2.Text = book.Title; break;
                    case 3:
                        textBox3.Text = book.Title; break;
                    case 4:
                        textBox4.Text = book.Title; break;
                    case 5:
                        textBox5.Text = book.Title; break;
                    case 6:
                        textBox6.Text = book.Title; break;
                }
                SetPrintImage(cname, tname, book.Title, DownloadImage, 1);
                i++;
            }
        }
        private void NewPrint()
        {
            int i = 11;
            nimageList.Clear();
            foreach (InterBook book in SearchBookList.iblnb)
            {
                string filepath = book.Cover;
                Stream ImageStream = Downloader.OpenRead(filepath);
                Image DownloadImage = Image.FromStream(ImageStream);

                nimageList.Add(DownloadImage);
                string cname = string.Empty;
                cname = "pictureBox" + i;
                string tname = "textBox" + i;
                SetPrintImage(cname, tname, book.Title, DownloadImage, 2);

                i++;
            }
        }
        private void RecommentPrint()
        {
            int i = 21;
            rimageList.Clear();
            foreach (InterBook book in SearchBookList.iblad)
            {
                string filepath = book.Cover;
                Stream ImageStream = Downloader.OpenRead(filepath);
                Image DownloadImage = Image.FromStream(ImageStream);

                rimageList.Add(DownloadImage);
                string cname = string.Empty;
                cname = "pictureBox" + i;
                string tname = "textBox" + i;
                switch (i)
                {
                    case 21:
                        textBox21.Text = book.Title; break;
                    case 22:
                        textBox27.Text = book.Title; break;
                    case 23:
                        textBox28.Text = book.Title; break;
                    case 24:
                        textBox29.Text = book.Title; break;
                    case 25:
                        textBox30.Text = book.Title; break;
                }
                SetPrintImage(cname, tname, book.Title, DownloadImage, 3);
                i++;
            }
        }
        private void 상세검색SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }
        private void 프로그램종료EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void 사용자정보등록하기SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserRegister ur = new UserRegister();
            ur.ShowDialog();
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }
        #region 표지 검색
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
            //Image DownloadImage = imageList[0];
            //pictureBox26.Image = DownloadImage;
        }
        private void click_picture(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
            Control ctrl = sender as Control;
            string pbName = ctrl.Name;
            for (int i = 0; i < this.groupBox1.Controls.Count; i++)
            {
                if (this.groupBox1.Controls[i].Name.Equals(pbName))
                {
                    try
                    {

                        int parstr = int.Parse(pbName.Substring(10));
                        Image DownloadImage = bimageList[parstr - 1];
                        this.pictureBox26.Image = DownloadImage;
                        textBox22.Text = SearchBookList.iblbs[parstr - 1].Title;
                        textBox36.Text = SearchBookList.iblbs[parstr - 1].CategoryName;
                        textBox25.Text = SearchBookList.iblbs[parstr - 1].Author;
                        textBox32.Text = "정가: " + SearchBookList.iblbs[parstr - 1].Price.ToString()
                            + "-할인율(" + SearchBookList.iblbs[parstr - 1].DiscountRate + "%)";
                        textBox33.Text = "할인가: " + SearchBookList.iblbs[parstr - 1].Discount.ToString();
                        textBox35.Text = "구매시 지급되는 마일리지: " + SearchBookList.iblbs[parstr - 1].Mileage.ToString();
                        textBox34.Text = "판매상태: " + SearchBookList.iblbs[parstr - 1].SaleStatus;
                        textBox31.Text = SearchBookList.iblbs[parstr - 1].Description;
                        textBox24.Text = "상품번호: " + SearchBookList.iblbs[parstr - 1].ItemId.ToString();
                        textBox37.Text = SearchBookList.iblbs[parstr - 1].Publisher;
                        textBox38.Text = "출간일: " + SearchBookList.iblbs[parstr - 1].Pubdate;
                        textBox23.Text = "ISBN: " + SearchBookList.iblbs[parstr - 1].ISBN.ToString();
                        if (int.Parse(SearchBookList.iblbs[parstr - 1].CategoryId) < 130)
                            SearchUser.fcidlist[int.Parse(SearchBookList.iblbs[parstr - 1].CategoryId) - 100].CCount += 1;
                        if (int.Parse(SearchBookList.iblbs[parstr - 1].CategoryId) >= 200)
                            SearchUser.fcidlist[int.Parse(SearchBookList.iblbs[parstr - 1].CategoryId) - 170].CCount += 1;
                    }
                    catch (Exception)
                    {

                    }
                    }
            }
            for (int i = 0; i < this.groupBox2.Controls.Count; i++)
            {
                if (this.groupBox2.Controls[i].Name.Equals(pbName))
                {
                    try
                    {

                        int parstr = int.Parse(pbName.Substring(10));
                        Image DownloadImage = nimageList[parstr - 11];
                        this.pictureBox26.Image = DownloadImage; textBox22.Text = SearchBookList.iblnb[parstr - 11].Title;
                        textBox36.Text = SearchBookList.iblnb[parstr - 11].CategoryName;
                        textBox25.Text = SearchBookList.iblnb[parstr - 11].Author;
                        textBox32.Text = "정가: " + SearchBookList.iblnb[parstr - 11].Price.ToString()
                            + "-할인율(" + SearchBookList.iblnb[parstr - 11].DiscountRate + "%)";
                        textBox33.Text = "할인가: " + SearchBookList.iblnb[parstr - 11].Discount.ToString();
                        textBox35.Text = "구매시 지급되는 마일리지: " + SearchBookList.iblnb[parstr - 1].Mileage.ToString();
                        textBox34.Text = "판매상태: " + SearchBookList.iblnb[parstr - 11].SaleStatus;
                        textBox31.Text = SearchBookList.iblnb[parstr - 11].Description;
                        textBox24.Text = "상품번호: " + SearchBookList.iblnb[parstr - 11].ItemId.ToString();
                        textBox37.Text = SearchBookList.iblnb[parstr - 11].Publisher;
                        textBox38.Text = "출간일: " + SearchBookList.iblnb[parstr - 11].Pubdate;
                        textBox23.Text = "ISBN: " + SearchBookList.iblnb[parstr - 11].ISBN.ToString();
                        if (int.Parse(SearchBookList.iblnb[parstr - 11].CategoryId) < 130)
                            SearchUser.fcidlist[int.Parse(SearchBookList.iblnb[parstr - 11].CategoryId) - 100].CCount += 1;
                        if (int.Parse(SearchBookList.iblnb[parstr - 11].CategoryId) >= 200)
                            SearchUser.fcidlist[int.Parse(SearchBookList.iblnb[parstr - 11].CategoryId) - 170].CCount += 1;
                    }
                    catch (Exception)
                    {

                    }
                    }

            }
            for (int i = 0; i < this.groupBox3.Controls.Count; i++)
            {
                if (this.groupBox3.Controls[i].Name.Equals(pbName))
                {
                    try
                    {

                        string parstr = pbName.Substring(10);
                        Image DownloadImage = rimageList[int.Parse(parstr) - 21];
                        this.pictureBox26.Image = DownloadImage; textBox22.Text = SearchBookList.iblad[int.Parse(parstr) - 21].Title;
                        textBox36.Text = SearchBookList.iblad[int.Parse(parstr) - 21].CategoryName;
                        textBox25.Text = SearchBookList.iblad[int.Parse(parstr) - 21].Author;
                        textBox32.Text = "정가: " + SearchBookList.iblad[int.Parse(parstr) - 21].Price.ToString()
                            + "-할인율(" + SearchBookList.iblad[int.Parse(parstr) - 21].DiscountRate + "%)";
                        textBox33.Text = "할인가: " + SearchBookList.iblad[int.Parse(parstr) - 21].Discount.ToString();
                        textBox35.Text = "구매시 지급되는 마일리지: " + SearchBookList.iblad[int.Parse(parstr) - 21].Mileage.ToString();
                        textBox34.Text = "판매상태: " + SearchBookList.iblad[int.Parse(parstr) - 21].SaleStatus;
                        textBox31.Text = SearchBookList.iblad[int.Parse(parstr) - 21].Description;
                        textBox24.Text = "상품번호: " + SearchBookList.iblad[int.Parse(parstr) - 21].ItemId.ToString();
                        textBox37.Text = SearchBookList.iblad[int.Parse(parstr) - 21].Publisher;
                        textBox38.Text = "출간일: " + SearchBookList.iblad[int.Parse(parstr) - 21].Pubdate;
                        textBox23.Text = "ISBN: " + SearchBookList.iblad[int.Parse(parstr) - 21].ISBN.ToString();
                        if (int.Parse(SearchBookList.iblad[int.Parse(parstr) - 21].CategoryId) < 130)
                            SearchUser.fcidlist[int.Parse(SearchBookList.iblad[int.Parse(parstr) - 21].CategoryId) - 100].CCount += 1;
                        if (int.Parse(SearchBookList.iblad[int.Parse(parstr) - 21].CategoryId) >= 200)
                            SearchUser.fcidlist[int.Parse(SearchBookList.iblad[int.Parse(parstr) - 21].CategoryId) - 170].CCount += 1;
                    }
                    catch (Exception)
                    {

                    }
                    }
            }
        }
        #endregion
        private void tabPage1_Scroll(object sender, ScrollEventArgs e)
        {
            panel2.Location = new System.Drawing.Point(panel2.Location.X,tabPage1.VerticalScroll.Value);
            tabPage1.Update();
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Process process = new System.Diagnostics.Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Internet Explorer\\iexplore.exe", hlist[
            lv2idx1].Link);

            process.Start();
        }

        #region 키워드검색 페이지
        public void tabInit()
        {
            this.textBox40.Text = "";
            listView2.Columns.Add("책제목", 100, HorizontalAlignment.Left);
            listView2.Columns.Add("출판사", 100, HorizontalAlignment.Left);
            listView2.Columns.Add("저자", 100, HorizontalAlignment.Left);
            listView2.Columns.Add("출간일", 100, HorizontalAlignment.Left);
            listView2.Columns.Add("정가", 100, HorizontalAlignment.Left);
            listView2.Columns.Add("할인가", 100, HorizontalAlignment.Left);
            comboBox4.SelectedIndex = 0;
        }
        public string Selectbook()
        {
            int idx = 0;

            if (listView2.SelectedIndices.Count > 0)
            {
                idx = listView2.SelectedIndices[0];
            }

            string str = SearchBook.abld[idx].Title;

            return str;
        }

        private void TitlePrint()
        {
            ListViewItem lvi;

            listView2.Items.Clear();
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
                listView2.Items.Add(lvi);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    sb.BookSearch(textBox40.Text, 0);
                    break;
                case 1:
                    sb.BookSearch(textBox40.Text, 1);
                    break;
                case 2:
                    sb.BookSearch(textBox40.Text, 2);
                    break;
                case 3:
                    sb.BookSearch(textBox40.Text, 3);
                    break;
            }
            selectKey(textBox40);

            //리스트박스에 제목 출력
            TitlePrint();
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lv2idx = 0;
            if (listView2.SelectedIndices.Count > 0)
            {
                lv2idx = listView2.SelectedIndices[0];
            }
            //책정보 출력
            string des1 = SearchBook.abld[lv2idx].Description.Replace("<b>", "");
            textBox39.Text = des1.Replace("</b>", "");

            rv.BlogSearch(Selectbook());
            //리뷰출력
            if (rv.bloglist.Count > 0)
            {
                string des2 = rv.bloglist[0].Description.Replace("<b>", "");
                textBox26.Text = des2.Replace("&gt;", "");
                textBox26.Text = des2.Replace("&lt;", "");
                textBox26.Text = des2.Replace("&quot;", "");
                textBox26.Text = des2.Replace("</b>", "");
            }
            //이미지를 출력
            string filepath = SearchBook.abld[lv2idx].Cover;
            bookLink = SearchBook.abld[lv2idx].Link;
            bookName = SearchBook.abld[lv2idx].Title;
            bookPublisher = SearchBook.abld[lv2idx].Publisher;
            bookCategory = SearchBook.abld[lv2idx].CategoryId;
            WebClient Downloader = new WebClient();
            if (filepath.Length > 0)
            {
                try
                {
                    Stream ImageStream = Downloader.OpenRead(filepath);

                    Image DownloadImage = Image.FromStream(ImageStream);

                    pictureBox27.Image = DownloadImage;
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

        }
        private void pictureBox27_Click(object sender, EventArgs e)
        {
            Process process = new System.Diagnostics.Process();

            process.StartInfo = new System.Diagnostics.ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Internet Explorer\\iexplore.exe", bookLink);

            process.Start();
            History hs = new History(bookName, bookPublisher, bookLink);
            int idx = int.Parse(bookCategory);
            SearchUser.fcidlist[idx-100].CCount += 1;
            hlist.Add(hs);
            list();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lv2idx1 = 0;
            if (listView1.SelectedIndices.Count > 0)
            {
                lv2idx1 = listView1.SelectedIndices[0];
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int idx = listView1.SelectedIndices[0];
                ContextMenu m = new ContextMenu();
                MenuItem m1 = new MenuItem();
                m1.Text = "기록삭제"; m.MenuItems.Add(m1); m.Show(listView1, new Point(e.X, e.Y));

                m1.Click += (senders, es) =>
                { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.
                    hlist.RemoveAt(idx);
                    list();
                };
            }
        }
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            su.fcidxmlSave(SearchUser.fcidlist);
            FileUpdate();
            xWriter.Close();
            int i = 0;
            foreach (History hs in hlist)
            {
                sh.xmlSave(hs.BName, hs.Publisher, hs.Link, i);
                i++;
            }
        }
        private void FileLoadKey()
        {
            XmlReader reader = XmlReader.Create("SearchKeyword.xml");

            while (reader.Read())
            {
                if (reader.ReadToDescendant("KeyWord"))
                {
                    Keyword Key = new Keyword(string.Empty, 0);
                    Key.DataParse(reader, keylist);
                }
            }
            reader.Close();
        }
        public void selectKey(Control c)
        {
            bool check = true;
            
            string key = c.Text;
            foreach (Keyword k in keylist)
            {
                if (k.Keyname == key)
                {
                    k.Keycount += 1;
                    check = false;
                    //MessageBox.Show(k.Keyname);
                }
            }
            if (check == true)
            {
                Keyword ky = new Keyword(key, 1);
                keylist.Add(ky);
            }
        }
        private void FileUpdate()
        {
            string filename = "SearchKeyword.xml";
            xWriter = XmlWriter.Create(filename, xSettings);

            int count = 1;

            xWriter.WriteStartElement("search");
            xWriter.WriteStartElement("KeyWord");

            for (int i = 0; i < keylist.Count; i++)
            {
                string idx = "Index" + count.ToString();
                string blank = "Index" + (count + 1).ToString();
                xWriter.WriteStartElement(idx);
                xWriter.WriteValue(keylist[i].Keyname + "#" + keylist[i].Keycount);
                xWriter.WriteEndElement();
                count++;
                if (i == keylist.Count - 1)
                {
                    xWriter.WriteStartElement(blank);
                    xWriter.WriteValue("");
                    xWriter.WriteEndElement();
                }
            }
            xWriter.WriteEndElement();
            xWriter.WriteEndElement();
        }
        void dataAnalyze()
        {
           
            foreach (Keyword ky in keylist)
            {
                    kb4 = ky.Keycount;
                    
                    if (kb4 >= kb3)
                    {
                    kb4 = kb3;
                    kn4 = kn3;
                    kb3 = ky.Keycount;
                    kn3 = ky.Keyname;
                        if (kb3 >= kb2)
                        {
                        kb3 = kb2;
                        kn3 = kn2;
                        kb2 = ky.Keycount;
                        kn2 = ky.Keyname;
                            if (kb2 >= kb1)
                            {
                            kb2 = kb1;
                            kn2 = kn1;
                            kb1 = ky.Keycount;
                            kn1 = ky.Keyname;
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                    }
                }
            for (int i = 0; i< SearchUser.fcidlist.Count; i++)
            {
                cb4 = SearchUser.fcidlist[i].CCount;

                if (cb4 >= cb3)
                {
                    cb4 = cb3;
                    cn4 = cn3;
                    cb3 = SearchUser.fcidlist[i].CCount;
                    cn3 = categoryList[i];
                    
                    if (cb3 >= cb2)
                    {
                        cb3 = cb2;
                        cn3 = cn2;
                        cb2 = SearchUser.fcidlist[i].CCount;
                        cn2 = categoryList[i];
                        if (cb2 >= cb1)
                        {
                            cb2 = cb1;
                            cn2 = cn1;
                            cb1 = SearchUser.fcidlist[i].CCount;
                            cn1 = categoryList[i];
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
            }
            k1 = "1 " + kn1 + " (" + kb1 + "회)";
            label7.Text = k1;

            k2 = "2 " + kn2 + " (" + kb2 + "회)";
            label8.Text = k2;

            k3 = "3 " + kn3 + " (" + kb3 + "회)";
            label9.Text = k3;

            c1 = "1 " + cn1 + " (" + cb1 + "회)";
            label12.Text = c1;

            c2 = "2 " + cn2 + " (" + cb2 + "회)";
            label11.Text = c2;

            c3 = "3 " + cn3 + " (" + cb3 + "회)";
            label10.Text = c3;
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            dataAnalyze();
        }
        void datainb()
        {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (categoryList[i] == cn1)
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 0);
                            else if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 0);
                            else if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 0);
                            else
                                sbl.ListSearch(i + 100, 0);
                            BestPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 0);
                            BestPrint();
                        }
                    }
                }
        }
        void datainn()
        {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (cn1 == categoryList[i])
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 1);
                            else if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 1);
                            else if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 1);
                            else
                                sbl.ListSearch(i + 100, 1);
                            NewPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 1);
                            NewPrint();
                        }
                    }
                }
        }
        void datainr()
        {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (cn1 == categoryList[i])
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 +1, 2);
                            else if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 2);
                            else if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 2);
                            else
                                sbl.ListSearch(i + 100, 1);
                        RecommentPrint();
                        }
                        else if (i >= 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 2);
                            if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 2);
                            if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 2);
                            RecommentPrint();
                        }
                    }
                }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (categoryList[i].Equals(cn1))
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 0);
                            if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 0);
                            if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 0);
                            else
                            sbl.ListSearch(i + 100, 0);
                            BestPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 0);
                            BestPrint();
                        }
                    }
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (categoryList[i].Equals(cn2))
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 0);
                            if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 0);
                            if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 0);
                            else
                            sbl.ListSearch(i + 100, 0);
                            BestPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 0);
                            BestPrint();
                        }
                    }
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (categoryList[i].Equals(cn3))
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 0);
                            if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 0);
                            if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 0);
                            else
                            sbl.ListSearch(i + 100, 0);
                            BestPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 0);
                            BestPrint();
                        }
                    }
                }
            }

        }

        private void comboBox2_SelectedIndexChanged_3(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (cn1 == categoryList[i])
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 1);
                            if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 1);
                            if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 1);
                            else
                            sbl.ListSearch(i + 100, 1);
                            NewPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 1);
                            NewPrint();
                        }
                    }
                }
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (cn2 == categoryList[i])
                    {
                        if (i < 29)
                        {

                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 1);
                            if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 1);
                            if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 1);
                            else
                            sbl.ListSearch(i + 100, 1);
                            NewPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 1);
                            NewPrint();
                        }
                    }
                }
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (cn3 == categoryList[i])
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 1);
                            if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 1);
                            if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 1);
                            else
                            sbl.ListSearch(i + 100, 1);
                            NewPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 1);
                            NewPrint();
                        }
                    }
                }
            }
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {




            if (comboBox3.SelectedIndex == 0)
            {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (cn1 == categoryList[i])
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 2);
                            if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 2);
                            if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 2);
                            else
                            sbl.ListSearch(i + 100, 2);
                            RecommentPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 2);
                            RecommentPrint();
                        }
                    }
                }
            }
            else if (comboBox3.SelectedIndex == 1)
            {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (cn2 == categoryList[i])
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 2);
                            if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 2);
                            if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 2);
                            else
                            sbl.ListSearch(i + 100, 2);
                            RecommentPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 2);
                            RecommentPrint();
                        }
                    }
                }
            }
            else if (comboBox3.SelectedIndex == 2)
            {
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    if (cn3 == categoryList[i])
                    {
                        if (i < 29)
                        {
                            if (i == 6)
                                sbl.ListSearch(i + 100 + 1, 2);
                            if (i == 20)
                                sbl.ListSearch(i + 100 + 2, 2);
                            if (i == 25)
                                sbl.ListSearch(i + 100 + 3, 2);
                            else
                            sbl.ListSearch(i + 100, 2);
                            RecommentPrint();
                        }
                        else if (i >= 29)
                        {
                            sbl.ListSearch(i + 170, 2);
                            RecommentPrint();
                        }
                    }
                }
            }
        }
    }
}
