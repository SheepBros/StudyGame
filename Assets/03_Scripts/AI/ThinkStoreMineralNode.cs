using TRTS.Ability;
using TRTS.BT;
using TRTS.Unit;
using UnityEngine;

namespace TRTS.AI
{
    public class ThinkStoreMineralNode : NodeBehaviour
    {
        private GameManager _gameManager;
        
        private ICharacterUnit _unit;

        private MiningAbility _miningAbility;

        private MoveAbility _moveAbility;

        public ThinkStoreMineralNode(string name, GameManager gameManager, ICharacterUnit unit) : base(name)
        {
            _gameManager = gameManager;
            _unit = unit;
            _miningAbility = _unit.GetAbility<MiningAbility>();
            _moveAbility = _unit.GetAbility<MoveAbility>();
        }

        public override UpdateStatus Update()
        {
            if (_miningAbility.MinedAmount <= 0)
            {
                return UpdateStatus.Failure;
            }

            _unit.SetTarget(_gameManager.BaseCenter);
            return UpdateStatus.Success;
        }

        public override void PreUpdate()
        {
        }

        public override void PostUpdate()
        {
        }
    }
}