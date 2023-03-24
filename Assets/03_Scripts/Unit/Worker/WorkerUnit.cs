using System.Collections.Generic;
using TRTS.Ability;
using UnityEngine;

namespace TRTS.Unit
{
    public class WorkerUnit : ICharacterUnit
    {
        public Vector3 Position => _unitObject.Position;

        public float Size { get; private set; }

        public List<IAbility> Abilities { get; } = new();

        public IUnit Target { get; set; }

        public IResource Resource { get; set; }

        private readonly GameManager _gameManager;

        //private readonly WorkerFsm _ai;

        private IUnitObject _unitObject;

        private bool _patrolling;

        public WorkerUnit(GameManager gameManager)
        {
            _gameManager = gameManager;

            Size = 2f;

            //_ai = new WorkerFsm(_gameManager, this);
            Abilities.Add(new MoveAbility(this, 2f));
            Abilities.Add(new MiningAbility(this, 0.5f, 5, 3f));
        }

        public void SetObject(IUnitObject unitObject)
        {
            _unitObject = unitObject;
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

        public void Start()
        {
            //_ai.Start();
        }

        public void Update()
        {
            //_ai.Update();
        }

        public void SetTarget(IUnit target)
        {
            Target = target;
        }
    }
}