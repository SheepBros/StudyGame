using TRTS.Unit;
using UnityEngine;

namespace TRTS.Unit
{
    public class BaseCenterUnit : IUnit
    {
        public Vector3 Position { get; }

        public float Size { get; }

        private IUnitObject _unitObject;

        public BaseCenterUnit()
        {
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
    }
}