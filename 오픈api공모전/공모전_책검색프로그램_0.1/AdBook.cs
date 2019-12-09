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
    public partial class AdBook : Form
    {
        SearchBookList sbl = SearchBookList.getInstance();
        WebClient Downloader = new WebClient();
        string link = string.Empty;
        public AdBook(string filepath, string adlink)
        {
            InitializeComponent();
            link = adlink;
            AdPrint(filepath);
        }
        private void AdPrint(string filepath)
        {
                Stream ImageStream = Downloader.OpenRead(filepath);
                Image DownloadImage = Image.FromStream(ImageStream);
                pictureBox1.Image = DownloadImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //bool passTo = checkBox2.Checked;

            //xml저장코드
            this.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process process = new System.Diagnostics.Process();

            process.StartInfo = new System.Diagnostics.ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Internet Explorer\\iexplore.exe", link);

            process.Start();
        }
    }
}
