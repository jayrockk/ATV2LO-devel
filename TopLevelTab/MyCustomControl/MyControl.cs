using System;
using System.Drawing;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace ATV2LO_TopLevelTab
{
    public partial class MyControl : UserControl
    {
        #region Delegates

        delegate void StringParameterDelegate(string value);
        delegate void SystemDrawingImageDelegate(System.Drawing.Image value);
        delegate void VisibleLoadingCircleDelegate(bool visible);

        #endregion

        #region public variables
        System.Timers.Timer timer1 = new System.Timers.Timer();
        #endregion

        #region General

        public MyControl()
        {
            InitializeComponent();
        }

        private void MyControl_Load(object sender, EventArgs e)
        {
            labelDebugInfo.Visible = false;
            Initialize_Config();
            clear_service_status_area();
            update_service_status_area();
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);
            InitializeTimer(60);
        }

        private void MyControl_Leave(object sender, EventArgs e)
        {

        }

        private void Initialize_Config()
        {
            TopLevelTabHelpers.config = new FTR2LO_Config.FTR2LO_Config();
            FTR2LO_Config.ConfigFunctions.ReadConfig(TopLevelTabHelpers.config);
            ShowConfig();
        }


        private void InitializeTimer(int seconds)
        {
            timer1.Interval = seconds * 1000;
            timer1.Enabled = true;
            timer1.Start();
        }

        #endregion

        #region Help_Functions

        public void RestartService(int timeoutMilliseconds)
        {
            button_Apply.Enabled = false;
            TopLevelTabHelpers.RestartServiceHelper(timeoutMilliseconds);
            button_Apply.Enabled = true;
            clear_service_status_area();
        }

        private static string GetWindowsServiceStatus(string serviceName)
        {
            ServiceController service = null;
            string result;
            try
            {
                service = new ServiceController(serviceName);
                service.Refresh();
                result = service.Status.ToString();
            }
            catch
            {
                result = "Not installed.";
            }
            finally
            {
                if (service != null)
                    ((IDisposable)service).Dispose();
            }

            return result;
        }

        private void timer1_Tick(object o, EventArgs e)
        {
            Update_labelDebugInfo("timer1 fired: " + System.DateTime.Now.ToString());
            clear_pictureBox1();
            update_service_status_area();
        }
                
        private void ShowConfig()
        {
            textBox_tasklistxmlfilepath.Text = TopLevelTabHelpers.config.FilepathLO;
            textBox_ServerName.Text = TopLevelTabHelpers.config.ServerName;
            checkBoxPurgeOldItems.CheckState = stringToCheckState(TopLevelTabHelpers.config.PurgeOldFTR2LOItems);
            numericUpDown_ServerPort.Value = Decimal.Parse(TopLevelTabHelpers.config.ServerPort);
            numericUpDown_EarlyStart.Value = Decimal.Parse(TopLevelTabHelpers.config.EarlyStart);
        }

        private string checkStateToString(CheckState checkstate)
        {
            if (checkstate == CheckState.Checked)
                return Boolean.TrueString;
            else
                return Boolean.FalseString;
        }

        private CheckState stringToCheckState(string str)
        {
            if (str == Boolean.TrueString)
                return CheckState.Checked;
            else
                return CheckState.Unchecked;
        }

        #endregion

        #region GUI update

        private void clear_pictureBox1()
        {
            Update_pictureBox1_Image(ATV2LO_TopLevelTab.Properties.Resources.Blank);
            Visible_loadingCircle1(true);
        }

        private void clear_service_status_area()
        {
            //String _textBlank = null;

            Update_pictureBoxFTR_Image(ATV2LO_TopLevelTab.Properties.Resources.Blank__16x16_72);
            Update_pictureBoxFTR2LO_Image(ATV2LO_TopLevelTab.Properties.Resources.Blank__16x16_72);
            Update_pictureBoxFTR_Image(ATV2LO_TopLevelTab.Properties.Resources.Blank__16x16_72);
            Update_pictureBox1_Image(ATV2LO_TopLevelTab.Properties.Resources.Blank);

            Update_label_actual_FTRstatus("Working... please be patient");
            Update_label_actual_LOstatus("Working... please be patient");
            Update_label_actual_server_status("Working... please be patient");

            Visible_loadingCircle_FTR(true);
            Visible_loadingCircle_FTR2LO(true);
            Visible_loadingCircle_LO(true);
            Visible_loadingCircle1(true);
        }

        private void update_service_status_area()
        {
            try
            {
                Thread t = new Thread(new ThreadStart(update_service_status_area_Job));
                t.IsBackground = true;
                t.Start();
            }
            catch
            {
            }
        }

        private void update_service_status_area_Job()
        {
            Update_labelDebugInfo("GUI updatejob started: " + DateTime.Now.ToString());
            timer1.Stop();

            // Lights Out small icon
            string LOstatus = GetWindowsServiceStatus(TopLevelTabHelpers.LO_ServiceName);
            Update_label_actual_LOstatus(LOstatus);
            if (LOstatus == "Running")
            {
                Visible_loadingCircle_LO(false);
                Update_pictureBoxLO_Image(ATV2LO_TopLevelTab.Properties.Resources.Complete_OK__16x16_72);
            }
            else if (LOstatus == "Not installed.")
            {
                Visible_loadingCircle_LO(false);
                Update_pictureBoxLO_Image(ATV2LO_TopLevelTab.Properties.Resources.CriticalError_16x16_72);
            }
            else
            {
                Update_pictureBoxLO_Image(ATV2LO_TopLevelTab.Properties.Resources.Warning_16x16_72);
                Visible_loadingCircle_LO(false);
            }

            // FTR2LO and FTR icon plus large icon
            bool _isconnectedtoFTR2LO = false;
            int _status = -3;
            string _FTRStatus = "Unknown (1)";
            string _address = "http://localhost:41433/WCFService1/FTR2LO_InternalService";
            FTR2LOClient client = null;
            System.ServiceModel.WSHttpBinding binding = null;
            System.ServiceModel.EndpointAddress endpointAddress = null;

            try
            {
                binding = new System.ServiceModel.WSHttpBinding();
                binding.Name = "WSHttpBinding_IFTR2LO"; // not sure if this is necessary.
                endpointAddress = new System.ServiceModel.EndpointAddress(_address);
                System.TimeSpan receiveTimeout = new System.TimeSpan();
                TimeSpan.TryParse("00:10:00", out receiveTimeout);
                binding.ReceiveTimeout = receiveTimeout;
                //FTR2LOClient client = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A really crazy exception has occured:\n" + ex.ToString());
            }
            try
            {
                client = new FTR2LOClient(binding, endpointAddress);
                _isconnectedtoFTR2LO = client.IsConnectedToFTR2LO();
                _status = client.IPingFTR();
                _FTRStatus = client.IPingFTRToString(_status);
            }
            catch (Exception)
            {
                //in this case, display error for FTR2LO and leave FTR loadingcircle
                Update_label_actual_server_status("Error: Not connected");
                Update_label_actual_FTRstatus("Unknown");
                Update_pictureBoxFTR2LO_Image(ATV2LO_TopLevelTab.Properties.Resources.CriticalError_16x16_72);
                Visible_loadingCircle_FTR2LO(false);
                Update_pictureBox1_Image(ATV2LO_TopLevelTab.Properties.Resources.CriticalError);
                Visible_loadingCircle1(false);
            }
            finally
            {
                if (client.State == System.ServiceModel.CommunicationState.Opened)
                    client.Close();
            }


            if (_isconnectedtoFTR2LO)
            {
                Visible_loadingCircle_FTR2LO(false);
                Update_label_actual_server_status("Connected");
                Update_pictureBoxFTR2LO_Image(ATV2LO_TopLevelTab.Properties.Resources.Complete_OK__16x16_72);

                // FTRL2LO icon and large icon
                Update_label_actual_FTRstatus(_FTRStatus);
                if (_status == 0)
                {
                    Visible_loadingCircle_FTR(false);
                    Update_pictureBoxFTR_Image(ATV2LO_TopLevelTab.Properties.Resources.Complete_OK__16x16_72);
                    Visible_loadingCircle1(false);
                    Update_pictureBox1_Image(ATV2LO_TopLevelTab.Properties.Resources.Complete_OK);
                }
                else
                {
                    Visible_loadingCircle_FTR(false);
                    Update_pictureBoxFTR_Image(ATV2LO_TopLevelTab.Properties.Resources.Warning_16x16_72);
                    Visible_loadingCircle1(false);
                    Update_pictureBox1_Image(ATV2LO_TopLevelTab.Properties.Resources.Warning);
                }
            }
            else
            {
                Update_label_actual_server_status("Not connected");
                Visible_loadingCircle1(false);
                Update_pictureBoxFTR_Image(ATV2LO_TopLevelTab.Properties.Resources.CriticalError_16x16_72);
                Update_label_actual_FTRstatus("Unknown");
            }
            timer1.Start();
            Update_labelDebugInfo("GUI update finished: " + DateTime.Now.ToString());
        }


        // http://kristofverbiest.blogspot.com/2007/02/simple-pattern-to-invoke-gui-from.html

        void Visible_loadingCircle1(bool value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new VisibleLoadingCircleDelegate(Visible_loadingCircle1), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far
            loadingCircle1.Visible = value;
            loadingCircle1.Active = value;
        }

        void Visible_loadingCircle_LO(bool value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new VisibleLoadingCircleDelegate(Visible_loadingCircle_LO), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far
            loadingCircle_LO.Visible = value;
            loadingCircle_LO.Active = value;
        }

        void Visible_loadingCircle_FTR(bool value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new VisibleLoadingCircleDelegate(Visible_loadingCircle_FTR), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far
            loadingCircle_FTR.Visible = value;
            loadingCircle_FTR.Active = value;
        }

        void Visible_loadingCircle_FTR2LO(bool value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new VisibleLoadingCircleDelegate(Visible_loadingCircle_FTR2LO), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far
            loadingCircle_FTR2LO.Visible = value;
            loadingCircle_FTR2LO.Active = value;
        }

        void Update_labelDebugInfo(string value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterDelegate(Update_labelDebugInfo), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far
            labelDebugInfo.Text = value;
        }

        void Update_label_actual_server_status(string value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterDelegate(Update_label_actual_server_status), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far
            label_actual_server_status.Text = value;
        }

        void Update_label_actual_LOstatus(string value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterDelegate(Update_label_actual_LOstatus), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far
            label_actual_LOstatus.Text = value;
        }

        void Update_label_actual_FTRstatus(string value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterDelegate(Update_label_actual_FTRstatus), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far
            label_actual_FTRstatus.Text = value;
        }

        void Update_pictureBox1_Image(System.Drawing.Image value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SystemDrawingImageDelegate(Update_pictureBox1_Image), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far

            pictureBox1.Image = value;
        }

        void Update_pictureBoxFTR2LO_Image(System.Drawing.Image value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SystemDrawingImageDelegate(Update_pictureBoxFTR2LO_Image), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far

            pictureBoxFTR2LO.Image = value;
        }

        void Update_pictureBoxFTR_Image(System.Drawing.Image value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SystemDrawingImageDelegate(Update_pictureBoxFTR_Image), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far

            pictureBoxFTR.Image = value;
        }

        void Update_pictureBoxLO_Image(System.Drawing.Image value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SystemDrawingImageDelegate(Update_pictureBoxLO_Image), new object[] { value });
                return;
            }
            // Must be on the UI thread if we've got this far

            pictureBoxLO.Image = value;
        }


        #endregion

        #region buttons VAIL

        private void button_TasklistxmlFileDialog_Click(object sender, EventArgs e)
        {
            string _newpath = string.Empty;
            OpenFileDialog openFileDialog1 = null;
            try
            {
                openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Tasklist.xml|tasklist.xml|All files (*.*)|*.*";
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\LightsOut\\TaskList.xml";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _newpath = openFileDialog1.FileName;
                    TopLevelTabHelpers.config.FilepathLO = _newpath;
                    ShowConfig();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (openFileDialog1 != null)
                    ((IDisposable)openFileDialog1).Dispose();
            }
        }

        private void button_Defaults_Click(object sender, EventArgs e)
        {
            FTR2LO_Config.ConfigFunctions.LoadDefaults(TopLevelTabHelpers.config);
            ShowConfig();
        }

        private void Button_Cancel_Click_1(object sender, EventArgs e)
        {
            FTR2LO_Config.ConfigFunctions.ReadConfig(TopLevelTabHelpers.config);
            ShowConfig();
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            FTR2LO_Config.ConfigFunctions.SaveConfig(TopLevelTabHelpers.config);
            RestartService(2000);
        }
        #endregion

        #region textbox_leave_functions


        private void textBox_tasklistxmlfilepath_Leave_1(object sender, EventArgs e)
        {
            TopLevelTabHelpers.config.FilepathLO = textBox_tasklistxmlfilepath.Text;
        }

        private void textBox_ServerName_Leave_1(object sender, EventArgs e)
        {
            TopLevelTabHelpers.config.ServerName = textBox_ServerName.Text;
        }

        private void numericUpDown_ServerPort_Leave(object sender, EventArgs e)
        {
            TopLevelTabHelpers.config.ServerPort = numericUpDown_ServerPort.Value.ToString();
        }

        private void numericUpDown_EarlyStart_Leave(object sender, EventArgs e)
        {
            TopLevelTabHelpers.config.EarlyStart = numericUpDown_EarlyStart.Value.ToString();
        }

        private void checkBoxPurgeOldItems_CheckedChanged(object sender, EventArgs e)
        {
            TopLevelTabHelpers.config.PurgeOldFTR2LOItems = checkStateToString(checkBoxPurgeOldItems.CheckState);
        }

        #endregion

        #region textbox_specifc_validationfunctions

        private bool isok_textBox_tasklistxmlfilepath(string s)
        {
            return (isvalidfilepath(s, label_tasklistxmlfilepath.Text));
        }

        private bool isok_textBox_ServerName(string hostname)
        {
            return (isvalidhostname(hostname, label_ServerName.Text));
        }

        #endregion

        #region generic_validationfunctions

        static bool isvalidfilepath(string filepath, string title)
        {
            bool valid = true;

            if (filepath.Length == 0)
            {
                valid = false;
            }
            else
            {
                try
                {
                    string _path = Path.GetDirectoryName(filepath);
                }
                catch
                {
                    valid = false;
                }

                try
                {
                    string _filename = Path.GetFileName(filepath);

                }
                catch
                {
                    valid = false;
                }

            }

            if (valid == false)
            {
                MessageBox.Show(filepath + ": Invalid path", title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            return valid;
        }

        static bool isvalidhostname(string hostname, string title)
        {
            bool valid = true;

            if (hostname.Length == 0)
            {
                valid = false;
            }
            else
            {
                try
                {
                    Uri.CheckHostName(hostname);
                }
                catch
                {
                    valid = false;
                }
            }

            if (valid == false)
            {
                MessageBox.Show(hostname + ": Invalid hostname", title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return valid;
        }

        private void label_FTR2LO_service_status_Click(object sender, EventArgs e)
        {

        }

        /*public static bool IsValidNaturalNumber(string s, int lower_limit, int upper_limit, string title)
        {
            bool valid = false;
            int number;
            if (Int32.TryParse(s, out number))
            {
                if ((number >= lower_limit) && (number <= upper_limit))
                {
                    valid = true;
                }
            }
            if (valid == false)
            {
                MessageBox.Show(s + ": Value must be between " + lower_limit + " and " + upper_limit, title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return valid;
        }*/
        #endregion

    }
}
