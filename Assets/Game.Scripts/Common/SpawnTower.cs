using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace QtNameSpace
{
    public class SpawnTower : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Transform spawnTrans;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string format = "{0}";
        [SerializeField] private int timeDelay = 2;
        private bool _isAction;
        private UnityAction<Vector3> _action;
        

        public void Action(UnityAction<Vector3> action)
        {
            if(_isAction)
                return;
            
            _isAction = true;
            _action = action;
            text.SetText(string.Format(format, timeDelay));
            InvokeRepeating(nameof(DisplayDelay), 1, 1);
        }
        
        private void Awake()
        {
            _isAction = false;
            canvas.worldCamera = Camera.main;
        }

        private void OnDisable()
        {
            if(IsInvoking(nameof(DisplayDelay)))
                CancelInvoke(nameof(DisplayDelay));
        }

        private void DisplayDelay()
        {
            timeDelay--;
            if (timeDelay >= 0)
            {
                text.SetText(string.Format(format, timeDelay));
            }
            else
            {
                text.SetText(string.Empty);
                _action?.Invoke(spawnTrans.position);
                
                if(IsInvoking(nameof(DisplayDelay)))
                    CancelInvoke(nameof(DisplayDelay));
            }
        }
    }
}