using TRTS.Unit;
using UnityEngine;

namespace TRTS.Unit
{
    public interface IUnit
    {
        Vector3 Position { get; }
        
        float Size { get; }

        void SetObject(IUnitObject unitObject);
        
        void Start();
        
        void Update();
    }
}