using System;
using UnityEngine;

namespace QtNameSpace
{
    [Serializable]
    public class WeaponInfo : ABaseModel
    {
        public VariableInt atk = new();
        public VariableFloat speedAtk = new();
        public VariableVector2 bulletSizeExtra = new();
        public VariableFloat distanceRange = new();

        [HideInInspector]
        public VariableFloat countdown = new();
        [HideInInspector]
        public VariableBool isAttackHolding = new();

        public bool IsCountdown => countdown.RuntimeValue > 0;
        public float CountdownTime => 1f / speedAtk.RuntimeValue;
        
        public override void Restore()
        {
            atk.Restore();
            speedAtk.Restore();
            bulletSizeExtra.Restore();
            distanceRange.Restore();
            countdown = new VariableFloat(CountdownTime)
            {
                RuntimeValue = default
            };
        }
    }
}