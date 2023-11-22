using UnityEngine.Events;

namespace QtNameSpace
{
    public interface IVariable<T>
    {
        public UnityEvent<T> ValueChange();
        public void Restore();
    }
}