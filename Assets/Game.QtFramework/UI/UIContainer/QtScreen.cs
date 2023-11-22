using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace QtNameSpace
{
    [RequireComponent(typeof(CanvasGroup))]
    public class QtScreen : MonoBehaviour, IContainer
    {
        [Header("Base Config (Can None field)")]
        [SerializeField] protected float durationMotion = 0.5f;
        [SerializeField] protected Ease motionEase = Ease.Linear;
        [SerializeField] protected ContainerStatusEnum status;

        private CanvasGroup _canvasGroup;
    
    
        public ContainerTypeEnum GetContainerType() => ContainerTypeEnum.Screen;
        public ContainerStatusEnum GetContainerStatus() => status;

        public virtual void Open(bool useAmin = true, UnityAction action = null)
        {
            if (status is ContainerStatusEnum.Open)
                return;
        
            status = ContainerStatusEnum.Open;
            if (useAmin)
            {
                _canvasGroup ??= GetComponent<CanvasGroup>();
                _canvasGroup.DOFade(1, durationMotion).SetEase(motionEase).onComplete = () =>
                {
                    SetEnable(true);
                    action?.Invoke();
                };
            }
            else
            {
                SetEnable(true);
                action?.Invoke();
            }
        }

        public virtual void Close(bool useAmin = true, UnityAction action = null)
        {
            if (status is ContainerStatusEnum.Close)
                return;
        
            status = ContainerStatusEnum.Close;
            if (useAmin)
            {
                _canvasGroup ??= GetComponent<CanvasGroup>();
                _canvasGroup.DOFade(0, durationMotion).SetEase(motionEase).onComplete = () =>
                {
                    SetEnable(false);
                    action?.Invoke();
                };
            }
            else
            {
                SetEnable(false);
                action?.Invoke();
            }
        }

        public virtual void SetEnable(bool enable)
        {
            _canvasGroup ??= GetComponent<CanvasGroup>();
            _canvasGroup.alpha = enable ? 1 : 0;
            _canvasGroup.interactable = enable;
            _canvasGroup.blocksRaycasts = enable;
        }
    }
}