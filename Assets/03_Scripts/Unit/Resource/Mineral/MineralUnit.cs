using UnityEngine;

namespace TRTS.Unit
{
    public class MineralUnit : IUnit, IMineralResource
    {
        public Vector3 Position => UnitObject.Position;
        
        public IUnitObject UnitObject { get; private set; }

        public float Size { get; }

        public bool AvailableMining => Amount > 0;

        public int Amount { get; private set; }
        
        public IUnit MiningUnit { get; private set; }

        public MineralUnit(int amount)
        {
            Amount = amount;
            Size = 0.8f;
        }

        public void SetObject(IUnitObject unitObject)
        {
            UnitObject = unitObject;
        }

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void SetMiningSlot(IUnit unit)
        {
            MiningUnit = unit;
        }

        public int Mining(int amount)
        {
            if (!AvailableMining)
            {
                return 0;
            }
            
            int minedMinerals = amount;
            if (Amount < amount)
            {
                minedMinerals = Amount;
                Amount = 0;
            }
            else
            {
                Amount -= amount;
            }

            return minedMinerals;
        }
    }
}