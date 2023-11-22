using DG.Tweening;
using QtNameSpace;
using UnityEngine;
using UnityEngine.Events;

public abstract class ABaseMotion : MonoBehaviour, IMotion
{
    [SerializeField] protected float duration = 0.5f;
    [SerializeField] protected Ease easeType = Ease.Linear;

    private Sequence _motion;
    private bool _isDone;
    
    public Sequence Motion
    {
        get => _motion;
        protected set => _motion = value;
    }
    
    public bool IsDone
    {
        get => _isDone;
        protected set => _isDone = value;
    }

    public abstract void Action(UnityAction doneMotionEvent = null);
    public abstract void Back(UnityAction doneMotionEvent = null);

    public void SetDuration(float d)
    {
        duration = d;
    }

    public void Release()
    {
        _motion?.Kill();
        _isDone = true;
    }

    protected void Setup()
    {
        _motion = DOTween.Sequence();
    }

    protected void Play(Tween tWeen, UnityAction doneMotionEvent = null)
    {
        Release();
        _motion.Append(tWeen).onComplete = () =>
        {
            _isDone = true;
            doneMotionEvent?.Invoke();
        };
    }

    protected void Play(Tween[] tweens, UnityAction doneMotionEvent = null)
    {
        Release();
        _motion.Append(tweens[0]);
        if (tweens.Length == 1)
        {
            _motion.Append(tweens[0]).onComplete = () =>
            {
                _isDone = true;
                doneMotionEvent?.Invoke();
            };
        }
        else
        {
            for (var i = 1; i < tweens.Length; i++)
            {
                if (i == default)
                    _motion.Append(tweens[i]);
                else if (i == tweens.Length - 1)
                    _motion.Join(tweens[i]).onComplete = () =>
                    {
                        _isDone = true;
                        doneMotionEvent?.Invoke();
                    };
                else
                    _motion.Join(tweens[i]);
            }
        }
    }

    private void Start()
    {
        if(_motion != null)
            Release();
        else
            Setup();
    }
}
