using System;

namespace TRTS
{
    public class ReactiveProperty<T> where T : IComparable<T>
    {
        public event Action<T> OnValueChanged;
        
        public T Value
        {
            get => _value;
            set
            {
                T previousValue = _value;
                _value = value;
                
                if (previousValue.CompareTo(_value) != 0)
                {
                    OnValueChanged?.Invoke(value);
                }
            }
        }

        private T _value;

        public ReactiveProperty(T value)
        {
            _value = value;
        }

        public ReactiveProperty()
        {
            _value = default;
        }
    }
}