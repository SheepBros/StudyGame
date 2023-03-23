using System;
using System.Collections.Generic;
using UnityEngine;

namespace SB.Util
{
    public class FloatReactiveProperty : IComparable<FloatReactiveProperty>
    {
        public float Value
        {
            get => _value;
            set
            {
                bool valueChanged = Mathf.Approximately(_value, value);
                _value = value;
                if (valueChanged)
                {
                    OnValueChanged?.Invoke(_value);
                }
            }
        }

        public float DirectValue
        {
            set => _value = value;
        }

        private float _value;

        public event Action<float> OnValueChanged;

        public int CompareTo(FloatReactiveProperty other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return _value.CompareTo(other._value);
        }

        private sealed class ValueRelationalComparer : IComparer<FloatReactiveProperty>
        {
            public int Compare(FloatReactiveProperty x, FloatReactiveProperty y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }

                if (ReferenceEquals(null, y))
                {
                    return 1;
                }

                if (ReferenceEquals(null, x))
                {
                    return -1;
                }

                return x._value.CompareTo(y._value);
            }
        }

        public static IComparer<FloatReactiveProperty> ValueComparer { get; } = new ValueRelationalComparer();
    }
}