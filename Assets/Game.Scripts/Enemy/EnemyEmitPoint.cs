using UnityEngine;

namespace QtNameSpace
{
    public class EnemyEmitPoint : MonoBehaviour
    {
        [SerializeField] private Transform emitPoint;
        
        
        public Transform GetEmitPoint()
        {
            return emitPoint;
        }
    }
}