using TMPro;
using UnityEngine;

namespace Game.Scripts.Common
{
    public class HurtAnim : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string format = "-{0}";


        public void SetDamage(int value)
        {
            canvas.worldCamera = Camera.current;
            text.SetText(string.Format(format, value));
        }
    }
}