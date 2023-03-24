using System.Collections.Generic;

namespace TRTS.BehaviourTree
{
    public class BehaviourTreeBluePrinter
    {
        private readonly Stack<NodeBehaviour> _nodeStack;

        private Root _root;

        private NodeBehaviour _currentPointer;

        public BehaviourTreeBluePrinter()
        {
            _nodeStack = new Stack<NodeBehaviour>();
        }

        public BehaviourTreeBluePrinter Start(string rootName)
        {
            _root = new Root(rootName);
            _nodeStack.Push(_root);
            _currentPointer = _root;
            return this;
        }

        public NodeBehaviour End()
        {
            Root root = _root;
            _root = null;
            _nodeStack.Clear();
            _currentPointer = null;
            return root;
        }

        public BehaviourTreeBluePrinter AddNode(NodeBehaviour node)
        {
            _currentPointer.AddNode(node);
            _nodeStack.Push(node);
            _currentPointer = node;
            return this;
        }

        public BehaviourTreeBluePrinter PointerUp()
        {
            _currentPointer = _nodeStack.Pop();
            return this;
        }
    }
}