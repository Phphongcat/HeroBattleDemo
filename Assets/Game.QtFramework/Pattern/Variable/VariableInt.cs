using UnityEngine;
using UnityEngine.Events;

namespace QtNameSpace
{
    [System.Serializable]
    public class VariableInt : IVariable<int>
    {
        [SerializeField] private int value;
        private int _runtimeValue;
        private UnityEvent<int> _valueChange = new();

        public int Value => value;
        
        public int RuntimeValue
        {
            get => _runtimeValue;
            set
            {
                if (_runtimeValue != value)
                {
                    _runtimeValue = value;
                    _valueChange?.Invoke(_runtimeValue);
                }
            }
        }
        
        public void IncrementOverSize(int amount)
        {
            RuntimeValue += amount;
        }
        
        public void Increment(int amount, int clamp = default)
        {
            var newValue = RuntimeValue + amount;
            RuntimeValue = Mathf.Clamp(newValue, clamp, value);
        }

        public void Decrement(int amount, int clamp = default)
        {
            var newValue = RuntimeValue - amount;
            RuntimeValue = Mathf.Clamp(newValue, clamp, value);
        }

        public UnityEvent<int> ValueChange()
        {
            return _valueChange;
        }

        public void Restore()
        {
            RuntimeValue = Value;
        }
    }
}