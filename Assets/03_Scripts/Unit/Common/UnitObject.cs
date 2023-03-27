using UnityEngine;

namespace TRTS.Unit
{
    public class UnitObject : MonoBehaviour, IUnitObject
    {
        public Vector3 Position => _transform.position;

        public IUnit Unit { get; private set; }

        public Rigidbody2D Rigidbody => _rigidbody;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        protected Transform _transform;

        protected virtual void Awake()
        {
            _transform = transform;
        }

        public virtual void SetUp(IUnit unit)
        {
            Unit = unit;
        }
    }
}