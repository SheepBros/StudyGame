using System;
using TRTS.Ability;
using TRTS.BehaviourTree;
using TRTS.Unit;

namespace TRTS.AI
{
    public class GoToNode : NodeBehaviour
    {
        private ICharacterUnit _unit;

        private MoveAbility _moveAbility;

        private Type _targetType;
        
        public GoToNode(string name, ICharacterUnit unit, Type targetType) : base(name)
        {
            _unit = unit;
            _targetType = targetType;
            _moveAbility = _unit.GetAbility<MoveAbility>();
        }

        public override UpdateStatus Update()
        {
            if (!_moveAbility.IsAvailable())
            {
                return UpdateStatus.Failure;
            }

            if (_unit.Target.GetType() != _targetType)
            {
                _moveAbility.MoveToTarget();
            }
            
            if (_moveAbility.IsMoving)
            {
                return UpdateStatus.Running;
            }
            
            if (_moveAbility.HasArrived())
            {
                return UpdateStatus.Success;
            }
            
            _moveAbility.MoveToTarget();
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