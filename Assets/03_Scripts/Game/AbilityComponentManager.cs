using System;
using System.Collections.Generic;
using SB;
using TRTS.Ability;
using TRTS.Unit;
using UnityEngine;

namespace TRTS
{
    public class AbilityComponentManager : MonoBehaviour
    {
        private Dictionary<Type, Type> _abilityComponentTable;

        private void Awake()
        {
            _abilityComponentTable = new Dictionary<Type, Type>
            {
                { typeof(MoveAbility), typeof(MoveAbilityComponent) },
                { typeof(MiningAbility), typeof(MiningAbilityComponent) }
            };
        }

        public void AddAbilityComponent(UnitObject unitObject, IAbility ability)
        {
            if (!_abilityComponentTable.TryGetValue(ability.GetType(), out Type componentType))
            {
                return;
            }
            
            Component component = unitObject.gameObject.AddComponent(componentType);
            if (component is not IAbilityComponent abilityComponent ||
                !abilityComponent.SetUp(unitObject, ability))
            {
                Destroy(component);
                return;
            }
        }
    }
}