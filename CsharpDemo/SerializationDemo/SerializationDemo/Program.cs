using System;
using System.Collections.Generic;


namespace SerializationDemo
{
    internal class Program
    {
        static void TestCase1<T>(IDataCaseFactory<T> customDataFactory, bool circleReference, ICustomSerializer serializer)
        {
            try
            {
                Console.WriteLine($"[{typeof(T)}][{serializer.GetType()}][circle ({circleReference}] Starting test case");
                T root = customDataFactory.CreateCustomData(circleReference);
                //ICustomSerializer serializer = new GeneralSystemSerializer();
                serializer.AddConverter<IDataCase1>();
                string jsonContent = serializer.Serialize(root);
                Console.WriteLine($"Serialized content: {jsonContent}");
                T deserialized = serializer.Deserialize<T>(jsonContent);
                customDataFactory.Compare(root, deserialized);
                Console.WriteLine($"[{serializer.GetType()}][circle ({circleReference}] Finihsed test case");
            }
            catch (Exception ex) {
                Console.WriteLine($"[{serializer.GetType()}][circle ({circleReference}] Failed test case with error {ex.Message} in {ex.StackTrace}");
            }
        }

        static void Main(string[] args)
        {
            var customDataFactory1 = new DataCase1Factory();
            //TestCase1(false, new GeneralSystemSerializer());
            //TestCase1(true, new GeneralSystemSerializer());
            TestCase1(customDataFactory1, false, new NewtonSerializer());
            TestCase1(customDataFactory1, true, new NewtonSerializer());

            var customDataFactory9 = new DataCase9Factory();
            ////TestCase1(false, new GeneralSystemSerializer());
            ////TestCase1(true, new GeneralSystemSerializer());
            TestCase1(customDataFactory9, false, new NewtonSerializer());
            TestCase1(customDataFactory9, true, new NewtonSerializer());

        }
    }
}
