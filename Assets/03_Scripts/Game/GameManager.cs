using System.Collections.Generic;
using SB;
using SB.Util;
using TRTS.AI;
using TRTS.BT;
using TRTS.Event;
using TRTS.Unit;

namespace TRTS
{
    public class GameManager : IGameManager
    {
        public IntReactiveProperty Minerals { get; } = new();
        
        public List<IUnit> Units { get; } = new();
        
        public List<IUnit> MineralUnits { get; } = new();
        
        public IBuildingUnit BaseCenter { get; private set; }
        
        private IGameEventManager _gameEventManager;
        
        [Inject]
        public void InjectBindings(IGameEventManager gameEventManager)
        {
            _gameEventManager = gameEventManager;
        }

        public void Start()
        {
            _gameEventManager.Fire(new PrepareUnitsEvent());
            
            foreach (IUnit unit in Units)
            {
                unit.Start();
            }
            
            foreach (IUnit mineral in MineralUnits)
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

        public WorkerUnit CreateWorker()
        {
            WorkerUnit worker = new WorkerUnit(this);
            BehaviourTreeBluePrinter aiBluePrinter = new BehaviourTreeBluePrinter();
            aiBluePrinter.Start("root").
                AddNode(new Selector("selector")).
                    AddNode(new Sequence("store_mineral_sequence")).
                        AddNode(new ThinkStoreMineralNode("think_store_mineral", this, worker)).PointerUp().
                        AddNode(new GoToNode("go_base_center", worker, typeof(BaseCenterUnit))).PointerUp().
                        AddNode(new StoreMineralNode(this, "go_store_mined_mineral", worker)).PointerUp().PointerUp().
                    AddNode(new Sequence("mining_sequence")).
                        AddNode(new SearchMineralNode("search_mineral", this, worker)).PointerUp().
                        AddNode(new GoToNode("go_mineral", worker, typeof(MineralUnit))).PointerUp().
                        AddNode(new MiningMineralNode("mining", worker));

            BehaviourTree behaviourTree = aiBluePrinter.End();
            worker.SetAI(behaviourTree);
            
            Units.Add(worker);
            return worker;
        }

        public MineralUnit CreateMineral()
        {
            MineralUnit mineral = new MineralUnit(1000);
            MineralUnits.Add(mineral);
            return mineral;
        }

        public IBuildingUnit CreateBaseCenter()
        {
            BaseCenter = new BaseCenterUnit(this);
            return BaseCenter;
        }
    }
}