using System;

namespace QtNameSpace
{
    [Serializable]
    public class ScoreCounter : ABaseModel
    {
        public VariableInt score;
        
        
        public override void Restore()
        {
            score.Restore();
        }
    }
}