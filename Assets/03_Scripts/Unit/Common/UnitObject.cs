using System;
using System.Collections.Generic;
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

        protected readonly Dictionary<Type, MonoBehaviour> _components = new ();

        protected Transform _transform;

        protected virtual void Awake()
        {
            _transform = transform;
        }

        public virtual void SetUp(IUnit unit)
        {
            Unit = unit;
        }

        public T GetCachedComponent<T>(bool includeChild = false) where T : MonoBehaviour
        {
            if (!_components.TryGetValue(typeof(T), out MonoBehaviour monoBehaviour))
            {
                if (includeChild)
                {
                    return GetComponentInChildren<T>(true);
                }

                return GetComponent<T>();
            }

            return monoBehaviour as T;
        }
    }
}