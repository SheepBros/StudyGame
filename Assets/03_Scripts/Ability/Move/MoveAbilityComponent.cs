using System;
using TRTS.Unit;
using UnityEngine;

namespace TRTS.Ability
{
    public class MoveAbilityComponent : MonoBehaviour, IAbilityComponent
    {
        private MoveAbility _ability;

        private UnitObject _unitObject;

        private void Update()
        {
            if (_ability == null)
            {
                return;
            }
            
            _ability.Update();
            _unitObject.Rigidbody.velocity = _ability.CurrentVelocity;
        }
        
        public bool SetUp(UnitObject unitObject, IAbility ability)
        {
            _unitObject = unitObject;
            _ability = ability as MoveAbility;
            return _ability != null;
        }
    }
}