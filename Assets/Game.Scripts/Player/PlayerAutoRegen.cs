namespace QtNameSpace
{
    public class PlayerAutoRegen : ABaseElement
    {
        public override void OtherInit()
        {
            if(IsInvoking(nameof(AutoRegen)))
                CancelInvoke(nameof(AutoRegen));
            
            InvokeRepeating(nameof(AutoRegen), default, 1f);
        }

        public override void OtherRelease()
        {
            if(IsInvoking(nameof(AutoRegen)))
                CancelInvoke(nameof(AutoRegen));
        }

        public override void UpdateView()
        {
        }

        private void AutoRegen()
        {
            if(IsRelease)
                return;
            
            var model = GameModel.Instance.GetModel<PlayerInfo>().stat;
            model.health.Increment(model.healthRegen.RuntimeValue);
            model.mana.Increment(model.manaRegen.RuntimeValue);
        }
    }
}