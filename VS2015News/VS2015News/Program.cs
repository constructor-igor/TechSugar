using System;

namespace VS2015News
{
    //
    //  1. <object?>
    //  2. get only
    //
    //
    class Program
    {
        static void Main(string[] args)
        {
            DataObject dataObject = CreateDataObject();
            string name = dataObject != null ? dataObject.Name : "unknown";
            Console.WriteLine("name: {0}", name);

            DataObject dataObject1 = CreateDataObject(false);
            ObjectType objectType1 = (dataObject1 == null) ? ObjectType.none :  dataObject1.ObjectType; //TODO <object>?
            ObjectType objectType2 = dataObject1?.ObjectType ?? ObjectType.none;
            Console.WriteLine("objectType: {0}", objectType1);
            Console.WriteLine("objectType: {0}", objectType2);
        }


        private static DataObject CreateDataObject(bool create = true)
        {
            if (!create)
                return null;
            return new DataObject("first", ObjectType.first);
        }
    }

    public enum ObjectType {none, first, second, third }
    public class DataObject
    {
        public string Name { get; private set; }
        public ObjectType ObjectType { get; }       //TODO 'get only'

        public DataObject(string name, ObjectType objectType)
        {
            Name = name;
            ObjectType = objectType;
        }
    }
}
