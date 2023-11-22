using DG.Tweening;
using UnityEngine.Events;

namespace QtNameSpace
{
    public interface IMotion
    {
        public Sequence Motion
        {
            get;
        }
        public bool IsDone
        {
            get;
        }

        public void SetDuration(float duration);
        public void Release();
        public void Action(UnityAction doneMotionEvent = null);
        public void Back(UnityAction doneMotionEvent = null);
    }
}