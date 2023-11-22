using System;

namespace QtNameSpace
{
    [Serializable]
    public class MageSkillInfo : ABaseModel
    {
        public VariableInt damaged;
        public VariableFloat second;


        public override void Restore()
        {
            damaged.Restore();
            second.Restore();
        }
    }
}