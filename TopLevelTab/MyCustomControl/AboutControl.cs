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
            label_Name.Text = "For the Record to Lights-Out";

            label_Version.Text = "Version " + get_FTR2LO_version();

            label_Description.Text = "Connects For the Record and Lights-Out and wakes\nyour Windows Home Server for TV recordings.";
            
            label_Donation.Text = "For the Record to Lights-Out\nis freeware -\nplease support the development!";

            pictureBox1.Image = FTR2LO_Vail.Properties.Resources.btn_donateCC_LG;

            pictureBox2.Image = FTR2LO_Vail.Properties.Resources.icon_ATV2LO_png;

        }

        private string get_FTR2LO_version()
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            string FTR2LO_name = "For the Record to Lights Out for WHS 2011";

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
    }
}