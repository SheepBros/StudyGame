using System;
using System.Collections.Generic;

namespace TRTS.BehaviourTree
{
    [Serializable]
    public abstract class NodeBehaviour
    {
        protected List<NodeBehaviour> _nodes = new ();

        public abstract UpdateStatus Update();

        public abstract void PreUpdate();

        public abstract void PostUpdate();
    }
}