using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Windows.Forms;
using Microsoft.WindowsServerSolutions.AddinInfrastructure;
using Microsoft.Win32;
using System.Net;
using System.Xml;
using System.Threading;
using System.IO;

namespace FTR2LO_Vail
{
    static class TopLevelTabHelpers
    {

        public const string ServiceName = "ATV2LoService";
        //public const string FTR_ServiceName = "For The Record Core Services";
        public const string LO_ServiceName = "Lights-Out Service";

        //public const string AppName = FTR2LO_Config.ConfigFunctions.AppName;

        public static FTR2LO_Config.FTR2LO_Config config;
        //public static FTR2LO_Config.ConfigFunctions configfunctions;

        public static void RestartServiceHelper(int timeoutMilliseconds)
        {
            string MyServiceName = ServiceName;

            ServiceController service = null;

            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service = new ServiceController(MyServiceName);

                if (!(service.Status.Equals(ServiceControllerStatus.Stopped)))
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    service.Refresh();
                }
                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                service.Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show("Problem restarting service.\n" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                if (service != null)
                    ((IDisposable)service).Dispose();
            }

        }

      /*  public static bool FTRbyWebRequest(string hostname, string port)
        {
            bool ret = false;

            string _uri = "http://" + hostname + ":" + port + "/ForTheRecord/Core/Version";
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(_uri);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            try
            {
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine("Status Description: " + ((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                ret = true;
                // Display the content.
                //Console.WriteLine("Response: " + responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception)
            {
                //Console.WriteLine("Exception: " + ex.ToString());
            }
            //Console.ReadLine();
            return ret;
        }*/

        public static void CheckForNewVersion()
        {
            Thread m_checkforupdates = new Thread(new ThreadStart(thread_CheckForNewVersion));
            m_checkforupdates.IsBackground = true;

            try
            {
                m_checkforupdates.Start();
            }
            catch (Exception)
            {
            }
        }

        private static void thread_CheckForNewVersion()
        {
            
            string local_version_string = get_installed_version();
            bool _updateavailable = false;
            bool success;

            string remote_version_string = "0.0.0.0";
            string remote_url = "http://code.google.com/p/for-the-record-to-lights-out/";
            string remote_name = "";
            string xml_string = "";

            //
            // The following block checks the latest version given in the remote file
            //

            //
            // Use the first line for production @ googlecode, and the second line for testing @ dropbox
            // Googlecode is not a good place to host. Better use dropbox
            // ** Github is even better!
            //
            //Uri latest_version_xml = new Uri("http://for-the-record-to-lights-out.googlecode.com/files/FTR2LO_Vail_latest_version.xml");
            //Uri latest_version_xml = new Uri("https://dl.dropbox.com/u/11034559/FTR2LightsOut/FTR2LO_Vail_latest_version.xml");
            Uri latest_version_xml = new Uri("https://raw.github.com/jayrockk/ArgusTV-to-Lights-Out/master/ATV2LO_latest_version.xml");
            WebClient client = null;
            try
            {
                client = new WebClient();
                xml_string = client.DownloadString(latest_version_xml);
            }
            catch (Exception)
            {
                //MessageBox.Show("Exception " + ex.ToString());
            }
            finally
            {
                if (client != null)
                {
                    ((IDisposable)client).Dispose();
                }
            }

            XmlReader reader = null;
            try
            {
                reader = XmlReader.Create(new StringReader(xml_string));

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
            catch (Exception)
            {
                //MessageBox.Show("Exception " + ex.ToString());
            }
            /*finally
            {
                if (client != null)
                    ((IDisposable)client).Dispose();
            }*/ 

            //
            // compare local and remote version, and set applicationinfrastructure if remote version is newer
            //
            Version local_version;
            success = Version.TryParse(local_version_string, out local_version);

            Version remote_version;
            success = Version.TryParse(remote_version_string, out remote_version);      

            if (remote_version.CompareTo(local_version) > 0)
            {
                _updateavailable = true;
            }
                        

            //taken from http://forum.home-server-blog.de/viewtopic.php?f=31&t=13785
            if (_updateavailable)
            {
                var addinMgr = new AddInManager();
                addinMgr.NewAddInVersionAvailable(
                   new Guid("0098E620-DA88-476D-9BFC-0F4CE2195E09"), //<Id> from AddIn.xml
                   new Version(remote_version_string),
                   new Uri(remote_url),
                   UpdateClassification.Update);
            }
        }
        
        private static string get_installed_version() //duplicate of get_FTR2LO_version from ftr2loservice
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
}
