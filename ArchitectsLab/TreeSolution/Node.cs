namespace TreeSolution
{
    public interface INode
    {
        INode Root { get; }
        INode LeftNode { get; }
        INode RightNode { get; }
        string ID { get; }
        double Value { get; }
        void SetLeftNode(INode node);
        void SetRightNode(INode node);

        bool IsDone { get; set; }
    }

    public class Node : INode
    {
        public string ID { get; }
        public double Value { get; }
        public INode Root { get; }
        public INode LeftNode { get; private set; }
        public INode RightNode { get; private set; }

        public Node(string ID, double value, INode leftNode, INode rightNode, INode root)
        {
            this.ID = ID;
            Value = value;
            LeftNode = leftNode;
            RightNode = rightNode;
            Root = root;
            IsDone = false;
        }

        public void SetLeftNode(INode node)
        {
            LeftNode = node;
        }

        public void SetRightNode(INode node)
        {
            RightNode = node;
        }

        public bool IsDone { get; set; }
    }
}
