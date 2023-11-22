using UnityEngine;

namespace QtNameSpace
{
    public class EnemyFlip : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        [SerializeField] private EnemyTargetFinder finder;
        [SerializeField] private Vector3 flipRight = new(0, 180, 0);
        [SerializeField] private Vector3 flipLeft = Vector3.zero;
        
        
        private void Update()
        {
            var target = finder.FindPlayer();
            if(enemy.GetModel().IsCountdown || target is null)
                return;
        
            if (target.position.x - enemy.transform.position.x > 0)
                enemy.transform.rotation = Quaternion.Euler(flipRight.x, flipRight.y, flipRight.z);
            else if (target.position.x - enemy.transform.position.x < 0)
                enemy.transform.rotation = Quaternion.Euler(flipLeft.x, flipLeft.y, flipLeft.z);
        }
    }
}