using System;
using System.Text;

namespace TreeSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();
            tree.Init();
            tree.PrintTreeWithRecursion();
            tree.PrintTreeWithoutRecursion();
            tree.PrintTreeWithoutRecursion_Stack(node=>Console.WriteLine(node.ID));

            StringBuilder sb = new StringBuilder()
                .Append("Text")
                .AppendLine("tree")
                .Replace("tr", "rr");

            MyStringBuilder myStringBuilder = new MyStringBuilder();
            myStringBuilder
                .Add("1")
                .Add("2");

            myStringBuilder
                .Add2("t1", "t2")
                .Add2("t1", "t2");
        }
    }
    public class MyStringBuilder
    {
        private String fullText = "";
        public MyStringBuilder Add(string text)
        {
            fullText = fullText + text;
            return this;
        }
    }

    public static class ExtMethods
    {
        public static MyStringBuilder Add2(this MyStringBuilder sb, string t1, string t2)
        {
            sb
                .Add(t1)
                .Add(t2);
            return sb;
        }
    }
}
