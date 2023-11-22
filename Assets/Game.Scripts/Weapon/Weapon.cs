using UnityEngine;

namespace QtNameSpace
{
    public class Weapon : ABasePresenter
    {
        [SerializeField] private QtEventListener listener;
        [SerializeField] private WeaponInfo model;

        
        public override void InitModel()
        {
            model.Restore();
            GameModel.Instance.GetModel<PlayerInfo>().weapon = model;
        }
        
        private void Awake()
        {
            Init();
            listener.StartListening(GameEventID.StartAttackButton, HoldAttack);
            listener.StartListening(GameEventID.EndAttackButton, EndAttack);
        }

        private void FixedUpdate()
        {
            if (model.isAttackHolding.RuntimeValue)
                Attack();
        }

        private void OnDestroy()
        {
            if(IsInvoking(nameof(Countdown)))
                CancelInvoke(nameof(Countdown));
            
            model.Restore();
        }

        private void HoldAttack()
        {
            model.isAttackHolding.Changed(true);
        }

        private void EndAttack()
        {
            model.isAttackHolding.Changed(false);
        }

        private void Attack()
        {
            if(model.IsCountdown)
                return;
            
            if(IsInvoking(nameof(Countdown)))
                CancelInvoke(nameof(Countdown));
            
            model.countdown.Restore();
            var deltaCountdown = model.countdown.RuntimeValue - model.CountdownTime;
            if (deltaCountdown > 0) model.countdown.Decrement(deltaCountdown);

            var view = elements.Find(item => item is IAttacker);
            if (view is not IAttacker attacker)
            {
                model.countdown.Decrement(model.CountdownTime);
                return;
            }

            attacker.Attack();
            InvokeRepeating(nameof(Countdown), default, Time.deltaTime);
        }

        private void Countdown()
        {
            model.countdown.Decrement(Time.deltaTime);
            if (model.IsCountdown is false)
                CancelInvoke(nameof(Countdown));
        }
    }
}