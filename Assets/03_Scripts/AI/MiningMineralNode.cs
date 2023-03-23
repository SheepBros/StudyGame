using TRTS.Ability;
using TRTS.BehaviourTree;
using TRTS.Unit;

namespace TRTS.AI
{
    public class MiningMineralNode : NodeBehaviour
    {
        private IAbilityUnit _unit;

        private MiningAbility _miningAbility;
        
        public MiningMineralNode(IAbilityUnit unit)
        {
            _unit = unit;
            _miningAbility = _unit.GetAbility<MiningAbility>();
        }
        
        public override UpdateStatus Update()
        {
            if (!_miningAbility.IsAvailable())
            {
                return UpdateStatus.Failure;
            }

            bool inDistance = _miningAbility.IsInDistance();
            if (!inDistance)
            {
                return UpdateStatus.Failure;
            }
            
            if (_miningAbility.IsMining)
            {
                return UpdateStatus.Running;
            }

            if (_miningAbility.MinedAmount > 0)
            {
                return UpdateStatus.Success;
            }

            _miningAbility.Use();
            return UpdateStatus.Running;
        }

        public override void PreUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void PostUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}