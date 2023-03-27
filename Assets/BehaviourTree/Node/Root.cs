using System;
using UnityEngine;

namespace TRTS.BT
{
    [Serializable]
    public class Root : NodeBehaviour
    {
        public Root(string name) : base(name)
        {
        }
        
        public override UpdateStatus Update()
        {
            foreach (NodeBehaviour node in _nodes)
            {
                node.Update();
            }

            return UpdateStatus.Success;
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
    }
}