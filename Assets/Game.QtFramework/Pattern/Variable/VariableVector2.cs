using System;
using UnityEngine;
using UnityEngine.Events;

namespace QtNameSpace
{
    [Serializable]
    public class VariableVector2 : IVariable<Vector2>
    {
        [SerializeField] private Vector2 value;
        private Vector2 _runtimeValue;
        private readonly UnityEvent<Vector2> _valueChange = new();

        public Vector2 Value => value;
        
        public Vector2 RuntimeValue
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

        public void IncrementOverSize(Vector2 amount)
        {
            RuntimeValue += amount;
        }

        public void Increment(Vector2 amount, Vector2 min = default)
        {
            var newValue = RuntimeValue + amount;
            RuntimeValue = new Vector2(
                Mathf.Clamp(newValue.x, min.x, value.x),
                Mathf.Clamp(newValue.y, min.y, value.y));
        }

        public void Decrement(Vector2 amount, Vector2 min = default)
        {
            var newValue = RuntimeValue - amount;
            RuntimeValue = new Vector2(
                Mathf.Clamp(newValue.x, min.x, value.x),
                Mathf.Clamp(newValue.y, min.y, value.y));
        }

        public UnityEvent<Vector2> ValueChange()
        {
            return _valueChange;
        }

        public void Restore()
        {
            RuntimeValue = Value;
        }
    }
}