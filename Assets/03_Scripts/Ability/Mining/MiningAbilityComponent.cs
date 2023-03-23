using TRTS.Unit;
using UnityEngine;

namespace TRTS.Ability
{
    public class MiningAbilityComponent : MonoBehaviour, IAbilityComponent
    {
        private MiningAbility _ability;

        private UnitObject _unitObject;
        
        public bool SetUp(UnitObject unitObject, IAbility ability)
        {
            _unitObject = unitObject;
            _ability = ability as MiningAbility;
            return _ability != null;
        }
    }
}