using TRTS.Ability;
using TRTS.BehaviourTree;
using TRTS.Unit;
using UnityEngine;

namespace TRTS.AI
{
    public class StoreMineralNode : NodeBehaviour
    {
        private GameManager _gameManager;
        
        private ICharacterUnit _unit;

        private MiningAbility _miningAbility;

        private MoveAbility _moveAbility;
        
        public StoreMineralNode(GameManager gameManager, string name, ICharacterUnit unit) : base(name)
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
            
            if (_unit.Target != _gameManager.BaseCenter)
            {
                return UpdateStatus.Failure;
            }
            
            StoreResourceAbility storeResourceAbility =
                _gameManager.BaseCenter.GetAbility<StoreResourceAbility>();
            if (storeResourceAbility == null)
            {
                return UpdateStatus.Failure;
            }
            
            if (NearInBaseCenter(storeResourceAbility))
            {
                _miningAbility.StoreMineral(storeResourceAbility);
                return UpdateStatus.Success;
            }

            if (_moveAbility.IsMoving)
            {
                return UpdateStatus.Running;
            }

            return UpdateStatus.Failure;
        }

        public override void PreUpdate()
        {
        }

        public override void PostUpdate()
        {
        }

        private bool NearInBaseCenter(StoreResourceAbility storeResourceAbility)
        {
            Vector3 distance = _unit.Position - _unit.Target.Position;
            float mineralDistance = _unit.Size + _unit.Target.Size + storeResourceAbility.StorableLength;
            return distance.sqrMagnitude < mineralDistance * mineralDistance;
        }
    }
}