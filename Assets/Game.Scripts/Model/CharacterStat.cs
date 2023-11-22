namespace QtNameSpace
{
    [System.Serializable]
    public class CharacterStat : ABaseModel
    {
        public VariableInt level = new();
        public VariableInt health = new();
        public VariableInt healthRegen = new();
        public VariableInt mana = new();
        public VariableInt manaRegen = new();
        public VariableFloat speed = new();
        public VariableBool canMove = new();

        
        public bool Alive() => health.RuntimeValue > 0;

        public override void Restore()
        {
            health.Restore();
            healthRegen.Restore();
            mana.Restore();
            manaRegen.Restore();
            speed.Restore();
            canMove.Restore();
        }
    }
}