using System.Collections.Generic;
using TRTS.Ability;
using TRTS.BT;
using UnityEngine;

namespace TRTS.Unit
{
    public class WorkerUnit : ICharacterUnit
    {
        public Vector3 Position => UnitObject.Position;

        public IUnitObject UnitObject { get; private set; }

        public float Size { get; private set; }

        public List<IAbility> Abilities { get; } = new();

        public IUnit Target { get; set; }

        public IResource Resource { get; set; }

        private readonly GameManager _gameManager;
        
        private BehaviourTree _behaviourTree;
        
        private bool _patrolling;
        
        public WorkerUnit(GameManager gameManager)
        {
            _gameManager = gameManager;

            Size = 0.5f;

            Abilities.Add(new MoveAbility(this, 2f));
            Abilities.Add(new MiningAbility(this, 0.5f, 5, 3f));
        }

        public void SetObject(IUnitObject unitObject)
        {
            UnitObject = unitObject;
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
        }

        public void Update()
        {
            _behaviourTree.Update();
        }

        public void SetTarget(IUnit target)
        {
            Target = target;
        }

        public void SetAI(BehaviourTree behaviourTree)
        {
            _behaviourTree = behaviourTree;
        }
    }
}