using System;
using System.Collections.Generic;

namespace SB.Util
{
    public class IntReactiveProperty : IComparable<IntReactiveProperty>
    {
        public event Action<int> OnValueChanged;
        
        public int Value
        {
            get => _value;
            set
            {
                bool valueChanged = value != _value;
                _value = value;
                if (valueChanged)
                {
                    OnValueChanged?.Invoke(_value);
                }
            }
        }

        public int DirectValue
        {
            set => _value = value;
        }

        private int _value;

        public IntReactiveProperty()
        {
            _value = default;
        }

        public IntReactiveProperty(int value)
        {
            _value = value;
        }

        public int CompareTo(IntReactiveProperty other)
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

        private sealed class ValueRelationalComparer : IComparer<IntReactiveProperty>
        {
            public int Compare(IntReactiveProperty x, IntReactiveProperty y)
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

        public static IComparer<IntReactiveProperty> ValueComparer { get; } = new ValueRelationalComparer();
    }
}