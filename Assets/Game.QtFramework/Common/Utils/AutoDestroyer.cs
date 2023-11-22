using UnityEngine;

namespace QtNameSpace
{
    [DisallowMultipleComponent]
    public class AutoDestroyer : MonoBehaviour
    {
        public float destroyedDelay = 1;
        
    
        public void Release()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            if(IsInvoking(nameof(Release)))
                CancelInvoke(nameof(Release));
            
            Invoke(nameof(Release), destroyedDelay);
        }

        private void OnDestroy()
        {
            if(IsInvoking(nameof(Release)))
                CancelInvoke(nameof(Release));
        }
    }
}