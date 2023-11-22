using UnityEngine;

namespace QtNameSpace
{
    public class EnemyTargetFinder : MonoBehaviour
    {
        public Transform FindPlayer()
        {
            return FindObjectOfType<PlayerEmitPoint>()?.GetEmitPoint();
        }
    }
}