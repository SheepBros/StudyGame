using UnityEngine;

namespace TRTS.Unit
{
    public interface IUnit
    {
        Vector3 Position { get; }
        
        IUnitObject UnitObject { get; }

        float Size { get; }

        void SetObject(IUnitObject unitObject);
        
        void Start();
        
        void Update();
    }
}