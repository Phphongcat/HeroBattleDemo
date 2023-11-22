using System.Collections.Generic;
using QtNameSpace;
using UnityEngine;

[System.Serializable]
public class CharacterSkin : ABaseModel
{
    public Sprite avatar;
    public RuntimeAnimatorController animationController;
    public List<CharacterAnimationState> animationStates;
    
    
    public override void Restore()
    {
        
    }
}