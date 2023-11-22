using UnityEngine;

namespace QtNameSpace
{
    public class EnemyAnimation : MonoBehaviour
    {
        private static readonly int AttackTrigger = Animator.StringToHash("attack");
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        
        [SerializeField] private Animator anim;


        public void ActiveAttackAnim()
        {
            anim.SetTrigger(AttackTrigger);
        }
        
        public void ActiveMoveAnim()
        {
            anim.SetBool(IsMoving, true);
        }

        public void SetSpeed(float speed)
        {
            var clampSpeed = Mathf.Clamp(speed, default, 1);
            anim.speed = clampSpeed;
        }

        public float GetCurrentClipLength()
        {
            return anim.GetCurrentAnimatorClipInfo(default)[default].clip.length;
        }
        
        private void Start()
        {
            ActiveMoveAnim();
        }
    }
}