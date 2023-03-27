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

        public BoolReactiveProperty IsMoving { get; } = new();

        public bool TargetUnitMoving { get; private set; }

        public Vector3 TargetPosition { get; private set; }

        private ITargetableUnit _targetableUnit;

        public MoveAbility(IUnit owner, float speed)
        {
            Owner = owner;
            Speed = speed;
            
            _targetableUnit = Owner as ITargetableUnit;
        }

        public bool IsAvailable()
        {
            return true;
        }

        public void MoveToTarget()
        {
            TargetUnitMoving = true;
            IsMoving.Value = !HasArrived();
        }

        public void MoveToLocation(Vector3 location)
        {
            TargetPosition = location;
            TargetUnitMoving = false;
            IsMoving.Value = !HasArrived();
        }

        public void Update()
        {
            if (!IsMoving)
            {
                return;
            }
            
            if (HasArrived())
            {
                Stop();
                return;
            }
            
            Vector3 distance = GetTargetLength().normalized;
            CurrentVelocity = distance * Speed;
        }

        public bool HasArrived()
        {
            Vector3 length = GetTargetLength();
            float destinationSize = GetDestinationSize();
            return length.sqrMagnitude < destinationSize * destinationSize;
        }

        public void Stop()
        {
            IsMoving.Value = false;
            CurrentVelocity = Vector2.zero;
        }

        private Vector2 GetTargetLength()
        {
            Vector3 length;
            if (TargetUnitMoving && _targetableUnit.Target != null)
            {
                length = _targetableUnit.Target.Position - Owner.Position;
            }
            else if (!TargetUnitMoving)
            {
                length = TargetPosition - Owner.Position;
            }
            else
            {
                length = Vector3.zero;
            }

            return length;
        }

        private float GetDestinationSize()
        {
            return TargetUnitMoving && _targetableUnit.Target != null ?
                _targetableUnit.Target.Size + Owner.Size : 0;
        }
    }
}