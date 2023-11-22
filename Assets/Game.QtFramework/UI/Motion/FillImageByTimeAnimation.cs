using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QtNameSpace.UI.Motion
{
    public class FillImageByTimeAnimation : ABaseMotion
    {
        [SerializeField] private Image fillImage;


        public override void Action(UnityAction doneMotionEvent = null)
        {
            fillImage.fillAmount = 0;
            Play(fillImage.DOFillAmount(1, duration).SetEase(easeType), doneMotionEvent);
        }

        public override void Back(UnityAction doneMotionEvent = null)
        {
            fillImage.fillAmount = 1;
            Play(fillImage.DOFillAmount(0, duration).SetEase(easeType), doneMotionEvent);
        }
    }
}