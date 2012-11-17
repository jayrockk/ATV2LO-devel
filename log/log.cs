using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FTR2LO_Log
{
    public static class FTR2LO_log
    {
        public enum LogLevel
        {
            DEBUG = 0,
            WARNING = 1,
            ERROR = 2,
            INFO = 3,
        }

        //The variable where the current log level is set
        public static int CurrentLogLevel = 0;

        private static string filename = "log";
        private static string appname = "FTR2LO";
        //int LogLevel;

        public static string Filename
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }

        public static string AppName
        {
            get
            {
                return appname;
            }
            set
            {
                appname = value;
            }
        }

        //private readonly string _filepath = Application.StartupPath;
        private static string _filepath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + appname + "\\logs";

        public static void do_log(string _modulename, int _msgloglevel, string sMsg)
        {
            int RetryDelay = 1000; //ms
            int MaximumRetryPeriod = 15; //seconds
            Object writeLock = new Object();
            DateTime fileReceived = DateTime.Now;

            if (_filepath == "")
            {
                //qMessageBox.Show("No Path to Log file.");
                Application.Exit();
            }

            if (_msgloglevel >= CurrentLogLevel)
            {
                string _sLogFormat = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString().ToString() + ":: ";
                string _logmessage = "[" + stringLogLevel(_msgloglevel) + "] [" + _modulename + "] " + _sLogFormat + sMsg;
                string _fullfilepath = _filepath + "\\" + sTime() + "-" + filename + ".log";
                bool loop = true;


                lock (writeLock)
                {
                    while (loop)
                    {

                        try
                        {
                            StreamWriter sw = new StreamWriter(_fullfilepath, true);
                            sw.WriteLine(_logmessage);
                            sw.Flush();
                            sw.Close();
                            loop = false;
                            break;
                        }
                        catch
                        {
                            System.Threading.Thread.Sleep(RetryDelay);
                        }

                        TimeSpan timeElapsed = DateTime.Now - fileReceived;
                        if (timeElapsed.TotalSeconds > MaximumRetryPeriod)
                        {
                            do_log(_modulename, (int)LogLevel.WARNING, "Error writing log file. At least one message lost.\n");
                            loop = false;
                            break;
                        }
                    }
                }
            }
        }

        //
        //http://www.codewrapper.com/Questions/View/24/how-to-get-oldest-files-in-a-directory-in-c-sharp
        //

        private static string sTime()
        {
            //string s = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            string s = DateTime.Now.ToString("yyyy-MM-dd");
            return s;
        }

        public static string stringLogLevel(int iLogLevel)
        {
            return ((LogLevel)Convert.ToInt16(iLogLevel)).ToString();
        }

        public static int intLogLevel(string stringLogLevel)
        {
            return (int)Enum.Parse(typeof(LogLevel), stringLogLevel);
        }

        public static int getMaxLogLevel()
        {
            LogLevel tmp = new LogLevel();
            return Enum.GetValues(tmp.GetType()).Cast<int>().Max();
        }

        public static int getMinLogLevel()
        {
            LogLevel tmp = new LogLevel();
            return Enum.GetValues(tmp.GetType()).Cast<int>().Min();
        }

        public static string Delete_old_logs(int max_number_of_files)
        {
            string _filefilter = "*" + filename + ".log";

            DirectoryInfo directorInfo = new DirectoryInfo(_filepath);
            FileInfo[] files = directorInfo.GetFiles(_filefilter);

            Array.Sort(files, delegate(FileInfo file1, FileInfo file2)
            {
                return file2.CreationTime.CompareTo(file1.CreationTime);
            });


            string result = "";

            int i = files.Count();

            while (i > max_number_of_files)
            {

                try
                {
                    files[i - 1].Delete();
                    result = result + files[i - 1].Name + ": old logfile deleted.\n";
                }
                catch (Exception ex)
                {
                    result = result + files[i - 1].FullName + ": could not delete old logfile.\nReason: " + ex.Message + "\n";
                }
                i = i - 1;

            }

            if (result == "")
            {
                result = "No log files deleted.\n";
            }

            return result;
        }
    }
}


