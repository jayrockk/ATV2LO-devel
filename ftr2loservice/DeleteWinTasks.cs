using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DeleteWinTasks_CMD
{
    class DeleteOldWinTasks
    {
        //cleans out all items that are old and contain FTR2LO or ATV2LO
        public static void CleanTaskList()
        {

            //run GetCMDResult("schtasks.exe") and get the tasks back
            string _schtasksOut = GetCMDResult("schtasks.exe", "/FO csv");

            //the following works in Windows Server 2012, but not in WHS 2011
            //string _schtasksOut = GetCMDResult("schtasks.exe", "/FO csv /nh");

            //filter for those tasks that contain FTR2LO or ATV2LO and delete them

            string[] lines = Regex.Split(_schtasksOut, "\r\n");

            foreach (string line in lines)
            {
                string[] lineItems = Regex.Split(line, ",");
                if ((lineItems.Length == 3) &&
                    ((lineItems[0].IndexOf("ATV2LO") != (-1)) | (lineItems[0].IndexOf("FTR2LO") != (-1))))
                {
                    lineItems[0] = lineItems[0].Trim(new Char[] { '"' }); //remove hyphens
                    lineItems[1] = lineItems[1].Trim(new Char[] { '"' }); //remove hyphens 
                    Console.WriteLine("[0]: " + lineItems[0]
                        + "[1]: " + lineItems[1]
                        );

                    DateTime taskStarttime;
                    bool isDateTime = DateTime.TryParse(lineItems[1], out taskStarttime);

                    if (isDateTime)
                        Console.WriteLine("Keep item. Parsed Time: " + taskStarttime.ToString());
                    else
                    {
                        Console.WriteLine("Remove item " + lineItems[0]);
                        Console.WriteLine(RemoveFromWindowsTasklist(lineItems[0]));
                    }
                }

            }
        }

        //remove a single item from Windows task list 
        public static string RemoveFromWindowsTasklist(string item)
        {
            return (GetCMDResult("schtasks", " /delete /f /tn " + item));
        }

        #region command line handling

        private static string GetCMDResult(string strCommand, string strCommandParameters)
        {
            //Create process
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            //strCommand is path and file name of command to run
            pProcess.StartInfo.FileName = strCommand;

            //strCommandParameters are parameters to pass to program
            pProcess.StartInfo.Arguments = strCommandParameters;

            pProcess.StartInfo.UseShellExecute = false;

            //Set output of program to be written to process output stream
            pProcess.StartInfo.RedirectStandardOutput = true;

            //Optional
            //pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;

            //Start the process
            pProcess.Start();

            //Get program output
            string strOutput = pProcess.StandardOutput.ReadToEnd();

            //Wait for process to finish
            pProcess.WaitForExit();

            return strOutput;
        }

        #endregion

    }
}
