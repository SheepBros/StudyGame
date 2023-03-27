namespace TRTS.BT
{
    public class BehaviourTree
    {
        private readonly Root _root;
        
        public BehaviourTree(Root root)
        {
            _root = root;
        }

        public void Update()
        {
            if (_root == null)
            {
                return;
            }
            
            _root.PreUpdate();
            _root.Update();
            _root.PostUpdate();
        }
    }
}