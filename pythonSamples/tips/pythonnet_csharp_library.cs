using System;

namespace pythonnet_csharp_sample_library
{
    public class FooClass
    {
        public FooClass(){
            Console.WriteLine("c#: FooClass()");            
        }
        public FooClass(string name){
            Console.WriteLine("c#: FooClass(string={0})", name);            
        }
        public FooClass(double value){
            Console.WriteLine("c#: FooClass(double={0})", value);            
        }
        public FooClass(string name, double value){
            Console.WriteLine("c#: FooClass(string={0}, double={1})", name, value);            
        }
        public FooClass(double[] data){
            Console.WriteLine("c#: FooClass(double[] data), data[{0}]: {1}", data.Length, string.Join(",", data));
        }
        public FooClass(double[,] data){
            Console.WriteLine("c#: FooClass(double[,] data[0.0]:{0})", data[0, 0]);
        }
    }
}
