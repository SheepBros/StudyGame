using UnityEngine;

namespace TRTS.Unit
{
    public interface IUnitObject
    {
        Vector3 Position { get; }
        
        IUnit Unit { get; }

        void SetUp(IUnit unit);
    }
}