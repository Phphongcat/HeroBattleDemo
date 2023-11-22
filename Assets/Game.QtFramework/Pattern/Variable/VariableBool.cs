using System;
using UnityEngine;
using UnityEngine.Events;

namespace QtNameSpace
{
    [Serializable]
    public class VariableBool : IVariable<bool>
    {
        [SerializeField] private bool value;
        private bool _runtimeValue;
        private UnityEvent<bool> _valueChange = new();

        public bool Value => value;
        
        public bool RuntimeValue
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

        public void Changed(bool b)
        {
            RuntimeValue = b;
        }

        public UnityEvent<bool> ValueChange()
        {
            return _valueChange;
        }

        public void Restore()
        {
            RuntimeValue = value;
        }
    }
}