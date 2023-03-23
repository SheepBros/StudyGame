using System;

namespace TRTS.BehaviourTree
{
    [Serializable]
    public class Root : NodeBehaviour
    {
        private NodeBehaviour _child;
        
        public override UpdateStatus Update()
        {
            return _child.Update();
        }

        public override void PreUpdate()
        {
            _child.PreUpdate();
        }

        public override void PostUpdate()
        {
            _child.PostUpdate();
        }
    }
}