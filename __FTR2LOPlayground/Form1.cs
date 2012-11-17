using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;
using ForTheRecord.Entities;
using ForTheRecord.ServiceAgents;
using ForTheRecord.ServiceContracts;
using LightsOutCalendarEntryPlayground;
using System.IO;
using System.Net;
using System.Xml;
//using System.Threading;
using Microsoft.Win32;

namespace __FTR2LOPlayground
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            ftr2lo_main();
        }

        public void ftr2lo_main()
        {

            //create and initialize configuration
            string FilepathLO = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\LightsOut\\TaskList.xml";
            string EarlyStart = "3";
            string ServerName = "server";
            string ServerPort = "49942";
            //int LogLevel = 0;

            EntryList loce1 = new EntryList();
            EntryList ftrce1 = new EntryList();
            EntryList toadd = new EntryList();
            EntryList todelete = new EntryList();



            InitializeServiceChannelFactories(ServerName, Convert.ToInt32(ServerPort));

            ////Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "ftr2lo_main running...");

            if (ServiceChannelFactories.IsInitialized)
            {
                HelpFunctionsPlayground.HelpFunctions hf = new HelpFunctionsPlayground.HelpFunctions();
                string filepath = FilepathLO;
                bool _newitems = false;

                /*UpcomingRecording[] upcomingrecordings;
                UpcomingRecordingsFilter filter = UpcomingRecordingsFilter.Recordings;
                ActiveRecording[] activerecordings;
                TvControlServiceAgent tvcsa = new TvControlServiceAgent();*/
                UpcomingProgram[] upcomingprograms;
                TvSchedulerServiceAgent tvssa = new TvSchedulerServiceAgent();

                if (File.Exists(filepath))
                {
                    ////Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "TaskList.xml found at " + filepath);
                    ////Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Loading LightsOut task list ....");
                    loce1 = hf.read_file(filepath);   //this is how the arraylist is read from the disk
                    ////Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "ok.");
                }

                if (checkBoxClearEntries.Checked == true)
                {
                    DateTime now = System.DateTime.Now;
                    string fnow = now.Year.ToString() + "-" + now.Month.ToString("00") + "-" + now.Day.ToString("00") + "T" + now.TimeOfDay.ToString();
                    loce1 = hf.remove_FTR2LO_entries_from_tasklist(loce1, fnow);
                }


                upcomingprograms = tvssa.GetAllUpcomingPrograms(ScheduleType.Recording, true);

                textBox1.Text = "****There are " + upcomingprograms.Length + " items scheduled in FTR (including cancelled) *****" + Environment.NewLine;
                textBox2.Text = "****There are " + loce1.listEntries.Count + " in total in the task list *****" + Environment.NewLine;
                textBoxToAdd.Text = "To be added to task list" + Environment.NewLine;
                textBoxToDelete.Text = "To be deleted from tast list" + Environment.NewLine;





                if (upcomingprograms.Length > 0)
                {
                    ////Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Number of upcoming programs: " + upcomingprograms.Length);

                    foreach (UpcomingProgram up in upcomingprograms)
                    {
                        DateTime astart;
                        DateTime astop;
                        Guid aprogid;
                        TimeSpan earlystart;
                        string fstart;
                        string fstop;
                        string fprogid;
                        bool iscancelled = up.IsCancelled;

                        if (iscancelled == false)
                        {
                            string aname = up.Title.ToString();
                            earlystart = new TimeSpan(0, Convert.ToInt16(EarlyStart), 0);
                            astart = up.ActualStartTime - earlystart;
                            astop = up.ActualStopTime;
                            aprogid = up.UpcomingProgramId;
                            fstart = astart.Year.ToString() + "-" + astart.Month.ToString("00") + "-" + astart.Day.ToString("00") + "T" + astart.TimeOfDay.ToString();
                            fstop = astop.Year.ToString() + "-" + astop.Month.ToString("00") + "-" + astop.Day.ToString("00") + "T" + astop.TimeOfDay.ToString();
                            fprogid = "FTR2LO-" + System.Guid.NewGuid().ToString();

                            Item item_tst = new Item(fstart, fstop, fprogid);
                            //textBox1.Refresh();
                            ftrce1.AddItem(item_tst);

                        }
                    }

                    DateTime now = System.DateTime.Now;
                    string snow = now.Year.ToString() + "-" + now.Month.ToString("00") + "-" + now.Day.ToString("00") + "T" + now.TimeOfDay.ToString();

                    foreach (Item i in loce1.listEntries)
                        if ((hf.find_task_in_list(ftrce1, i) == false)
                            && (String.Compare(i.enddate, snow) == -1))
                        {
                            todelete.AddItem(i);
                        }

                    foreach (Item i in ftrce1.listEntries)
                        if (hf.find_task_in_list(loce1, i) == false)
                        {
                            toadd.AddItem(i);
                        }

                    foreach (Item i in loce1.listEntries)
                    {
                        if (hf.find_substring_in_string(i.name, "FTR2LO") != -1)
                        {
                            textBox2.Text = textBox2.Text + " " + i.startdate + " " + i.enddate + " " + i.name + Environment.NewLine;
                        }
                    }

                    foreach (Item i in ftrce1.listEntries)
                    {
                        textBox1.Text = textBox1.Text + " " + i.startdate + " " + i.enddate + " " + i.name + Environment.NewLine;
                    }

                    foreach (Item i in toadd.listEntries)
                    {
                        textBoxToAdd.Text = textBoxToAdd.Text + " " + i.startdate + " " + i.enddate + " " + i.name + Environment.NewLine;
                    }

                    foreach (Item i in todelete.listEntries)
                    {
                        textBoxToDelete.Text = textBoxToDelete.Text + " " + i.startdate + " " + i.enddate + " " + i.name + Environment.NewLine;
                    }

                    if (!_newitems)
                    {
                        ////Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Nothing to add.");

                    }
                    else
                    {
                        ////Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Sorting list....");
                        loce1.SortList();
                        ////Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Writing to disk....");
                        hf.write_file(loce1, filepath);
                    }
                }
                else
                {
                    textBox1.Text = "ServiceChannelFactories not initialized";
                }
            }
        }

        private void listInstalledPrograms()
        {
            string FTR_display_name = "For The Record";
            string FTR_publisher = "ForTheRecord";
            string display_name = "";
            string publisher = "";
            string stmp = "";
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    try
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                        {
                            display_name = subkey.GetValue("DisplayName").ToString();
                            publisher = subkey.GetValue("Publisher").ToString();
                            if ((display_name.Contains(FTR_display_name) == true)
                            && (publisher == FTR_publisher))
                            {
                                stmp = stmp + display_name + " installed version: " + subkey.GetValue("DisplayVersion") + "\n";
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //stmp = stmp + ex.ToString();
                    }
                }
            }
            MessageBox.Show(stmp);
        }


        private void InitializeServiceChannelFactories(string _forTheRecordServerName, int _forTheRecordPort)
        {
            int RetryDelay = 30000; //ms
            bool success = ServiceChannelFactories.IsInitialized;
            bool success_on_first_attempt = true;

            ServerSettings serverSettings = new ServerSettings();
            serverSettings.ServerName = _forTheRecordServerName;
            serverSettings.Port = _forTheRecordPort;


            while (!success)
            {
                try
                {
                    ServiceChannelFactories.Initialize(serverSettings, true);
                    //string FTR_version = ForTheRecord.Entities.Constants.ProductVersion;
                    if (success_on_first_attempt)
                    {
                        //Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.INFO, "ServiceChannelFactories successfully initialized. For the Record Version: " + FTR_version);
                    }
                    else
                    {
                        //Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.INFO, "ServiceChannelFactories are now initialized. For the Record Version: " + FTR_version);
                    }
                    success = true;
                }

                catch (ForTheRecordException)
                {
                    success_on_first_attempt = false;
                    //Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.ERROR, ftrex.Message);
                    //Ftr2LoService._log.do_log(_modulename, (int)FTR2LO_log.LogLevel.ERROR, "Retrying in " + (RetryDelay / 1000).ToString() + " seconds....");
                    System.Threading.Thread.Sleep(RetryDelay);
                }
            }
        }

        //
        //Check for Updates
        //
        private void buttonCheckforupdates_Click(object sender, EventArgs e)
        {
            Thread m_checkforupdates = new Thread(new ThreadStart(CheckForUpdates));
            m_checkforupdates.IsBackground = true;

            try
            {
                m_checkforupdates.Start();
            }
            catch (Exception)
            {
            }

            //CheckForUpdates();
        }


        private void CheckForUpdates()
        {
            string remote_version_string = "0.0.0.0";
            string remote_url = "http://code.google.com/p/for-the-record-to-lights-out/";
            string remote_name = "";
            string xml_string = "";

            //
            // Googlecode is not a good place to host. Better use dropbox
            //
            //Uri latest_version_xml = new Uri("http://for-the-record-to-lights-out.googlecode.com/files/FTR2LO_Vail_latest_version.xml");
            //Uri latest_version_xml = new Uri("https://dl.dropbox.com/u/11034559/FTR2LightsOut/FTR2LO_Vail_latest_version.xml");
            Uri latest_version_xml = new Uri("https://raw.github.com/jayrockk/for-the-record-to-lights-out-WHS2011/master/FTR2LO_Vail_latest_version.xml");

            try
            {
                WebClient client = new WebClient();
                xml_string = client.DownloadString(latest_version_xml);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.ToString());
            }

            try
            {
                XmlReader reader = XmlReader.Create(new StringReader(xml_string));

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "name":
                                reader.Read();
                                remote_name = reader.Value;
                                break;
                            case "latest_version":
                                reader.Read();
                                remote_version_string = reader.Value;
                                break;
                            case "url":
                                reader.Read();
                                remote_url = reader.Value;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.ToString());
            }

            MessageBox.Show(remote_name + "\n" + remote_version_string + "\n" + remote_url);
        }

        private void buttonListInstalledPrograms_Click(object sender, EventArgs e)
        {
            listInstalledPrograms();
        }

        private int Ping_FTR()
        {
            int result = -2;

            labelAPIversion.Text = Constants.ForTheRecordApiVersion.ToString();
            AutoResetEvent signal = new AutoResetEvent(false);

            Thread workerThread = null;
            ThreadPool.QueueUserWorkItem((o) =>
            {
                workerThread = Thread.CurrentThread;
                Ping_FTR_Job(out result);
                signal.Set();
            });
            new System.Threading.Timer((o) => { signal.Set(); }, null, 10000, Timeout.Infinite);
            signal.WaitOne();
            if (workerThread != null && workerThread.IsAlive)
                workerThread.Abort();

            return result;
        }

        private void Ping_FTR_Job(out int result)
        {
            
            string ServerName = "192.168.0.34";
            string ServerPort = "49942";
            result = -3;

            ForTheRecordServiceAgent iftrs = new ForTheRecordServiceAgent();

            try
            {
                if (!ServiceChannelFactories.IsInitialized)
                {
                    InitializeServiceChannelFactories(ServerName, Convert.ToInt32(ServerPort));
                }

                result = iftrs.Ping(Constants.ForTheRecordApiVersion);
            }

            catch (EndpointNotFoundException)
            {
                //MessageBox.Show("EnpointNotFoundEx: " + ex.ToString());
            }
            catch (Exception)
            {
                //MessageBox.Show("Ex: " + ex.ToString());
            }

            //return result;
           
        }

        private void Ping_Click(object sender, EventArgs e)
        {
            Ping.Enabled = false;
            int result = Ping_FTR(); labelPing.Text = result.ToString();

            switch (result)
            {
                case 0:
                    labelPing.Text = "Ok";
                    break;
                case 1:
                    labelPing.Text = "FTR Server too old, please update to latest version";
                    break;
                case -1:
                    labelPing.Text = "FTR2LO too old, please update to latest version";
                    break;
                case -2:
                    labelPing.Text = "Cannot connect to Service (-2)";
                    break;
                case -3:
                    labelPing.Text = "Cannot connect to Service (-3)";
                    break;
            }

            Ping.Enabled = true;


        }



    }
}
