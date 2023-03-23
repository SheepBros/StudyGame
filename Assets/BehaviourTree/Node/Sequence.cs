using System;
using System.Collections.Generic;

namespace TRTS.BehaviourTree
{
    [Serializable]
    public class Sequence : NodeBehaviour, IComposite
    {
        private List<NodeBehaviour> _childrenList = new();

        private NodeBehaviour _runningNode;

        private int _runningNodeIndex;

        public override UpdateStatus Update()
        {
            if (_runningNode != null)
            {
                UpdateStatus status = _runningNode.Update();
                if (status == UpdateStatus.Success)
                {
                    ResetRunningNode();
                    return UpdateNode(_runningNodeIndex + 1);
                }
                
                return status;
            }
            
            return UpdateNode(0);
        }

        public override void PreUpdate()
        {
            foreach (NodeBehaviour node in _childrenList)
            {
                node.PreUpdate();
            }
        }

        public override void PostUpdate()
        {
            foreach (NodeBehaviour node in _childrenList)
            {
                node.PostUpdate();
            }
        }

        private UpdateStatus UpdateNode(int startIndex)
        {
            for (int i = startIndex; i < _childrenList.Count; ++i)
            {
                NodeBehaviour node = _childrenList[startIndex];
                UpdateStatus status = node.Update();
                if (status == UpdateStatus.Running)
                {
                    _runningNode = node;
                    _runningNodeIndex = i;
                    return UpdateStatus.Running;
                }

                if (status == UpdateStatus.Failure)
                {
                    return UpdateStatus.Failure;
                }
            }

            return UpdateStatus.Success;
        }

        private void ResetRunningNode()
        {
            _runningNode = null;
            _runningNodeIndex = -1;
        }
    }
}