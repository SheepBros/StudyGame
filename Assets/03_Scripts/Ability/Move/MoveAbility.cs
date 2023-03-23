using System;
using SB.Util;
using TRTS.Unit;
using UnityEngine;

namespace TRTS.Ability
{
    public class MoveAbility : IAbility
    {
        public IUnit Owner { get; }
        
        public Vector2 CurrentVelocity { get; private set; }

        public float Speed { get; private set; }

        private BoolReactiveProperty IsMoving { get; } = new();

        private IUnit _target;

        private Vector3 _targetPosition;

        public MoveAbility(IUnit owner, float speed)
        {
            Owner = owner;
            Speed = speed;
        }

        public bool IsAvailable()
        {
            return true;
        }

        public void Use()
        {
            IsMoving.Value = true;
        }

        public void Update()
        {
            if (!IsMoving)
            {
                return;
            }

            Vector3 length;
            float destinationSize = 0;
            if (_target != null)
            {
                length = _target.Position - Owner.Position;
                destinationSize = _target.Size;
            }
            else
            {
                length = _targetPosition - Owner.Position;
            }

            if (length.sqrMagnitude < destinationSize * destinationSize)
            {
                Stop();
                return;
            }
            
            Vector3 distance = length.normalized;
            CurrentVelocity = distance * Speed;
        }

        public void Stop()
        {
            IsMoving.Value = false;
            _target = null;
        }

        public void SetTarget(IUnit target)
        {
            _target = target;
        }

        public void SetPosition(Vector3 position)
        {
            _targetPosition = position;
        }
    }
}