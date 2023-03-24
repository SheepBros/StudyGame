using TRTS.Ability;
using TRTS.BehaviourTree;
using TRTS.Unit;

namespace TRTS.AI
{
    public class MiningMineralNode : NodeBehaviour
    {
        private ICharacterUnit _unit;

        private MiningAbility _miningAbility;
        
        public MiningMineralNode(string name, ICharacterUnit unit) : base(name)
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

            if (_unit.Target == null ||
                _unit.Target is not MineralUnit mineralUnit)
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

            _miningAbility.SetMine(_unit, mineralUnit);
            _miningAbility.StartMine();
            return UpdateStatus.Running;
        }

        public override void PreUpdate()
        {
        }

        public override void PostUpdate()
        {
        }
    }
}