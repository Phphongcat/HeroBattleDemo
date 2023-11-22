using Game.Scripts.Common;
using Unity.Mathematics;
using UnityEngine;

namespace QtNameSpace
{
    public class Enemy : MonoBehaviour, IHeart, IAttacker
    {
        [SerializeField] protected EnemyStat model;
        [SerializeField] private GameObject deathEffect;
        [SerializeField] private HurtAnim hurtAnimPrefab;
        [SerializeField] protected Transform attackPoint;
        [SerializeField] protected float attackDistance = 0.25f;
        [SerializeField] protected EnemyAnimation anim;
        [SerializeField] protected EnemyTargetFinder finder;
        protected bool DoneAttack;
        
        
        public virtual void TakeDamage(int damage)
        {
            model.health.Decrement(damage);

            if (hurtAnimPrefab != null)
            {
                var hurtA =
                    Instantiate(hurtAnimPrefab, transform.position, quaternion.identity)
                        .GetComponent<HurtAnim>();
                hurtA.SetDamage(damage);
            }
            
            if (model.IsAlive is false)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        public virtual void Attack()
        {
            InvokeRepeating(nameof(Countdown), default, Time.fixedDeltaTime);
        }

        public EnemyStat GetModel()
        {
            return model;
        }

        private void Awake()
        {
            model.Restore();
        }
        
        private void Update()
        {
            var target = finder.FindPlayer();
            if(model.IsCountdown || target is null)
                return;

            if (Vector2.Distance(attackPoint.position, target.position) <= attackDistance)
            {
                model.countdown = model.CountdownTime;
                Attack();
            }
            else
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    target.position,
                    model.speed * Time.deltaTime);
            }
        }
        
        private void OnDestroy()
        {
            if(IsInvoking(nameof(Countdown)))
                CancelInvoke(nameof(Countdown));
        }
        
        protected virtual void Countdown()
        {
            model.countdown -= Time.fixedDeltaTime;
            if (model.IsCountdown is false) CancelInvoke(nameof(Countdown));
        }
    }
}