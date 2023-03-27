using System;
using System.Collections.Generic;

namespace TRTS.BT
{
    [Serializable]
    public abstract class NodeBehaviour : IEquatable<NodeBehaviour>
    {
        public readonly string _name;
        
        protected List<NodeBehaviour> _nodes = new ();

        public NodeBehaviour(string name)
        {
            _name = name;
        }

        public abstract UpdateStatus Update();

        public abstract void PreUpdate();

        public abstract void PostUpdate();

        public void AddNode(NodeBehaviour node)
        {
            if (!_nodes.Contains(node))
            {
                _nodes.Add(node);
            }
        }

        public void AddNodes(List<NodeBehaviour> nodes)
        {
            foreach (NodeBehaviour node in nodes)
            {
                AddNode(node);
            }
        }

        public void RemoveNode(NodeBehaviour node)
        {
            if (node != null)
            {
                _nodes.Remove(node);
            }
        }

        public void RemoveNode(string name)
        {
            NodeBehaviour node = _nodes.Find(item => item._name == name);
            RemoveNode(node);
        }

        public bool Equals(NodeBehaviour other)
        {
            return other != null && _name == other._name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((NodeBehaviour)obj);
        }

        public override int GetHashCode()
        {
            return !string.IsNullOrEmpty(_name) ? _name.GetHashCode() : 0;
        }
    }
}