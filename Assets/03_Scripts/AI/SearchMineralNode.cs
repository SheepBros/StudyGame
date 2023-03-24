using System.Collections.Generic;
using TRTS.Ability;
using TRTS.BehaviourTree;
using TRTS.Unit;
using UnityEngine;

namespace TRTS.AI
{
    public class SearchMineralNode : NodeBehaviour
    {
        private GameManager _gameManager;

        private ICharacterUnit _unit;

        private MiningAbility _miningAbility;

        private MoveAbility _moveAbility;

        public SearchMineralNode(GameManager gameManager, string name, ICharacterUnit unit) : base(name)
        {
            _gameManager = gameManager;
            _unit = unit;
            _miningAbility = _unit.GetAbility<MiningAbility>();
            _moveAbility = _unit.GetAbility<MoveAbility>();
        }
        
        public override UpdateStatus Update()
        {
            if (_miningAbility == null)
            {
                _miningAbility = _unit.GetAbility<MiningAbility>();
                if (_miningAbility == null)
                {
                    return UpdateStatus.Failure;
                }
            }
            
            List<MineralUnit> availableMineList = null;
            for (int i = 0; i < _gameManager.MineralList.Count; i++)
            {
                if (_gameManager.MineralList[i] is not MineralUnit mineral)
                {
                    continue;
                }

                if (mineral.MiningUnit != null ||
                    !mineral.AvailableMining)
                {
                    continue;
                }

                availableMineList ??= new List<MineralUnit>();
                availableMineList.Add(mineral);
            }

            if (availableMineList == null ||
                availableMineList.Count == 0)
            {
                return UpdateStatus.Failure;
            }

            int mineralIndex = Random.Range(0, availableMineList.Count);
            _unit.SetTarget(availableMineList[mineralIndex]);
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