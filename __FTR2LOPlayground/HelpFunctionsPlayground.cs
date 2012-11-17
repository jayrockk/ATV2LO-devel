using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using LightsOutCalendarEntryPlayground;
//using FTR2LO_Log;

namespace HelpFunctionsPlayground
{
    class HelpFunctions
    {
        // useful information
        // http://www.devhood.com/Tutorials/tutorial_details.aspx?tutorial_id=236
        // http://dotnetperls.com/arraylist

        public String SerializeObject(Object pObject)
        {

            try
            {
                String XmlizedString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(typeof(EntryList));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                return XmlizedString;
            }

            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }

        public Object DeserializeObject(String pXmlizedString)
        {
            XmlSerializer xs = new XmlSerializer(typeof(EntryList));
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return xs.Deserialize(memoryStream);
        }

        public void write_file(EntryList list, string filepath)
        {
            //string _modulename = "HelpFunctions.write_file";
            //FTR2LO_Log.FTR2LO_log _log = new FTR2LO_log();

            //first sort...
            list.SortList();

            //then write ...
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(EntryList));
                TextWriter w = new StreamWriter(filepath);
                s.Serialize(w, list);
                w.Close();
            }
            catch (Exception)
            {
                //Console.WriteLine("Error writing XML file.\n" + e.ToString());
                //_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Error writing XML file. Stop.\n" + e.ToString());
                Environment.Exit(1);
            }

        }

        public EntryList read_file(string filepath)
        {
            EntryList entryList;
            //FTR2LO_Log.FTR2LO_log _log = new FTR2LO_log();
            //string _modulename = "HelpFunctions.read_file";

            try
            {
                XmlSerializer s = new XmlSerializer(typeof(EntryList));
                TextReader r = new StreamReader(filepath);
                entryList = (EntryList)s.Deserialize(r);
                r.Close();
                return entryList;
            }
            catch (Exception)
            {
                //_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "Error reading XML file. Stop.\n" + e.ToString());
                //Console.WriteLine("Error reading XML file.\n" + e.ToString());
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
            int i = 0;

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

        public EntryList remove_FTR2LO_entries_from_tasklist(EntryList list, string ending_later_than)
        {
            EntryList retval = new EntryList();
            int i = 0;
            


            for (i = 0; (i < list.listEntries.Count); i = i + 1)
            {
                Item tmp_item = list.listEntries[i] as Item;
                MessageBox.Show("A: " + tmp_item.enddate + "\nB: " + ending_later_than + "\nCompare: " + String.Compare(tmp_item.enddate, ending_later_than).ToString());
                
                //the first check it to make sure that non-FTTR2LO entries are ignored;
                //the result of the string comparison is -1 if enddate is aftern ending_later_than
                //and +1 if enddate is earlier than ending_later_than
                
                if ((find_substring_in_string(tmp_item.name, "FTR2LO") != 1) &&
                    (String.Compare(tmp_item.enddate, ending_later_than) == -1))
                      retval.AddItem(tmp_item);
            }

            return retval;
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
