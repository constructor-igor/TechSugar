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
        private string _ID;
        private double _value;
        private INode _leftNode;
        private INode _rightNode;
        private INode _root;

        public string ID
        {
            get
            {
                return _ID;
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }
        }

        public INode LeftNode
        {
            get
            {
                return _leftNode;
            }
        }

        public INode RightNode
        {
            get
            {
                return _rightNode;
            }
        }

        public INode Root
        {
            get
            {
                return _root;
            }
        }

        public Node(string ID, double value, INode leftNode, INode rightNode, INode root)
        {
            _ID = ID;
            _value = value;
            _leftNode = leftNode;
            _rightNode = rightNode;
            _root = root;
            IsDone = false;
        }

        public void SetLeftNode(INode node)
        {
            _leftNode = node;
        }

        public void SetRightNode(INode node)
        {
            _rightNode = node;
        }

        public bool IsDone { get; set; }
    }
}
