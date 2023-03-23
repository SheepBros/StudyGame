using TRTS.Unit;
using UnityEngine;

namespace TRTS.Event
{
    public class UnitCreatedEvent
    {
        public readonly IUnit Unit;

        public readonly Vector3 Position;

        public UnitCreatedEvent(IUnit unit, Vector3 position)
        {
            Unit = unit;
            Position = position;
        }
    }
}