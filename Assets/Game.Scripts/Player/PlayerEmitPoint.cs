using UnityEngine;

namespace QtNameSpace
{
    public class PlayerEmitPoint : MonoBehaviour
    {
        [SerializeField] private Transform emitPoint;
        
        
        public Transform GetEmitPoint()
        {
            return emitPoint;
        }
    }
}