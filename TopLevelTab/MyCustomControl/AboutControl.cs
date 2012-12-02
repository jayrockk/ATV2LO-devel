using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace FTR2LO_Vail
{
    public partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();
        }

        private void AboutControl_Load(object sender, EventArgs e)
        {
            label_Name.Text = "Argus TV to Lights-Out";

            label_Version.Text = "Version " + get_FTR2LO_version();

            label_Description.Text = "Connects ArgusTV and Lights-Out and wakes\nyour Windows Server for TV recordings.";
            
            label_Donation.Text = "ArgusTV to Lights-Out\nis freeware -\nplease support the development!";

            pictureBox1.Image = FTR2LO_Vail.Properties.Resources.btn_donateCC_LG;

            pictureBox2.Image = FTR2LO_Vail.Properties.Resources.icon_ATV2LO1;

            //Version 1: Load the image from the internet
            //string URL = "https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif";
            //pictureBox1.Load(URL);

            //Version 2, presumably better: Load the image from the internet
            //string URL = "https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif";
            /*WebClient wc = new WebClient();
            wc.Proxy = null;
            byte[] bFile = wc.DownloadData(URL);
            MemoryStream ms = new MemoryStream(bFile);
            Image img = Image.FromStream(ms);
            pictureBox1.Image = img;*/

            
        }

        /*private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=GDYUJ9NCTCYP4

            /*button html
            <form action="https://www.paypal.com/cgi-bin/webscr" method="post">
            <input type="hidden" name="cmd" value="_s-xclick">
            <input type="hidden" name="hosted_button_id" value="GDYUJ9NCTCYP4">
            <input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!">
            <img alt="" border="0" src="https://www.paypalobjects.com/de_DE/i/scr/pixel.gif" width="1" height="1">
            </form>

              
             */
        }*/

        private string get_FTR2LO_version()
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            string FTR2LO_name = "Argus TV to Lights Out";

            string ret = "unknown";

            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        if (Convert.ToString(sk.GetValue("DisplayName")) == FTR2LO_name)
                        {
                            ret = Convert.ToString(sk.GetValue("DisplayVersion"));
                            break;
                        }
                    }
                }
                return ret;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=GDYUJ9NCTCYP4");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://code.google.com/p/for-the-record-to-lights-out/");
        }


        /*private Bitmap LoadPicture(string url)
        {
            HttpWebRequest wreq;
            HttpWebResponse wresp;
            Stream mystream;
            Bitmap bmp;

            bmp = null;
            mystream = null;
            wresp = null;
            try
            {
                wreq = (HttpWebRequest)WebRequest.Create(url);
                wreq.AllowWriteStreamBuffering = true;

                wresp = (HttpWebResponse)wreq.GetResponse();

                if ((mystream = wresp.GetResponseStream()) != null)
                    bmp = new Bitmap(mystream);
            }
            finally
            {
                if (mystream != null)
                    mystream.Close();

                if (wresp != null)
                    wresp.Close();
            }
            return (bmp);
        }*/
        
    }
}