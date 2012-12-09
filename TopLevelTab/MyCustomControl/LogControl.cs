using System;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Text;

namespace ATV2LO_TopLevelTab
{
    public partial class LogControl : UserControl
    {
        #region General

        static string _logfilepath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + FTR2LO_Config.ConfigFunctions.AppName + "\\logs";
        static string _logfilefilter = "*" + ".log";

        //FTR2LO_Config.FTR2LO_Config config;
        //FTR2LO_Config.ConfigFunctions configfunctions;

        public LogControl()
        {
            InitializeComponent();
        }


        private void LogControl_Load(object sender, EventArgs e)
        {
            Initialize_GUI();
            Initialize_Config();
            Initialize_Loglist(0);
        }

        private void LogControl_Leave(object sender, EventArgs e)
        {

        }


        private void Initialize_GUI()
        {
            fileSystemWatcher2.Path = _logfilepath;
            fileSystemWatcher2.Filter = _logfilefilter;
        }

        private void Initialize_Config()
        {
            TopLevelTabHelpers.config = new FTR2LO_Config.FTR2LO_Config();
            //TopLevelTabHelpers.configfunctions = new FTR2LO_Config.ConfigFunctions();
            FTR2LO_Config.ConfigFunctions.ReadConfig(TopLevelTabHelpers.config);
            ShowConfig();
        }

        private void Initialize_Loglist(int index)
        {
            DirectoryInfo directoryInfo = null;

            try
            {
                directoryInfo = new DirectoryInfo(_logfilepath);
                FileInfo[] files = directoryInfo.GetFiles(_logfilefilter);

                Array.Sort(files, delegate(FileInfo file1, FileInfo file2)
                {
                    return file2.CreationTime.CompareTo(file1.CreationTime);
                });

                if (index < files.Length)
                {
                    comboBox1.DataSource = files;
                    comboBox1.SelectedIndex = index;
                    if (comboBox1.Enabled == false) { comboBox1.Enabled = true; }
                    textBox_log.Text = logfilecontents(files[index].FullName);
                    textBox_log.SelectionStart = textBox_log.Text.Length;
                    textBox_log.ScrollToCaret();
                }
                else
                {
                    if (comboBox1.Enabled == true) { comboBox1.Enabled = false; }
                    comboBox1.DataSource = null;
                    textBox_log.Text = "";
                    textBox_log.Text = "Log file does not exits yet.";
                }
            }
            catch (Exception e)
            {
                textBox_log.Text = "Error in InitializeLoglist()\n\n" + e.ToString();
            }
        }

        #endregion

        #region Help_Functions


        public void RestartService(int timeoutMilliseconds)
        {
            button_Apply.Enabled = false;
            TopLevelTabHelpers.RestartServiceHelper(timeoutMilliseconds);
            button_Apply.Enabled = true;
        }


        private void ShowConfig()
        {
            comboBox_LogLevel.DataSource = Enum.GetValues(typeof(FTR2LO_Log.FTR2LO_log.LogLevel));
            comboBox_LogLevel.SelectedIndex = FTR2LO_Log.FTR2LO_log.intLogLevel(TopLevelTabHelpers.config.LogLevel);
        }

        #endregion

        #region textBox_log_handling

        public string logfilecontents(string _fullName)
        {
            /*int RetryDelay = 500; //ms
            int MaximumRetryPeriod = 5; //seconds*/
            DateTime fileReceived = DateTime.Now;
            StringBuilder s = new StringBuilder();

            //while (true)
            //{
            StreamReader sr = null;
            string line;
            try
            {
                sr = new StreamReader(_fullName);
                while ((line = sr.ReadLine()) != null)
                {
                    if (!(line == String.Empty))
                    {
                        s.Append(line);
                        s.Append(Environment.NewLine);
                    }
                }
                //sr.Close();
                //  break;
            }
            catch (IOException)
            {
                s.Append("IO Exception reading log file");
            }
            finally
            {
                if (sr != null)
                    ((IDisposable)sr).Dispose();
            }

            /*
             *  I think the following is useless and not neccesary...
             *  
             * // Calculate the elapsed time and stop if the maximum retry        
            // period has been reached.        
            TimeSpan timeElapsed = DateTime.Now - fileReceived;
            if (timeElapsed.TotalSeconds > MaximumRetryPeriod)
            {
                s = "The file " + _fullName + " could not be processed.";
                break;
            }*/

            // }
            if (s.Length == 0)
            {
                s.Append("No log file found.");
            }

            return s.ToString();
        }

        private void fileSystemWatcher2_Changed(object sender, FileSystemEventArgs e)
        {
            //MessageBox.Show("fileSystemWatcher1_Changed_2");
            if ((e.Name == comboBox1.SelectedText))
            {
                textBox_log.Text = logfilecontents(e.FullPath);
                textBox_log.SelectionStart = textBox_log.Text.Length;
                textBox_log.ScrollToCaret();
            }
            else
            {
                Initialize_Loglist(0);
            }
        }

        private void fileSystemWatcher2_Created(object sender, FileSystemEventArgs e)
        {
            // !!!!!!!!!!!!!
            // leave this in, otherwise fileSystemWatcher breaks.
            // seems to be a bug.
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Initialize_Loglist(comboBox1.SelectedIndex);
        }

        #endregion

        #region buttons VAIL

        private void button_Apply_Click(object sender, EventArgs e)
        {
            FTR2LO_Config.ConfigFunctions.SaveConfig(TopLevelTabHelpers.config);
            RestartService(10000);
        }
        #endregion

        #region textbox_leave_functions

        private void comboBox_LogLevel_Leave(object sender, EventArgs e)
        {
            TopLevelTabHelpers.config.LogLevel = FTR2LO_Log.FTR2LO_log.stringLogLevel(comboBox_LogLevel.SelectedIndex);
            ShowConfig();
        }

        #endregion



    }
}
