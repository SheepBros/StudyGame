using TRTS.Ability;
using TRTS.BehaviourTree;
using TRTS.Unit;

namespace TRTS.AI
{
    public class HasMineralNode : NodeBehaviour
    {
        private IAbilityUnit _unit;

        private MiningAbility _miningAbility;
        
        public HasMineralNode(string name, IAbilityUnit unit) : base(name)
        {
            _unit = unit;
            _miningAbility = _unit.GetAbility<MiningAbility>();
        }

        public override UpdateStatus Update()
        {
            if (_miningAbility.MinedAmount <= 0)
            {
                return UpdateStatus.Failure;
            }
            
            return _miningAbility.MinedAmount > 0 ? UpdateStatus.Success : UpdateStatus.Failure;
        }

        public override void PreUpdate()
        {
        }

        public override void PostUpdate()
        {
        }
    }
}