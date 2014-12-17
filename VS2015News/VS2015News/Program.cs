using System;
using System.Collections.Generic;
using System.Linq;
using System.Math;

namespace VS2015News
{
    //
    //  1. <object>?, <event>?
    //      http://dailydotnettips.com/2014/12/10/null-conditional-operators-in-c-6-0
    //  2. get only
    //
    //  3. nameof operator
    //     http://dailydotnettips.com/2014/12/09/using-nameof-operator-in-c-6-0/
    //
    //  4. Connect() related
    //     http://channel9.msdn.com/Events/Visual-Studio/Connect-event-2014
    //     http://channel9.msdn.com/Events/Visual-Studio/Connect-event-2014/116
    //     http://dailydotnettips.com/tag/whats-new-in-c6-0/
    //
    //  5. Primary constructor
    //     http://msdn.microsoft.com/en-us/magazine/dn683793.aspx
    //
    //  6. Build 2014 related
    //     http://channel9.msdn.com/Events/Build/2014
    //
    //  7. IDE (Debugging)
    //     breakpoints http://blogs.msdn.com/b/visualstudioalm/archive/2014/11/14/set-breakpoints-on-auto-implemented-properties-with-visual-studio-2015.aspx
    //     - breakpoint undo
    //     - Set breakpoints on auto-implemented properties with Visual 
    //
    //  8. Connect() news
    //     - .NET Core is Open Source http://blogs.msdn.com/b/dotnet/archive/2014/11/12/net-core-is-open-source.aspx
    //
    class Program
    {
        static void Main()
        {
            //NullConditionOperators();
            NameofOperator();
            StaticUsing_StringInterpolation();
            IndexInitializers();
            ExceptionFilter();
            //DeclarationExpressions();
            //AdditionalNumericLiteralFormats();
            MyName.BreakPointsSample.ProgramMain();
            DebuggerSamples();
        }

        #region NullConditionOperators
        private static EventHandler changed;
        private static void NullConditionOperators()
        {
            DataObject dataObject = CreateDataObject();
            string name1 = dataObject == null ? "unknown" : dataObject.Name;
            string name2 = dataObject?.Name ?? "unknown";
            Console.WriteLine("name1: {0}", name1);
            Console.WriteLine("name2: {0}", name2);

            DataObject dataObject1 = CreateDataObject(false);
            ObjectType objectType1 = (dataObject1 == null) ? ObjectType.none : dataObject1.ObjectType; //TODO <object>?
            ObjectType objectType2 = dataObject1?.ObjectType ?? ObjectType.none;
            Console.WriteLine("objectType: {0}", objectType1);
            Console.WriteLine("objectType: {0}", objectType2);

            DataObject dataObject2 = CreateDataObject(false);
            string message;
            if (dataObject2 != null && dataObject2.Message != null)
                message = dataObject2.Message;
            else
                message = "unknown";
            Console.WriteLine("message: {0}", message);
            Console.WriteLine("message: {0}", dataObject2?.Message ?? "unknown"); //TODO <object>?

            Console.WriteLine("[next].message: {0}", dataObject2?.Next?.Message ?? "unknown"); //TODO <object>?

            if (changed != null) //TODO <event>?
                changed(null, new EventArgs());

            changed?.Invoke(null, EventArgs.Empty);
        }

        private static DataObject CreateDataObject(bool create = true)
        {
            if (!create)
                return null;
            return new DataObject("first", ObjectType.first);
        }
        #endregion

        public static void NameofOperator()
        {
            Console.WriteLine("nameof: {0}", nameof(DataObject));               // TODO nameof 
            Console.WriteLine("nameof: {0}", nameof(DataObject.Message));       // TODO nameof 
        }

        public static void StaticUsing_StringInterpolation()
        {
            double r1 = Sqrt(4);
            double r2 = Sqrt(16);                           //TODO static method without class name (class name in namespace)

            string msg1 = String.Format("r1={0}, r2={1}", r1, r2);
            Console.WriteLine(msg1);

            string msg2 = "r1=\{r1}, r2=\{r2}";             //TODO String interpolation
            Console.WriteLine(msg2);

            DataObject dataObject = new DataObject("customName", ObjectType.none) { Message = "customMessage" };
            Console.WriteLine(dataObject.GetMsg());
        }

        //TODO IndexInitializers
        public static void IndexInitializers()
        {
            Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
            dictionary1["X"] = 1;
            dictionary1["Y"] = 2;

            Dictionary<string, int> dictionary2 = new Dictionary<string, int> {["X"] = 1,["Y"] = 2 };

//            Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
//            dictionary3["X"] = 1;
//            dictionary3["Y"] = 2;
        }

        //TODO Exception filter
        public static void ExceptionFilter()
        {
            try
            {
                Do(true);
            }
            catch (CustomException e) if (e.IsSevere)
            {
                Console.WriteLine("[CustomException] IsSevere: {0}", e.IsSevere);
            }
            catch
            {
                Console.WriteLine("[general]");
            }
            finally
            {
                Console.WriteLine("finally");
            }
        }

        private static void Do(bool isSevere)
        {
            throw new CustomException(isSevere);
        }

        //        //TODO DeclarationExpressions
        //        public static void DeclarationExpressions()
        //        {
        //            int value1;
        //            Int32.TryParse("15", out value1);
        //
        //            Int32.TryParse("16", out int value2);
        //            
        //        }

        //        // TODO AdditionalNumericLiteralFormats
        //        public static void AdditionalNumericLiteralFormats()
        //        {
        //            int million = 1_000_000;
        //        }

        //TODO Better together: C# 6 and the Visual Studio 2015 Debugger
        // view LINQ in Quick Watch
        // calculate string interpolation in Immediate Window
        // calculate LINQ interpolation in Immediate Window
        // new breakpoint settings window (embeded to code editor)
        public static void DebuggerSamples()
        {
            //  lambda expressions in the debugger windows
            List<int> list = new List<int> { 0, 1, 2, 3, 4, 5 };
            IEnumerable<int> partOfList = list.Take(3);
            Console.WriteLine("DebuggerSamples completed");
        }
    }

    public enum ObjectType {none, first, second, third }
    public class DataObject
    {
        public string Name { get; private set; }
        public ObjectType ObjectType { get; }    //TODO 'get only'

        public string Message { get; set; }

        public DataObject Next { get; set; }

        public int ID { get; } = 100;             // TODO Initializers for auto-properties

        public DataObject(string name, ObjectType objectType)
        {
            Name = name;
            ObjectType = objectType;
            Message = "message";
        }

        //TODO Expression-bodies methods
        public string GetMsg() => "name: \{Name}, message: \{Message}";
    }

    public class CustomException : Exception
    {
        public bool IsSevere;
        public CustomException(bool isSevere) { IsSevere = isSevere; }
    }

    //TODO Primary constructor
//    public class Person
//    {
//        public Person(string name, int id, string department) : this(name, id)
//        {
//            Department = department;
//        }
//
//        private string Name = name;
//        public int ID { get; } = id;
//
//        public string Department { get; }
//
////        public Person(string name, int id)
////        {
////            Name = name;
////            ID = id;
////        }
//
//    }

//    [Serializable]
//    public class Patent(string title, string yearOfPublication)
//    {
//        public Patent(string title, string yearOfPublication, IEnumerable<string> inventors):this(title, yearOfPublication)
//        {
//            Inventors.AddRange(inventors);
//        }
//        private string _Title = title;
//        public string Title
//        {
//            get
//            {
//                return _Title;
//            }
//            set
//            {
//                if (value == null)
//                {
//                    throw new ArgumentNullException("Title");
//                }
//                _Title = value;
//            }
//        }
//        public string YearOfPublication { get; set; } = yearOfPublication;
//        public List<string> Inventors { get; } = new List<string>();
//        public string GetFullName()
//        {
//            return string.Format("{0} ({1})", Title, YearOfPublication);
//        }
//    }
}
