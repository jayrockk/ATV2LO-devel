using System;
using System.Collections;
using System.Xml.Serialization;


namespace LightsOutCalendarEntry
{
    [XmlRoot("TaskList",Namespace="http://www.axonet.de/2010/07/LightsOutTaskList")]
    public class EntryList
    {

        // This attribute enables the ArrayList to be serialized:
        [System.Xml.Serialization.XmlArray("List")]
        // Explicitly tell the serializer to expect the Item class
        // so it can be properly written to XML from the collection:
        [System.Xml.Serialization.XmlArrayItem("LightsOutTask", typeof(Item))]
        public ArrayList listEntries;

        public EntryList()
        {
            listEntries = new ArrayList();
        }

        public int AddItem(Item item)
        {
            //the following line is removed as there is output already in the calling funtion
            //Console.WriteLine("Entry added to LightsOut Calendar: " + item.startdate + " " + item.enddate);
            return listEntries.Add(item);
        }

        public void RemoveItem(Item item)
        {
            listEntries.Remove(item);
        }

        public void SortList()
        {
            //http://msdn.microsoft.com/de-de/library/0e743hdt(v=VS.80).aspx

            IComparer myComparer = new myComparerClass();
            listEntries.Sort(myComparer);
        }

        public sealed class myComparerClass : IComparer
        {
            int IComparer.Compare(Object x, Object y)
            {
                Item x1 = x as Item;
                Item y1 = y as Item;
                return String.Compare(x1.startdate, y1.startdate);
            }
        }

    }

    public class Item
    {
        [XmlAttribute("Days")]
        public string days;
        [XmlAttribute("EndDate")]
        public string enddate;
        [XmlAttribute("Force")]
        public string force;
        [XmlAttribute("Mode")]
        public string mode;
        [XmlAttribute("Name")]
        public string name;
        [XmlAttribute("OnEnd")]
        public string onend;
        [XmlAttribute("OnStart")]
        public string onstart;
        [XmlAttribute("StartDate")]
        public string startdate;
        [XmlAttribute("Type")]
        public string type;
        
        public Item()
        {
        }

        public Item(string StartDate, string EndDate, string Name)
        {
            days = "None";
            enddate = EndDate;
            force = "false";
            mode = "TimeSpan";
            name = Name;
            onend= "DoNothing";
            onstart = "WakeUp";
            startdate = StartDate;
            type = "Argus TV";
        }

    }
    
}
