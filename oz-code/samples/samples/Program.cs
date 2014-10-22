using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace samples
{
    internal class Program
    {
        private static void Main()
        {
            var dataItems = new DataItems<DataItem>();
            dataItems.Items.Add(new DataItem("Joe", "QA"));
            dataItems.Items.Add(new DataItem("Peter", "student"));
            dataItems.Items.Add(new DataItem("Adam", "first"));
            dataItems.Items.Add(new DataItem("Bill", "boss"));
            dataItems.Items.Add(new DataItem("Joe", "CEO"));

            var dataItemsWithToString = new DataItems<DataItemWithToString>();
            dataItemsWithToString.Items.Add(new DataItemWithToString("Joe", "QA"));
            dataItemsWithToString.Items.Add(new DataItemWithToString("Peter", "student"));
            dataItemsWithToString.Items.Add(new DataItemWithToString("Adam", "first"));
            dataItemsWithToString.Items.Add(new DataItemWithToString("Bill", "boss"));
            dataItemsWithToString.Items.Add(new DataItemWithToString("Joe", "CEO"));

            var dataItemsWithDebuggerDisplayAttribute = new DataItems<DataItemWithDebuggerDisplayAttribute>();
            dataItemsWithDebuggerDisplayAttribute.Items.Add(new DataItemWithDebuggerDisplayAttribute("Joe", "QA"));
            dataItemsWithDebuggerDisplayAttribute.Items.Add(new DataItemWithDebuggerDisplayAttribute("Peter", "student"));
            dataItemsWithDebuggerDisplayAttribute.Items.Add(new DataItemWithDebuggerDisplayAttribute("Adam", "first"));
            dataItemsWithDebuggerDisplayAttribute.Items.Add(new DataItemWithDebuggerDisplayAttribute("Bill", "boss"));
            dataItemsWithDebuggerDisplayAttribute.Items.Add(new DataItemWithDebuggerDisplayAttribute("Joe", "CEO"));

            var dataItemsWithDebuggerDebuggerTypeProxy = new DataItems<DataItemWithAttributeDebuggerTypeProxy>();
            dataItemsWithDebuggerDebuggerTypeProxy.Items.Add(new DataItemWithAttributeDebuggerTypeProxy("Joe", "QA"));
            dataItemsWithDebuggerDebuggerTypeProxy.Items.Add(new DataItemWithAttributeDebuggerTypeProxy("Peter", "student"));
            dataItemsWithDebuggerDebuggerTypeProxy.Items.Add(new DataItemWithAttributeDebuggerTypeProxy("Adam", "first"));
            dataItemsWithDebuggerDebuggerTypeProxy.Items.Add(new DataItemWithAttributeDebuggerTypeProxy("Bill", "boss"));
            dataItemsWithDebuggerDebuggerTypeProxy.Items.Add(new DataItemWithAttributeDebuggerTypeProxy("Joe", "CEO"));

            var listOfDataItems = new DataItems<DataItems<DataItem>>();
            listOfDataItems.Items.Add(dataItems);
            listOfDataItems.Items.Add(dataItems);

            #region BugAid visualization features testing

            foreach (DataItem dataItem in dataItems.Items)
            {
                Console.WriteLine("{0}: {1}", dataItem.Name, dataItem.Description);
            }

            bool errorFound = IsError1() || IsError2();
            Console.WriteLine("errorFound: {0}", errorFound);

            if (IsError1() || IsError2())
            {
                Console.WriteLine("flags enabled");
            }

            int xcode = 5;
            int ycode = 10;
            errorFound = IsError1(xcode) || IsError2(ycode);
            Console.WriteLine("errorFound: {0}", errorFound);

            IEnumerable<bool> foundItems = dataItems.Items.Select(dataItem => dataItem.Name == "Joe");

            #endregion

            Console.WriteLine("press any key...");
            Console.ReadKey();
        }

        private static bool IsError1()
        {
            return true;
        }
        private static bool IsError2()
        {
            return false;
        }

        private static bool IsError1(int code)
        {
            return code < 0;
        }
        private static bool IsError2(int code)
        {
            return code >= 0;
        }
    }

    public class DataItem
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DataItem(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
    public class DataItemWithToString : DataItem
    {
        public DataItemWithToString(string name, string description): base(name, description)
        {
        }
        public override string ToString()
        {
            return String.Format("ToString ==> {0}: {1}", Name, Description);
        }
    }

    [DebuggerDisplay("DebuggerDisplay ==> Name = {Name}, Description = {Description,nq}")]
    public class DataItemWithDebuggerDisplayAttribute : DataItem
    {
        public DataItemWithDebuggerDisplayAttribute(string name, string description)
            : base(name, description)
        {
        }
    }

    //http://stackoverflow.com/questions/16695625/debuggerdisplay-attribute-on-a-debuggertypeproxy-class
    //http://stackoverflow.com/questions/1302034/debuggerdisplay-attribute-does-not-work-as-expected?rq=1
    [DebuggerTypeProxy(typeof(FormatTo))]
    public class DataItemWithAttributeDebuggerTypeProxy: DataItem
    {
        private object InternalData;
        public DataItemWithAttributeDebuggerTypeProxy(string name, string description) : base(name, description)
        {            
        }

        internal class FormatTo
        {
            public string ToShow { get; private set; }

            public FormatTo(DataItem item)
            {
                ToShow = String.Format("DebuggerTypeProxy ==> To Show Name = {0}", item.Name);
            }
        }
    }

    public class DataItems<T>
    {       
        public List<T> Items { get; private set; }

        public DataItems()
        {
            Items = new List<T>();
        }
    }
}
