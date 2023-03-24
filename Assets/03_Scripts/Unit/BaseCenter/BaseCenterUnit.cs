using System.Collections.Generic;
using TRTS.Ability;
using UnityEngine;

namespace TRTS.Unit
{
    public class BaseCenterUnit : IBuildingUnit
    {
        public Vector3 Position { get; }

        public float Size { get; }

        public List<IAbility> Abilities { get; } = new();

        private GameManager _gameManager;

        private IUnitObject _unitObject;

        public BaseCenterUnit(GameManager gameManager)
        {
            _gameManager = gameManager;
            
            Size = 4f;
        }

        public void SetObject(IUnitObject unitObject)
        {
            _unitObject = unitObject;
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