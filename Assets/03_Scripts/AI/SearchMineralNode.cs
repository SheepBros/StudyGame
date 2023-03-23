using System.Collections.Generic;
using SB;
using TRTS.Ability;
using TRTS.BehaviourTree;
using TRTS.Unit;
using UnityEngine;

namespace TRTS.AI
{
    public class SearchMineralNode : NodeBehaviour
    {
        private GameManager _gameManager;

        private IAbilityUnit _unit;

        private MiningAbility _miningAbility;

        [Inject]
        public void InjectBindings(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public SearchMineralNode(IAbilityUnit unit)
        {
            _unit = unit;
            _miningAbility = _unit.GetAbility<MiningAbility>();
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

                if (mineral.MiningUnit != null)
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
            _miningAbility.SetMine(_unit as IUnit, availableMineList[mineralIndex]);
            return UpdateStatus.Success;
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