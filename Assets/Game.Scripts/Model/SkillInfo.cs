using System;
using QtNameSpace;
using UnityEngine;

[Serializable]
public class SkillInfo : ABaseModel
{
    public VariableInt manaCost;
    public VariableFloat spellCountdown;

    [HideInInspector] 
    public VariableFloat countdown;
    
    
    public bool IsCountdown => countdown.RuntimeValue > 0;
    public float CountdownTime => spellCountdown.RuntimeValue;
    
    public override void Restore()
    {
        manaCost.Restore();
        spellCountdown.Restore();
        countdown = new VariableFloat(CountdownTime)
        {
            RuntimeValue = default
        };
    }
}