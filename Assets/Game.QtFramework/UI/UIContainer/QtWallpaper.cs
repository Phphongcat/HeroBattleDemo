using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QtNameSpace
{
    [RequireComponent(typeof(CanvasGroup))]
    public class QtWallpaper : MonoBehaviour, IContainer
    {
        [Header("Require")]
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected Image background;

        [Header("Base Config (Can None field)")]
        [SerializeField] protected ContainerStatusEnum status;

    
        public ContainerTypeEnum GetContainerType() => ContainerTypeEnum.Wallpaper;
        public ContainerStatusEnum GetContainerStatus() => status;

        public virtual void Open(bool useAmin = true, UnityAction openAction = null)
        {
            if (status is ContainerStatusEnum.Open)
                return;
        
            status = ContainerStatusEnum.Open;
            SetEnable(true);
            openAction?.Invoke();
        }

        public virtual void Close(bool useAmin = true, UnityAction openAction = null)
        {
            if (status is ContainerStatusEnum.Close)
                return;
        
            status = ContainerStatusEnum.Close;
            SetEnable(false);
            openAction?.Invoke();
        }

        public virtual void SetEnable(bool enable)
        {
            canvasGroup ??= GetComponent<CanvasGroup>();
            canvasGroup.alpha = enable ? 1 : 0;
            canvasGroup.interactable = enable;
            canvasGroup.blocksRaycasts = enable;
        }
    }
}