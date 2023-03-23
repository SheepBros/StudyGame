using System.Collections.Generic;
using SB;
using TRTS.Event;
using TRTS.Unit;
using UnityEngine;

namespace TRTS
{
    public class GameManager
    {
        public List<IUnit> Units { get; } = new();

        public List<IUnit> MineralList { get; } = new();
        
        public IUnit BaseCenter { get; private set; }

        private GameEventManager _gameEventManager;

        [Inject]
        public void InjectBindings(GameEventManager gameEventManager)
        {
            _gameEventManager = gameEventManager;
        }

        public void Start()
        {
            CreateBaseCenter();
            CreateWorker();
            CreateMineral(new Vector3(-5f, 0, 0));
            CreateMineral(new Vector3(6f, 0, 0));
            
            foreach (IUnit unit in Units)
            {
                unit.Start();
            }
            
            foreach (IUnit mineral in MineralList)
            {
                mineral.Start();
            }
        }

        public void Update()
        {
            foreach (IUnit unit in Units)
            {
                unit?.Update();
            }
        }

        private void CreateWorker()
        {
            IUnit worker = new WorkerUnit(this);
            _gameEventManager.Fire(new UnitCreatedEvent(worker, BaseCenter.Position));
            Units.Add(worker);
        }

        private void CreateMineral(Vector3 position)
        {
            IUnit mineral = new MineralUnit(1000);
            _gameEventManager.Fire(new UnitCreatedEvent(mineral, position));
            MineralList.Add(mineral);
        }

        private void CreateBaseCenter()
        {
            BaseCenter = new BaseCenterUnit();
            _gameEventManager.Fire(new UnitCreatedEvent(BaseCenter, Vector3.zero));
            Units.Add(BaseCenter);
        }
    }
}