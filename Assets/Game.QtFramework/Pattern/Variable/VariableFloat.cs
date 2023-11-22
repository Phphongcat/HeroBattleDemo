using System;
using UnityEngine;
using UnityEngine.Events;

namespace QtNameSpace
{
    [Serializable]
    public class VariableFloat : IVariable<float>
    {
        [SerializeField] private float value;
        private float _runtimeValue;
        private UnityEvent<float> _valueChange = new();

        public float Value => value;

        public float RuntimeValue
        {
            get => _runtimeValue;
            set
            {
                if (Math.Abs(_runtimeValue - value) > 0)
                {
                    _runtimeValue = value;
                    _valueChange?.Invoke(_runtimeValue);
                }
            }
        }

        public void IncrementOverSize(float amount)
        {
            RuntimeValue += amount;
        }

        public void Increment(float amount, float clamp = default)
        {
            var newValue = RuntimeValue + amount;
            RuntimeValue = Mathf.Clamp(newValue, clamp, value);
        }

        public void Decrement(float amount, float clamp = default)
        {
            var newValue = RuntimeValue - amount;
            RuntimeValue = Mathf.Clamp(newValue, clamp, value);
        }

        public UnityEvent<float> ValueChange()
        {
            return _valueChange;
        }

        public void Restore()
        {
            RuntimeValue = Value;
        }

        public VariableFloat(float newValue)
        {
            value = newValue;
        }

        public VariableFloat()
        {
            
        }
    }
}