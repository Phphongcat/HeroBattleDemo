using System;
using QtNameSpace;
using UnityEngine;

public class PlayerFlip : ABaseElement
{
    [SerializeField] private Transform playerTrans;
    [SerializeField] private Vector3 rightFlip = Vector3.zero;
    [SerializeField] private Vector3 leftFlip = new(0, 180, 0);
    
    
    public override void OtherInit()
    {
    }

    public override void OtherRelease()
    {
    }

    public override void UpdateView()
    {
    }

    private void LateUpdate()
    {
        if(IsRelease)
            return;

        var model = GetModel<PlayerInfo>();
        if (model.weapon != null && model.weapon.isAttackHolding.RuntimeValue && model.target != null)
        {
            if (playerTrans.position.x > model.target.position.x)
                playerTrans.rotation = Quaternion.Euler(leftFlip);
            else if (playerTrans.position.x < model.target.position.x)
                playerTrans.rotation = Quaternion.Euler(rightFlip);
            return;
        }

        var direction = JoystickInput.Direction;
        if(direction.magnitude == 0)
            return;

        if (direction.x < 0 && Math.Abs(playerTrans.rotation.eulerAngles.y - leftFlip.y) > 0)
            playerTrans.rotation = Quaternion.Euler(leftFlip);
        else if (direction.x > 0 && Math.Abs(playerTrans.rotation.eulerAngles.y - rightFlip.y) > 0)
            playerTrans.rotation = Quaternion.Euler(rightFlip);
    }
}