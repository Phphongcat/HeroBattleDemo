using UnityEngine;

namespace QtNameSpace
{
    public class AttackPointLookAtPlayer : MonoBehaviour
    {
        [SerializeField] private EnemyTargetFinder finder;
        [SerializeField] private Transform viewTrans;


        private void Update()
        {
            var playerPoint = finder.FindPlayer();
            if (playerPoint is null)
            {
                viewTrans.localRotation = Quaternion.Euler(Vector3.zero);
                return;
            }

            LookTarget(playerPoint.position);
        }
        
        private void LookTarget(Vector3 target)
        {
            var look = viewTrans.InverseTransformPoint(target);
            var angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
            viewTrans.Rotate(default, default, angle);
        }
    }
}