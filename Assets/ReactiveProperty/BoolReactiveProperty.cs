using System;
using System.Collections.Generic;

namespace SB.Util
{
    public class BoolReactiveProperty : IComparable<BoolReactiveProperty>
    {
        public event Action<bool> OnValueChanged;
        
        public bool Value
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

        public bool DirectValue
        {
            set => _value = value;
        }

        private bool _value;

        public BoolReactiveProperty()
        {
            _value = default;
        }

        public BoolReactiveProperty(bool value)
        {
            _value = value;
        }

        public static bool operator true(BoolReactiveProperty property)
        {
            return property._value;
        }

        public static bool operator false(BoolReactiveProperty property)
        {
            return property._value == false;
        }

        public static bool operator !(BoolReactiveProperty property)
        {
            return !property._value;
        }
        
        public int CompareTo(BoolReactiveProperty other)
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

        private sealed class ValueRelationalComparer : IComparer<BoolReactiveProperty>
        {
            public int Compare(BoolReactiveProperty x, BoolReactiveProperty y)
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

        public static IComparer<BoolReactiveProperty> ValueComparer { get; } = new ValueRelationalComparer();
    }
}