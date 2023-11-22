using UnityEngine;

namespace QtNameSpace
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private GameObject bullet;


        public override void Attack()
        {
            if (IsInvoking(nameof(Countdown)))
                CancelInvoke(nameof(Countdown));

            anim.ActiveAttackAnim();

            var current = anim.GetCurrentClipLength();
            var delta = model.CountdownTime - current;
            if (delta < 0)
            {
                var percent = model.CountdownTime / current;
                anim.SetSpeed(percent);
                model.countdown = model.CountdownTime;
            }
            else
            {
                anim.SetSpeed(1);
                model.countdown = delta;
            }

            DoneAttack = false;
            InvokeRepeating(nameof(Countdown), default, Time.fixedDeltaTime);
        }

        protected override void Countdown()
        {
            model.countdown -= Time.deltaTime;

            if (model.countdown <= model.CountdownTime && DoneAttack is false)
            {
                attackPoint.GetPositionAndRotation(out var position, out var rotation);
                var projectile = Instantiate(bullet, position, rotation).GetComponent<Projectile>();
                projectile.SetScaleSize(Vector3.zero);
                projectile.SetDamaged(model.atk);
                DoneAttack = true;
            }

            if (model.IsCountdown is false)
            {
                CancelInvoke(nameof(Countdown));
                anim.ActiveMoveAnim();
                anim.SetSpeed(1);
            }
        }
    }
}