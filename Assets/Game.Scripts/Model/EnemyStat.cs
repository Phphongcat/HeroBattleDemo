using System;
using QtNameSpace;
using UnityEngine;

[Serializable]
public class EnemyStat : ABaseModel
{
    public int score;
    public VariableInt health;
    public int atk;
    public float speedAtk;
    public float speed;
    
    [HideInInspector] 
    public float countdown;
    [HideInInspector]
    public Transform target;

    
    public bool IsCountdown => countdown > 0;
    public bool IsAlive => health.RuntimeValue > 0;

    public float CountdownTime => speedAtk <= 0.5f
        ? VariableRule.MaxAttackCountdownTime
        : Mathf.Max(1f / speedAtk, VariableRule.MinAttackCountdownTime);
    
    
    public override void Restore()
    {
        health.Restore();
        countdown = default;
    }
}