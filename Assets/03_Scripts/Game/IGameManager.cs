using System.Collections.Generic;
using SB.Util;
using TRTS.Unit;

namespace TRTS
{
    public interface IGameManager
    {
        public IntReactiveProperty Minerals { get; }
        
        public List<IUnit> Units { get; }
        
        public List<IUnit> MineralUnits { get; }
        
        public IBuildingUnit BaseCenter { get; }

        public void Start();

        public void Update();

        public WorkerUnit CreateWorker();

        public MineralUnit CreateMineral();

        public IBuildingUnit CreateBaseCenter();
    }
}