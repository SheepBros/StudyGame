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
        
        private GameManager _gameManager;

        private GameEventManager _gameEventManager;

        private AbilityComponentManager _abilityComponentManager;

        //private Dictionary<Type, Action<IUnit, Vector3>> _instantiateTable = new();

        private Dictionary<Type, GameObject> _instantiateTable = new();

        private List<GameObject> _objectList = new();
        
        [Inject]
        public void InjectBindings(GameManager gameManager, GameEventManager gameEventManager,
            AbilityComponentManager abilityComponentManager)
        {
            _gameManager = gameManager;
            _gameEventManager = gameEventManager;
            _abilityComponentManager = abilityComponentManager;
        }

        public void Initialize()
        {
            _gameEventManager.Register<UnitCreatedEvent>(OnUnitCreated);
            
            // TODO: 개선 요망.
            // _instantiateTable.Add(typeof(WorkerUnit), new PrefabInfo(_workerPrefab, InstantiateWorker));
            // _instantiateTable.Add(typeof(BaseCenterUnit), new PrefabInfo(_baseCenterPrefab, InstantiateBaseCenter));
            // _instantiateTable.Add(typeof(MineralUnit), new PrefabInfo(_mineralPrefab, InstantiateMineral));
            _instantiateTable.Add(typeof(WorkerUnit), _workerPrefab);
            _instantiateTable.Add(typeof(BaseCenterUnit), _baseCenterPrefab);
            _instantiateTable.Add(typeof(MineralUnit), _mineralPrefab);
        }

        public void Dispose()
        {
            _gameEventManager.Unregister<UnitCreatedEvent>(OnUnitCreated);
        }

        private void OnUnitCreated(UnitCreatedEvent eventData)
        {
            if (eventData == null)
            {
                return;
            }

            Type unitType = eventData.Unit.GetType();
            if (_instantiateTable.ContainsKey(unitType))
            {
                //_instantiateTable[unitType].InstantiateEvent.Invoke(eventData.Unit, eventData.Position);
                InstantiateUnit(_instantiateTable[unitType], eventData.Unit, eventData.Position);
            }
        }

        private void InstantiateUnit(GameObject prefab, IUnit unit, Vector3 position)
        {
            GameObject instance = Instantiate(prefab, position, Quaternion.identity);
            UnitObject unitObject = instance.GetComponent<UnitObject>();
            unitObject.SetUp(unit);

            if (unit is IAbilityUnit abilityUnit)
            {
                foreach (IAbility ability in abilityUnit.Abilities)
                {
                    _abilityComponentManager.AddAbilityComponent(unitObject, ability);
                }
            }
            
            _objectList.Add(instance);
        }

        private void InstantiateWorker(IUnit unit, Vector3 position)
        {
            GameObject unitObject = Instantiate(_workerPrefab, position, Quaternion.identity);
            WorkerObject workerObject = unitObject.GetComponent<WorkerObject>();
            workerObject.SetUp(unit);
            _objectList.Add(unitObject);
        }

        private void InstantiateBaseCenter(IUnit unit, Vector3 position)
        {
            GameObject unitObject = Instantiate(_baseCenterPrefab, position, Quaternion.identity);
            BaseCenterObject baseCenterObject = unitObject.GetComponent<BaseCenterObject>();
            baseCenterObject.SetUp(unit);
            _objectList.Add(unitObject);
        }

        private void InstantiateMineral(IUnit unit, Vector3 position)
        {
            GameObject unitObject = Instantiate(_mineralPrefab, position, Quaternion.identity);
            MineralObject mineralObject = unitObject.GetComponent<MineralObject>();
            mineralObject.SetUp(unit);
            _objectList.Add(unitObject);
        }

        private class PrefabInfo
        {
            public readonly GameObject Prefab;

            public readonly Action<IUnit, Vector3> InstantiateEvent;

            public PrefabInfo(GameObject prefab, Action<IUnit, Vector3> instantiateEvent)
            {
                Prefab = prefab;
                InstantiateEvent = instantiateEvent;
            }
        }
    }
}