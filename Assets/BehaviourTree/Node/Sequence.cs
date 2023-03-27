using System;
using UnityEngine;

namespace TRTS.BT
{
    [Serializable]
    public class Sequence : NodeBehaviour, IComposite
    {
        private NodeBehaviour _runningNode;

        private int _runningNodeIndex;

        public Sequence(string name) : base(name)
        {
        }

        public override UpdateStatus Update()
        {
            if (_runningNode != null)
            {
                UpdateStatus status = _runningNode.Update();
                if (status == UpdateStatus.Success)
                {
                    return UpdateNode(_runningNodeIndex + 1);
                }
                
                if (status == UpdateStatus.Failure)
                {
                    ResetRunningNode();
                }
                
                return status;
            }
            
            ResetRunningNode();
            return UpdateNode(0);
        }

        public override void PreUpdate()
        {
            foreach (NodeBehaviour node in _nodes)
            {
                node.PreUpdate();
            }
        }

        public override void PostUpdate()
        {
            foreach (NodeBehaviour node in _nodes)
            {
                node.PostUpdate();
            }
        }

        private UpdateStatus UpdateNode(int startIndex)
        {
            for (int i = startIndex; i < _nodes.Count; ++i)
            {
                NodeBehaviour node = _nodes[i];
                UpdateStatus status = node.Update();
                if (status == UpdateStatus.Running)
                {
                    _runningNode = node;
                    _runningNodeIndex = i;
                    return UpdateStatus.Running;
                }

                if (status == UpdateStatus.Failure)
                {
                    _runningNode = null;
                    return UpdateStatus.Failure;
                }
            }
            
            _runningNode = null;
            return UpdateStatus.Success;
        }

        private void ResetRunningNode()
        {
            _runningNode = null;
            _runningNodeIndex = -1;
        }
    }
}