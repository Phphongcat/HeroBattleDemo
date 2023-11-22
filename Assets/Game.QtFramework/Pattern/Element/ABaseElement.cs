using UnityEngine;

namespace QtNameSpace
{
    public abstract class ABaseElement : MonoBehaviour, IElement
    {
        protected bool IsRelease;

        public abstract void OtherInit();
        public abstract void OtherRelease();
        
        public abstract void UpdateView();
        
        public  void Init()
        {
            IsRelease = false;
            OtherInit();
        }

        public void Release()
        {
            IsRelease = true;
            OtherRelease();
        }
        
        public virtual T GetModel<T>() where T : ABaseModel
        {
            return GameModel.Instance.GetModel<T>();
        }
        
        private void OnDestroy()
        {
            Release();
        }
    }
}