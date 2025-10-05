using System;
using System.Collections.Generic;


namespace SerializationDemo
{
    internal class Program
    {
        static bool TestCase<T>(IDataCaseFactory<T> customDataFactory, bool circleReference, ICustomSerializer serializer)
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
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine($"[{serializer.GetType()}][circle ({circleReference}] Failed test case with error {ex.Message} in {ex.StackTrace}");
                return false;
            }
        }

        static void Main(string[] args)
        {
            List<bool> list = new List<bool>();
            var customDataFactory1 = new DataCase1Factory();
            list.Add(TestCase(customDataFactory1, false, new NewtonSerializer()));
            list.Add(TestCase(customDataFactory1, true, new NewtonSerializer()));

            var customDataFactory2 = new DataCase2Factory();
            list.Add(TestCase(customDataFactory2, false, new NewtonSerializer()));
            list.Add(TestCase(customDataFactory2, true, new NewtonSerializer()));

            var customDataFactory9 = new DataCase9Factory();
            list.Add(TestCase(customDataFactory9, false, new NewtonSerializer()));
            list.Add(TestCase(customDataFactory9, true, new NewtonSerializer()));

            Console.WriteLine($"{string.Join(",", list)}");

            CustomData c = new CustomData();
            c.Name = "Joe";
            NewtonSerializer serializer = new NewtonSerializer();
            string jsonContent = serializer.Serialize(c);
            object deserialized = serializer.Deserialize<object>(jsonContent);
            Type deserializedType = deserialized.GetType();
            Console.WriteLine($"{deserializedType}");
            CustomData typedObject = deserialized as CustomData;
            if (typedObject != null)
            {
                Console.WriteLine(typedObject.Name);
            }

        }
    }
}
