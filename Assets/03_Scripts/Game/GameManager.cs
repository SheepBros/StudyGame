using System.Collections.Generic;
using SB;
using SB.Util;
using TRTS.AI;
using TRTS.BehaviourTree;
using TRTS.Event;
using TRTS.Unit;
using UnityEngine;

namespace TRTS
{
    public class GameManager
    {
        public IntReactiveProperty Minerals { get; } = new();
        
        public List<IUnit> Units { get; } = new();
        
        public List<IUnit> MineralList { get; } = new();
        
        public IBuildingUnit BaseCenter { get; private set; }
        
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
            WorkerUnit worker = new WorkerUnit(this);
            BehaviourTreeBluePrinter aiBluePrinter = new BehaviourTreeBluePrinter();
            aiBluePrinter.Start("root").
                AddNode(new Selector("selector")).
                    AddNode(new Sequence("store_mineral_sequence")).
                        AddNode(new HasMineralNode("mined_mineral", worker)).
                        AddNode(new GoToNode("go_base_center", worker, typeof(BaseCenterUnit))).PointerUp().
                        AddNode(new StoreMineralNode(this, "go_store_mined_mineral", worker)).
                    AddNode(new Sequence("mining_sequence")).
                        AddNode(new SearchMineralNode(this, "search_mineral", worker)).PointerUp().
                        AddNode(new GoToNode("go_mineral", worker, typeof(MineralUnit))).PointerUp().
                        AddNode(new MiningMineralNode("mining", worker));

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
            BaseCenter = new BaseCenterUnit(this);
            _gameEventManager.Fire(new UnitCreatedEvent(BaseCenter, Vector3.zero));
            Units.Add(BaseCenter);
        }
    }
}