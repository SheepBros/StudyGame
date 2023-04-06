using System;
using System.Collections.Generic;
using SB;
using TRTS.Ability;
using TRTS.Event;
using TRTS.Unit;
using UnityEngine;

namespace TRTS
{
    public class ObjectSpawnManager : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField]
        private GameObject _workerPrefab;

        [SerializeField]
        private GameObject _baseCenterPrefab;

        [SerializeField]
        private GameObject _mineralPrefab;
        
        private IGameManager _gameManager;

        private IGameEventManager _gameEventManager;

        private AbilityComponentManager _abilityComponentManager;

        private Dictionary<Type, GameObject> _instantiateTable = new();

        private List<GameObject> _objectList = new();
        
        [Inject]
        public void InjectBindings(IGameManager gameManager, IGameEventManager gameEventManager,
            AbilityComponentManager abilityComponentManager)
        {
            _gameManager = gameManager;
            _gameEventManager = gameEventManager;
            _abilityComponentManager = abilityComponentManager;
        }

        public void Initialize()
        {
            _gameEventManager.Register<PrepareUnitsEvent>(OnPrepareUnits);
            
            _instantiateTable.Add(typeof(WorkerUnit), _workerPrefab);
            _instantiateTable.Add(typeof(BaseCenterUnit), _baseCenterPrefab);
            _instantiateTable.Add(typeof(MineralUnit), _mineralPrefab);
        }

        public void Dispose()
        {
            _gameEventManager.Unregister<PrepareUnitsEvent>(OnPrepareUnits);
        }

        public void SetUpUnit(UnitObject unitObject, IUnit unit)
        {
            unitObject.SetUp(unit);

            if (unit is IAbilityUnit abilityUnit)
            {
                foreach (IAbility ability in abilityUnit.Abilities)
                {
                    _abilityComponentManager.AddAbilityComponent(unitObject, ability);
                }
            }
            
            _objectList.Add(unitObject.gameObject);
        }

        private void OnPrepareUnits(PrepareUnitsEvent eventData)
        {
            foreach (IUnit unit in _gameManager.Units)
            {
                if (unit.UnitObject == null)
                {
                    InstantiateUnit(unit);
                }
            }
            
            foreach (IUnit unit in _gameManager.MineralUnits)
            {
                if (unit.UnitObject == null)
                {
                    InstantiateUnit(unit);
                }
            }

            if (_gameManager.BaseCenter.UnitObject == null)
            {
                InstantiateUnit(_gameManager.BaseCenter);
            }
        }

        private void InstantiateUnit(IUnit unit)
        {
            Type unitType = unit.GetType();
            if (_instantiateTable.ContainsKey(unitType))
            {
                InstantiateUnit(_instantiateTable[unitType], unit, unit.Position);
            }
        }

        private void InstantiateUnit(GameObject prefab, IUnit unit, Vector3 position)
        {
            GameObject instance = Instantiate(prefab, position, Quaternion.identity);
            UnitObject unitObject = instance.GetComponent<UnitObject>();
            SetUpUnit(unitObject, unit);
        }
    }
}