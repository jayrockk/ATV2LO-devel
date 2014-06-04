using System;
using System.IO;
using System.Xml.Serialization;

namespace FTR2LO_Config
{
    [Serializable()]
    public class FTR2LO_Config
    {
        public FTR2LO_Config() { }
        public string FilepathLO
        {
            get
            {
                return _filepathlo;
            }
            set
            {
                _filepathlo = value;
            }
        }

        public string EarlyStart
        {
            get
            {
                return _earlystart;
            }
            set
            {
                _earlystart = value;
            }
        }

        public string ServerName
        {
            get
            {
                return _servername;
            }
            set
            {
                _servername = value;
            }
        }

        public string ServerPort
        {
            get
            {
                return _serverport;
            }
            set
            {
                _serverport = value;
            }
        }

        public string RefreshIntervalInMins
        {
            get
            {
                return _refreshintervalinmins;
            }
            set
            {
                _refreshintervalinmins = value;
            }
        }

        public string LogLevel
        {
            get
            {
                return _loglevel;
            }
            set
            {
                _loglevel = value;
            }
        }

        public string PurgeOldFTR2LOItems
        {
            get
            {
                return _purgeoldftr2loitems;
            }
            set
            {
                _purgeoldftr2loitems = value;
            }
        }


        private string _purgeoldftr2loitems;
        private string _filepathlo;
        private string _earlystart;
        private string _servername;
        private string _serverport;
        private string _refreshintervalinmins;
    private string _loglevel;
    }


    public static class ConfigFunctions
    {
        private static string _appname = "ATV2LO";

        public static string AppName
        {
            get
            {
                return _appname;
            }
        }
                
        //readonly string _filepathConfig = Application.StartupPath + "\\config.xml";
        private static string _filepathConfig = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + AppName + "\\config\\config.xml";

        public static bool ConfigExists()
        {
            return File.Exists(_filepathConfig);
        }


        public static void LoadDefaults(FTR2LO_Config cf)
        {
            cf.FilepathLO = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\LightsOut\\TaskList.xml";
            cf.EarlyStart = "3";
            cf.ServerName = "localhost";
            cf.ServerPort = "49943";
            cf.RefreshIntervalInMins = "1";
            cf.LogLevel = FTR2LO_Log.FTR2LO_log.stringLogLevel((int)FTR2LO_Log.FTR2LO_log.LogLevel.WARNING);
            cf.PurgeOldFTR2LOItems = bool.FalseString;
        }

        public static void SaveConfig(FTR2LO_Config cf)
        {
            XmlSerializer ser = new XmlSerializer(typeof(FTR2LO_Config));

            FileMode myfilemode;

            if (ConfigExists())
            {
                myfilemode = FileMode.Truncate;
            }
            else
            {
                myfilemode = FileMode.Create;
            }

            using (FileStream fs = new FileStream(_filepathConfig, myfilemode))
            {
                TextWriter tw = new StreamWriter(fs);
                ser.Serialize(tw, cf);
                tw.Close();
            }

        }

        public static void ReadConfig(FTR2LO_Config cf)
        {
            FTR2LO_Config cf_tmp = new FTR2LO_Config();
            XmlSerializer ser = new XmlSerializer(typeof(FTR2LO_Config));
            if (ConfigExists())
            {
                //create a deep copy of cf_tmp
                using (FileStream fs = new FileStream(_filepathConfig, FileMode.Open))
                {
                    cf_tmp = (FTR2LO_Config)ser.Deserialize(fs);
                    cf.FilepathLO = cf_tmp.FilepathLO;
                    cf.EarlyStart = cf_tmp.EarlyStart;
                    cf.ServerName = cf_tmp.ServerName;
                    cf.ServerPort = cf_tmp.ServerPort;
                    cf.RefreshIntervalInMins = cf_tmp.RefreshIntervalInMins;
                    cf.LogLevel = cf_tmp.LogLevel;
                    cf.PurgeOldFTR2LOItems = cf_tmp.PurgeOldFTR2LOItems;
                }
            }
            else
            {
                LoadDefaults(cf);
                SaveConfig(cf);
            }
        }

        public static void CheckExistingConfigValidity()
        {
            FTR2LO_Config cf = new FTR2LO_Config();
            FTR2LO_Config cf_default = new FTR2LO_Config();
            bool _updaterequired = false;
            if(ConfigExists())
                ReadConfig(cf);
            LoadDefaults(cf_default);
            if (String.IsNullOrEmpty(cf.FilepathLO))
            {
                cf.FilepathLO = cf_default.FilepathLO;
                _updaterequired = true;
            }
            if(String.IsNullOrEmpty(cf.EarlyStart))
            {
                cf.EarlyStart = cf_default.EarlyStart;
                _updaterequired = true;
            }
            if(String.IsNullOrEmpty(cf.ServerName))
            {
                cf.ServerName = cf_default.ServerName;
                _updaterequired = true;
            }
            if(String.IsNullOrEmpty(cf.ServerPort))
            {
                cf.ServerPort = cf_default.ServerPort;
                _updaterequired = true;
            }
            if (String.IsNullOrEmpty(cf.RefreshIntervalInMins))
            {
                cf.RefreshIntervalInMins = cf_default.RefreshIntervalInMins;
                _updaterequired = true;
            }
            if (String.IsNullOrEmpty(cf.LogLevel))
            {
                cf.LogLevel = cf_default.LogLevel;
                _updaterequired = true;
            }
            if(String.IsNullOrEmpty(cf.PurgeOldFTR2LOItems))
            {
                cf.PurgeOldFTR2LOItems = cf_default.PurgeOldFTR2LOItems;
                _updaterequired = true;
            }
            if (_updaterequired)
                SaveConfig(cf);
        }


    }

}

