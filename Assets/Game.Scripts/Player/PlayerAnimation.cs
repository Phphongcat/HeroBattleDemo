using System;
using QtNameSpace;
using UnityEngine;

public class PlayerAnimation : ABaseElement
{
    [SerializeField] private Animator anim;
    [SerializeField] private CharacterAnimationStateEnum state;
    [SerializeField] private AnimationClip idle;
    [SerializeField] private AnimationClip move;
    [SerializeField] private AnimationClip die;


    public override void OtherInit()
    {
        var model = GetModel<PlayerInfo>();
        model.stat.health.ValueChange().AddListener(CheckDeadAmin);
        foreach (var a in model.skin.animationStates)
        {
            switch (a.state)
            {
                case CharacterAnimationStateEnum.Idle:
                    idle = a.clip;
                    break;
                case CharacterAnimationStateEnum.Move:
                    move = a.clip;
                    break;
                case CharacterAnimationStateEnum.Die:
                default:
                    die = a.clip;
                    break;
            }
        }
        anim.runtimeAnimatorController = model.skin.animationController;
    }

    public override void OtherRelease()
    {
    }

    public override void UpdateView()
    {
    }

    private void FixedUpdate()
    {
        if (IsRelease)
            return;

        if (JoystickInput.Direction.magnitude == 0)
        {
            if(state is CharacterAnimationStateEnum.Idle)
                return;
            
            anim.Play(idle.name);
            state = CharacterAnimationStateEnum.Idle;
        }
        else
        {
            if(state is CharacterAnimationStateEnum.Move)
                return;
            
            anim.Play(move.name);
            state = CharacterAnimationStateEnum.Move;
        }
    }

    private void CheckDeadAmin(int arg0)
    {
        if (IsRelease)
            return;
        
        if(GetModel<PlayerInfo>().IsAlive)
            return;
        
        anim.Play(die.name);
        state = CharacterAnimationStateEnum.Die;
    }
}