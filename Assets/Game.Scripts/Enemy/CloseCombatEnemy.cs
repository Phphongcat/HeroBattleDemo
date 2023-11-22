using System.Collections.Generic;
using UnityEngine;

public class CloseCombatEnemy : QtNameSpace.Enemy
{
    [SerializeField] private Collider2D coll2D;
    [SerializeField] private LayerMask mask;
    

    public override void Attack()
    {
        if(IsInvoking(nameof(Countdown)))
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
            DoneAttack = true;
            
            var collider2Ds = new List<Collider2D>();
            var contact2D = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = mask
            };
            
            Physics2D.OverlapCollider(coll2D, contact2D, collider2Ds);
            foreach (var coll in collider2Ds)
            {
                if(coll is null || coll.GetComponent<IHeart>() is not { } heart)
                    continue;
                heart.TakeDamage(model.atk);
            }
        }
        
        if (model.IsCountdown is false)
        {
            CancelInvoke(nameof(Countdown));
            anim.ActiveMoveAnim();
            anim.SetSpeed(1);
        }
    }
}