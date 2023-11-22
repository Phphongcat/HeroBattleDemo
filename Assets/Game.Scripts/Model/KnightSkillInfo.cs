using System;

namespace QtNameSpace
{
    [Serializable]
    public class KnightSkillInfo : ABaseModel
    {
        public VariableVector2 bulletSizeBuff;
        public VariableFloat spdAtkBuff;
        public VariableInt damageBuff;
        
        
        public override void Restore()
        {
            bulletSizeBuff.Restore();
            spdAtkBuff.Restore();
            damageBuff.Restore();
        }
    }
}