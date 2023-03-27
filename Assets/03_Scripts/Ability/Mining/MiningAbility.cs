using SB.Util;
using TRTS.Unit;
using UnityEngine;

namespace TRTS.Ability
{
    public class MiningAbility : IAbility
    {
        public IUnit Owner { get; private set; }

        public float MineDistance { get; private set; }
        
        public int MiningAmount { get; private set; }

        public BoolReactiveProperty IsMining { get; } = new();
        
        public float MiningTime { get; private set; }
        
        public float CurrentMiningTime { get; private set; }
        
        public int MinedAmount { get; private set; }

        public MineralUnit TargetMineral { get; private set; }
        
        public MiningAbility(IUnit owner, float mineDistance, int miningAmount, float miningTime)
        {
            Owner = owner;
            MineDistance = mineDistance;
            MiningAmount = miningAmount;
            MiningTime = miningTime;
        }

        public bool IsAvailable()
        {
            return MinedAmount <= 0;
        }

        public void StartMining()
        {
            if (!IsMineralMinable())
            {
                return;
            }

            IsMining.Value = true;
            CurrentMiningTime = 0;
        }

        public void StopMining()
        {
            IsMining.Value = false;
            CurrentMiningTime = 0;
        }

        public void Update()
        {
            if (!IsMining)
            {
                return;
            }

            Debug.LogError($"Mining: {!IsMineralMinable()} / {!IsInDistance()} {CurrentMiningTime} >= {MiningTime}");
            if (!IsMineralMinable() ||
                !IsInDistance())
            {
                IsMining.Value = false;
                CurrentMiningTime = 0;
                return;
            }

            CurrentMiningTime += Time.deltaTime;
            if (CurrentMiningTime >= MiningTime)
            {
                IsMining.Value = false;
                CurrentMiningTime = 0;
                MinedAmount = TargetMineral.Mining(MiningAmount);
                TargetMineral = null;
            }
        }
        
        public void SetMine(IUnit owner, MineralUnit mineralUnit)
        {
            if (mineralUnit == null)
            {
                return;
            }
            
            Owner = owner;
            TargetMineral = mineralUnit;
        }

        public bool IsMineralMinable()
        {
            return MinedAmount <= 0 &&
                   TargetMineral != null &&
                   TargetMineral.AvailableMining;
        }

        public bool IsInDistance()
        {
            Vector3 distance = Owner.Position - TargetMineral.Position;
            float mineralDistance = Owner.Size + TargetMineral.Size + MineDistance;
            return distance.sqrMagnitude < mineralDistance * mineralDistance;
        }

        public void StoreMineral(StoreResourceAbility storeResourceAbility)
        {
            storeResourceAbility.StoreMineral(MinedAmount);
            MinedAmount = 0;
        }
    }
}