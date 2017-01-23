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
        }
    }
}
