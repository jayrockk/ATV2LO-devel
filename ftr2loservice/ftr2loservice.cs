using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using ArgusTV.DataContracts;
using ArgusTV.ServiceAgents;
using FTR2LO_Log;
using LightsOutCalendarEntry;
using Microsoft.Win32;

//http://www.mycsharp.de/wbb2/thread.php?threadid=18902

namespace ftr2loservice
{
    #region Installer Class
    [RunInstaller(true)]
    public class Ftr2LoServiceInstaller : Installer
    {

        private ServiceInstaller m_ThisService;
        private ServiceProcessInstaller m_ThisServiceProcess;
        public const string ServiceName = "Ftr2LoService";

        public Ftr2LoServiceInstaller()
        {
            m_ThisService = new ServiceInstaller();
            m_ThisServiceProcess = new ServiceProcessInstaller();

            m_ThisServiceProcess.Account = ServiceAccount.LocalSystem;
            m_ThisService.ServiceName = ServiceName;
            m_ThisService.StartType = ServiceStartMode.Automatic;
            //m_ThisService.ServicesDependedOn = new string[] { "For The Record Core Services" };


            Installers.Add(m_ThisService);
            Installers.Add(m_ThisServiceProcess);
        }
    }
    #endregion

    #region ServiceBase Class
    public class Ftr2LoService : ServiceBase
    {
        //initialize Log
        //public static FTR2LO_Log.FTR2LO_log _log = new FTR2LO_Log.FTR2LO_log();
        private string _modulename = "Ftr2LoService";

        //create and initialize configuration
        public static FTR2LO_Config.FTR2LO_Config config = new FTR2LO_Config.FTR2LO_Config();
        //public static FTR2LO_Config.ConfigFunctions configfunctions = new FTR2LO_Config.ConfigFunctions();

        //prepare Server thread
        private static FTR2LO m_ftr2lo = new FTR2LO();
        private Thread m_ftr2loFTR2LOServerThread = new Thread(new ThreadStart(m_ftr2lo.FTR2LO_Server));

        public static void Main(string[] args)
        {
            System.ServiceProcess.ServiceBase.Run(new Ftr2LoService());
        }

        protected override void OnStart(string[] args)
        {
            //Prepare config
            FTR2LO_Config.ConfigFunctions.CheckExistingConfigValidity();
            FTR2LO_Config.ConfigFunctions.ReadConfig(config);
            FTR2LO_Log.FTR2LO_log.CurrentLogLevel = FTR2LO_Log.FTR2LO_log.intLogLevel(config.LogLevel);
            FTR2LO_Log.FTR2LO_log.Filename = "FTR2LO";
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_Log.FTR2LO_log.LogLevel.INFO, "Start service. Version " + get_FTR2LO_version_from_registry());
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_Log.FTR2LO_log.LogLevel.DEBUG, FTR2LO_Log.FTR2LO_log.Delete_old_logs(10));


            //start background thread
            //m_StopServer = false;
            m_ftr2loFTR2LOServerThread.IsBackground = true;

            try
            {
                m_ftr2loFTR2LOServerThread.Start();
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "m_ftr2loThread started.\n");
            }
            catch (Exception ex)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Problem starting m_ftr2loThread.\n" + ex.ToString());
            }
        }

        protected override void OnStop()
        {
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_Log.FTR2LO_log.LogLevel.DEBUG, "Sending stop signal to service...");
            //m_StopServer = true;
            //m_ftr2loThread.Abort();
            m_ftr2lo.FTR2LO_Stop();
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_Log.FTR2LO_log.LogLevel.DEBUG, "Stopped.");
        }

        private string get_FTR2LO_version_from_registry()
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
    }

    #endregion

    #region ServiceContract/Interface definition

    [ServiceContract(Namespace = "http://FTR2LO.InternalService/")]
    public interface IFTR2LO
    {
        [OperationContract]
        bool IsConnectedToFTR2LO();
        [OperationContract]
        bool IsConnectedToFTR();
        [OperationContract]
        int IPingFTR();
        [OperationContract]
        string IPingFTRToString(int status);
    }

    #endregion

    #region main class
    public class FTR2LO : IFTR2LO, IDisposable
    {
        #region const
        private System.Timers.Timer timer1 = new System.Timers.Timer();
        ServiceHost serviceHost;
        const string _modulename = "FTR2LO";
        #endregion

        #region public variables
        //stop flag
        public static System.Threading.AutoResetEvent stopFlag = new System.Threading.AutoResetEvent(false);
        // create local host for communication with Dashboard GUI
        ServiceHost localHost;
        #endregion

        #region IDispose members
        //http://www.codeproject.com/Articles/15360/Implementing-IDisposable-and-the-Dispose-Pattern-P

        private bool disposed = false;
        // implements IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                // if this is a dispose call dispose on all state you
                // hold, and take yourself off the Finalization queue.
                if (disposing)
                {
                    if (timer1 != null)
                    {
                        timer1.Dispose();
                    }

                    if (localHost != null)
                    {
                        ((IDisposable)localHost).Dispose();
                    }
                }

                // free your own state (unmanaged objects)
                AdditionalCleanup();

                this.disposed = true;
            }
        }

        // finalizer simply calls Dispose(false)
        ~FTR2LO()
        {
            Dispose(false);
        }

        // some custom cleanup logic
        private void AdditionalCleanup()
        {
            // this method should not allocate or take locks, unless
            // absolutely needed for security or correctness reasons.
            // since it is called during finalization, it is subject to
            // all of the restrictions on finalizers above.
        }

        #endregion

        #region Interface members

        public bool IsConnectedToFTR2LO()
        {
            return true;
        }

        //this prodecure is rather useless,
        //because it returns true also if the ServiceChannelFactories do not longer exist
        public bool IsConnectedToFTR()
        {
            return ServiceChannelFactories.IsInitialized;
        }

        public int IPingFTR()
        {
            return PingFTR(Ftr2LoService.config.ServerName, Convert.ToInt32(Ftr2LoService.config.ServerPort));
        }

        public string IPingFTRToString(int status)
        {
            return PingFTRToString(status);
        }

        #endregion

        #region Server method

        public void FTR2LO_Server()
        {
            string _modulename = "FTR2LO_Server";
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Inside FTR2LO_Server.\n");
            AppSettingsReader appsettingsreader = new AppSettingsReader();

            try
            {
                Uri baseAddr = new Uri("http://localhost:41432/WCFService1");
                localHost = new ServiceHost(typeof(FTR2LO), baseAddr);
                localHost.AddServiceEndpoint(typeof(IFTR2LO), new WSHttpBinding(), "FTR2LO_InternalService");
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                localHost.Description.Behaviors.Add(smb);
                localHost.Open();
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Internal Service Host started");
            }
            catch (Exception ex)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Problem starting Internal Service Host. Exception: " + ex.ToString());
            }

            // log the status of LO service
            string service_status_lo = GetWindowsServiceStatus(Convert.ToString(appsettingsreader.GetValue("LOservicename", String.Empty.GetType())));
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_Log.FTR2LO_log.LogLevel.DEBUG, "Lights-Out Service status: " + service_status_lo);

            string service_status_ftr = GetWindowsServiceStatus(Convert.ToString(appsettingsreader.GetValue("FTRservicename", String.Empty.GetType())));
            string service_status_ftr_old = "";
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_Log.FTR2LO_log.LogLevel.DEBUG, "For The Record Service status: " + service_status_ftr);

            //Loop and wait until FTR services is running
            int countdown = 30;
            int counter = countdown;
            while (true)
            {

                // break if FTR is not installed (because maybe it is installed remotely)
                if (service_status_ftr == "Not installed.")
                {
                    break;
                }

                System.Threading.Thread.Sleep(1000);

                // break if FTR services is running
                if (service_status_ftr == ServiceControllerStatus.Running.ToString())
                {
                    break;
                }

                counter = counter - 1;

                if (counter <= 0)
                {
                    counter = countdown;
                    TimeSpan timeout = TimeSpan.FromMilliseconds(60 * 1000);
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_Log.FTR2LO_log.LogLevel.DEBUG, "Trying to start For The Record Service ....");
                    ServiceController FTRservicecontroller = new ServiceController(Convert.ToString(appsettingsreader.GetValue("FTRservicename", String.Empty.GetType())));
                    try
                    {
                        FTRservicecontroller.Start();
                        FTRservicecontroller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    }
                    catch (Exception ex)
                    {
                        FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_Log.FTR2LO_log.LogLevel.DEBUG, "Exception starting For The Record Service\n" + ex.ToString());
                    }
                    finally
                    {
                        if (FTRservicecontroller != null)
                            ((IDisposable)FTRservicecontroller).Dispose();
                    }
                }

                service_status_ftr_old = service_status_ftr;
                service_status_ftr = GetWindowsServiceStatus(Convert.ToString(appsettingsreader.GetValue("FTRservicename", String.Empty.GetType())));

                // check if status of FTR has changed and if so, update log file
                if (!(service_status_ftr == service_status_ftr_old))
                {
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_Log.FTR2LO_log.LogLevel.DEBUG, "For The Record Service status: " + service_status_ftr);
                }

            }


            // timer1 is for scheduled run of ftr2lo_main()
            this.timer1.AutoReset = true;
            this.timer1.Interval = 1000 * 60 * Convert.ToInt32(appsettingsreader.GetValue("scheduledIntervalInMinutes", Int32.MaxValue.GetType()));
            this.timer1.Enabled = true;
            this.timer1.Elapsed += new ElapsedEventHandler(onTimer1Elapsed);
            this.timer1.Start();

            InitializeServiceChannelFactories(Ftr2LoService.config.ServerName, Convert.ToInt32(Ftr2LoService.config.ServerPort));

            InitializeEventListener(); //comment this out to remove the event listener

            try
            {
                ftr2lo_main();
            }
            catch (Exception ex)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Problem calling ftr2lo_main() from FTR2LO_Server.\n" + ex.ToString());
            }

            stopFlag.WaitOne();

            //the following is called when the service is finally stopped
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Stopping....");
            {
                if (serviceHost != null)
                    ((IDisposable)serviceHost).Dispose();
            }

            //the following is called when the service is finally stopped
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Stopped.");
            this.Dispose();
        }

        public void FTR2LO_Stop()
        {
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Stopping FTR2LO_Server()...");
            stopFlag.Set();
        }

        #endregion

        #region main method
        public void ftr2lo_main()
        {
            EntryList loce1 = new EntryList();
            EntryList ftrce1 = new EntryList();
            EntryList toadd = new EntryList();
            EntryList todelete = new EntryList();
            Hashtable guid_name_hashtable = new Hashtable();

            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "ftr2lo_main running...");

            if (ServiceChannelFactories.IsInitialized)
            {
                HelpFunctions.HelpFunctions hf = new HelpFunctions.HelpFunctions();
                string filepath = Ftr2LoService.config.FilepathLO;
                bool _changeflag = false;
                UpcomingProgram[] upcomingprograms;

                #region get Lights-Out entries

                if (File.Exists(filepath))
                {
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "TaskList.xml found at " + filepath);
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Loading LightsOut task list ....");
                    loce1 = hf.read_file(filepath);   //this is how the arraylist is read from the disk
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "ok.");
                }
                else
                {
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.WARNING, "TaskList.xml not found at " + filepath + ", file will be created if planned recordings exist.");
                }

                //FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Cleaning list....");
                //loce1 = hf.clean_tasklist(loce1, fnow);

                #endregion

                #region get FTR entries

                using (ArgusTV.ServiceAgents.SchedulerServiceAgent tvssa = new ArgusTV.ServiceAgents.SchedulerServiceAgent())
                {
                    upcomingprograms = tvssa.GetAllUpcomingPrograms(ScheduleType.Recording, false);
                }

                foreach (UpcomingProgram up in upcomingprograms)
                {
                    DateTime astart;
                    DateTime astop;
                    Guid aprogid;
                    TimeSpan earlystart;
                    string fstart;
                    string fstop;
                    string fprogid;
                    string aname;

                    aname = up.Title.ToString();
                    earlystart = new TimeSpan(0, Convert.ToInt16(Ftr2LoService.config.EarlyStart), 0);
                    astart = up.ActualStartTime - earlystart;
                    astop = up.ActualStopTime;
                    aprogid = up.UpcomingProgramId;
                    fstart = astart.Year.ToString() + "-" + astart.Month.ToString("00") + "-" + astart.Day.ToString("00") + "T" + astart.TimeOfDay.ToString();
                    fstop = astop.Year.ToString() + "-" + astop.Month.ToString("00") + "-" + astop.Day.ToString("00") + "T" + astop.TimeOfDay.ToString();
                    fprogid = "FTR2LO-" + System.Guid.NewGuid().ToString();

                    Item item_tst = new Item(fstart, fstop, fprogid);
                    ftrce1.AddItem(item_tst);
                    guid_name_hashtable.Add(fprogid, aname);
                }

                if (upcomingprograms.Length > 0)
                {
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Number of upcoming programs: " + upcomingprograms.Length);

                }

                else
                {
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "No upcoming recordings");
                }

                #endregion

                #region Calculate Add/Delete lists

                bool _purgeoldftr2loitems = false;
                Boolean.TryParse(Ftr2LoService.config.PurgeOldFTR2LOItems, out _purgeoldftr2loitems);

                foreach (Item i in ftrce1.listEntries)
                    if (hf.find_task_in_list(loce1, i) == false)
                    {
                        _changeflag = true;
                        FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.INFO, "Add \"" + guid_name_hashtable[i.name] + "\" to LightsOut. Actual Start Time: " + i.startdate + ", Actual Stop Time: " + i.enddate);
                        toadd.AddItem(i);
                    }

                DateTime now = System.DateTime.Now;
                string fnow = now.Year.ToString() + "-" + now.Month.ToString("00") + "-" + now.Day.ToString("00") + "T" + now.TimeOfDay.ToString();

                foreach (Item i in loce1.listEntries)
                {
                    //FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "fnow: " + fnow + ", Actual Stop Time: " + i.enddate + ", Compare: " + String.Compare(i.enddate, fnow).ToString() + ", Substring" + hf.find_substring_in_string(i.name, "FTR2LO") + ", in FTR: "+ (hf.find_task_in_list(ftrce1, i)));
                    if ((hf.find_substring_in_string(i.name, "FTR2LO") != -1) // if FTRLO incl, it is greater than -1
                        && (_purgeoldftr2loitems || (String.Compare(i.enddate, fnow) == 1)) //if earlier, it is -1
                        && (hf.find_task_in_list(ftrce1, i) == false))
                    {
                        _changeflag = true;
                        FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.INFO, "Remove obsolete task from Lights-Out. Actual Start Time: " + i.startdate + ", Actual Stop Time: " + i.enddate);
                        todelete.AddItem(i);
                    }
                }
                #endregion

                #region Do Add/Delete Actions

                foreach (Item i in toadd.listEntries)
                {
                    loce1.AddItem(i);
                }

                foreach (Item i in todelete.listEntries)
                {
                    loce1.RemoveItem(i);
                }

                #endregion

                if (_changeflag)
                {
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Sorting list....");
                    loce1.SortList();
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Writing to disk....");
                    hf.write_file(loce1, filepath);
                }
                else
                {
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Nothing to do.");
                }
            }
            else
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Probelm in ftr2lo_main: ServiceChannelFactories not initialized, trying to re-init....");
                InitializeServiceChannelFactories(Ftr2LoService.config.ServerName, Convert.ToInt32(Ftr2LoService.config.ServerPort));
            }

            this.timer1.Start();
        }

        #endregion

        #region Private help functions

        private void onTimer1Elapsed(object source, ElapsedEventArgs e)
        {
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Periodic launch of ftr2lo_main...");
            ftr2lo_main();
        }

        private string GetWindowsServiceStatus(string serviceName)
        {
            string result;
            try
            {
                using (ServiceController service = new ServiceController(serviceName))
                {
                    service.Refresh();
                    result = service.Status.ToString();
                }

            }
            catch
            {
                result = "Not installed.";
            }

            return result;
        }

        private void InitializeServiceChannelFactories(string _forTheRecordServerName, int _forTheRecordPort)
        {
            int RetryDelay = 30000; //ms
            bool success = ServiceChannelFactories.IsInitialized;
            bool success_on_first_attempt = true;

            ServerSettings serverSettings = new ServerSettings();
            serverSettings.ServerName = _forTheRecordServerName;
            serverSettings.Port = _forTheRecordPort;

            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Trying to connect to FTR service on " + _forTheRecordServerName + ":" + _forTheRecordPort);

            while (!success)
            {
                try
                {
                    ServiceChannelFactories.Initialize(serverSettings, true);
                    string FTR_version = ArgusTV.DataContracts.Constants.ProductVersion;
                    if (success_on_first_attempt)
                    {
                        FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "ServiceChannelFactories successfully initialized.");
                    }
                    else
                    {
                        FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "ServiceChannelFactories are now initialized.");
                    }
                    success = true;
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "For the Record installed version: " + FTR_version);
                }

                catch (ArgusTV.DataContracts.ArgusTVException atvex)
                {
                    success_on_first_attempt = false;
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "ATV exception: " + atvex.Message);
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Retrying in " + (RetryDelay / 1000).ToString() + " seconds....");
                    System.Threading.Thread.Sleep(RetryDelay);
                }

                catch (Exception ex)
                {
                    success_on_first_attempt = false;
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Exception :" + ex.Message);
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Retrying in " + (RetryDelay / 1000).ToString() + " seconds....");
                    System.Threading.Thread.Sleep(RetryDelay);
                }
            }
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "FTR2LO API version: " + Constants.CurrentApiVersion.ToString());
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, PingFTRToString(PingFTR(_forTheRecordServerName, _forTheRecordPort)));
        }

        private int PingFTR(string _forTheRecordServerName, int _forTheRecordPort)
        {
            int status = -2;

            AutoResetEvent signal = new AutoResetEvent(false);

            Thread workerThread = null;
            ThreadPool.QueueUserWorkItem((o) =>
            {
                workerThread = Thread.CurrentThread;
                Ping_FTR_Job(_forTheRecordServerName, _forTheRecordPort, out status);
                signal.Set();
            });
            using (new System.Threading.Timer((o) => { signal.Set(); }, null, 10000, Timeout.Infinite))
            {
                signal.WaitOne();
            }
            if (workerThread != null && workerThread.IsAlive)
                workerThread.Abort();

            return status;
        }

        //better: create an Enum for the status
        private string PingFTRToString(int status)
        {

            string ret = "unknown";

            switch (status)
            {
                case 0:
                    ret = "FTR2LO and FTR are compatible";
                    break;
                case 1:
                    ret = "FTR Server too old, please update to latest version";
                    break;
                case -1:
                    ret = "FTR2LO too old, please update to latest version";
                    break;
                case -2:
                    ret = "Cannot connect to Service (-2)";
                    break;
                case -3:
                    ret = "Cannot connect to Service (-3)";
                    break;
            }

            return ret;
        }

        private void Ping_FTR_Job(string _forTheRecordServerName, int _forTheRecordPort, out int result)
        {

            result = -3;



            try
            {
                if (!ServiceChannelFactories.IsInitialized)
                {
                    InitializeServiceChannelFactories(_forTheRecordServerName, _forTheRecordPort);
                }
                using (ArgusTV.ServiceAgents.CoreServiceAgent iftrs = new ArgusTV.ServiceAgents.CoreServiceAgent())
                {
                    result = iftrs.Ping(Constants.CurrentApiVersion);
                }
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


        private static int GetFreeTcpPort(int defaultport, int minport)
        {
            int port = -1;
            while (defaultport >= minport)
            {
                try
                {
                    System.Net.Sockets.TcpListener tcpListener = new System.Net.Sockets.TcpListener(IPAddress.Any, defaultport);
                    tcpListener.Start();
                    tcpListener.Stop();
                    port = defaultport;
                    break;
                }
                catch (System.Net.Sockets.SocketException)
                {
                    defaultport--;
                }
            }
            return port;
        }

        #endregion

        #region EventListener

        private void InitializeEventListener()
        {
            const int defaultport = 38772;
            const int portrange = 100;
            const string _modulename = "InitializeEventListener";
            const string eventListenerHost = "localhost";

            int port = GetFreeTcpPort(defaultport, defaultport - portrange);
            if (port < 0)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.ERROR, "No free port found.");
            }
            else
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "EventListener on port " + port.ToString());

                try
                {
                    if (ServiceChannelFactories.IsInitialized)
                    {
                        StartListenerServices(String.Format("net.tcp://{0}:{1}/FTREventListener/", eventListenerHost, port.ToString()));
                    }
                    else
                    {
                        FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Problem in InitializeEventListener: ServiceChannelFactories not initialized, trying to re-init....");
                        InitializeServiceChannelFactories(Ftr2LoService.config.ServerName, Convert.ToInt32(Ftr2LoService.config.ServerPort));
                    }
                }
                catch (Exception e)
                {
                    FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.WARNING, "Problem in InitializeEventListener. \n" + e.ToString());
                }
            }
        }

        private void StartListenerServices(string serviceUrl)
        {
            const string _modulename = "StartListenerServices";

            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Starting EventListener...");
            serviceHost = EventsListenerService.CreateServiceHost(serviceUrl);
            EventsListenerService.Ftr2lo = this;
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Eventlistener status: " + serviceHost.State.ToString());

            try
            {
                serviceHost.Open();
                using (ArgusTV.ServiceAgents.CoreServiceAgent agent = new ArgusTV.ServiceAgents.CoreServiceAgent())
                {
                    agent.EnsureEventListener(ArgusTV.DataContracts.EventGroup.RecordingEvents, serviceUrl, Constants.EventListenerApiVersion);
                }
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "ok.");
            }
            catch (Exception e)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.WARNING, "EventListener not started. \n" + e.ToString());
            }

        }

        private void StopListenerServices()
        {
            const string _modulename = "StopListenerServices";
            FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Stop EventListener...");
            serviceHost.Close();
        }
        #endregion
    }
    #endregion
}