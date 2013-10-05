using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using LightsOutCalendarEntry;
using FTR2LO_Log;

namespace HelpFunctions
{
    class HelpFunctions
    {
        // useful information
        // http://www.devhood.com/Tutorials/tutorial_details.aspx?tutorial_id=236
        // http://dotnetperls.com/arraylist

        public String SerializeObject(Object pObject)
        {
            String XmlizedString = null;
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(EntryList));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            }

            catch (Exception)
            {
                //System.Console.WriteLine(e);
            }
            finally
            {
                if (memoryStream != null)
                    ((IDisposable)memoryStream).Dispose();
            }
            return XmlizedString;
        }

        public Object DeserializeObject(String pXmlizedString)
        {
            Object ret;
            XmlSerializer xs = new XmlSerializer(typeof(EntryList));
            using (MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)))
            {
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                ret = xs.Deserialize(memoryStream);
            }
            return ret;
        } 

        public void write_file(EntryList list, string filepath)
        {
            string _modulename = "HelpFunctions.write_file";
            //FTR2LO_Log.FTR2LO_log _log = new FTR2LO_log();

            //first sort...
            list.SortList();

            //then write ...
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(EntryList));
                using (TextWriter w = new StreamWriter(filepath))
                {
                    s.Serialize(w, list);
                }
                //w.Close();
            }
            catch(Exception e)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.ERROR, "Error writing to file " + filepath + ". Stop.\n" + e.ToString());
                Environment.Exit(1);
            }

        }

        public EntryList read_file(string filepath)
        {
            EntryList entryList;
            //FTR2LO_Log.FTR2LO_log _log = new FTR2LO_log();
            string _modulename = "HelpFunctions.read_file";

            try
            {
                XmlSerializer s = new XmlSerializer(typeof(EntryList));
                using (TextReader r = new StreamReader(filepath))
                {
                    entryList = (EntryList)s.Deserialize(r);
                }
                //r.Close();
                return entryList;
            }            
            catch(Exception e)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Error reading from file." + filepath + " Stop.\n" + e.ToString());
                Environment.Exit(1);
                // the code below will never be called
                entryList = null;
                return entryList;
            }

        }

        public int find_substring_in_string(string fullstring, string substring)
        {
            return fullstring.IndexOf(substring);
        }
            
       public bool find_task_in_list(EntryList list, Item item)
       {
           //foreach (Item current_item in list)
           int i=0;
           
           for (i = 0; (i < list.listEntries.Count); i = i + 1)
           {

               Item tmp_item = list.listEntries[i] as Item;
               if (is_same_task(item, tmp_item))
               {
                   return true;
               }
           }
          return false;

       }


       private bool is_same_task(Item item1, Item item2)
       {
           bool result = false;
           if (item1.startdate == item2.startdate)
           {
               if (item1.enddate == item2.enddate)
               {
                   result = true;
               }
           }
           
           return result;
       }
 
        private String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        } 
    }
}
