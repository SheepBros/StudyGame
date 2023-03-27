using System.Collections.Generic;
using UnityEngine;

namespace TRTS.BT
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
            _currentPointer = _root;
            return this;
        }

        public BehaviourTree End()
        {
            BehaviourTree controller = new (_root);
            _nodeStack.Clear();
            _root = null;
            _currentPointer = null;
            return controller;
        }

        public BehaviourTreeBluePrinter AddNode(NodeBehaviour node)
        {
            _currentPointer.AddNode(node);
            _nodeStack.Push(_currentPointer);
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