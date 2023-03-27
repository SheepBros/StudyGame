using System.Collections.Generic;
using TRTS.Ability;
using UnityEngine;

namespace TRTS.Unit
{
    public class BaseCenterUnit : IBuildingUnit
    {
        public Vector3 Position { get; }

        public IUnitObject UnitObject { get; private set; }

        public float Size { get; }

        public List<IAbility> Abilities { get; } = new();

        private GameManager _gameManager;

        public BaseCenterUnit(GameManager gameManager)
        {
            _gameManager = gameManager;
            
            Size = 1f;
            Abilities.Add(new StoreResourceAbility(_gameManager, 0.25f));
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

        public TAbility GetAbility<TAbility>() where TAbility : class, IAbility
        {
            foreach (IAbility ability in Abilities)
            {
                if (ability is TAbility found)
                {
                    return found;
                }
            }

            return null;
        }
    }
}