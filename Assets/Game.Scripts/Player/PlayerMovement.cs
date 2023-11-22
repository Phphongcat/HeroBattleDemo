using QtNameSpace;
using UnityEngine;

public class PlayerMovement : ABaseElement
{
    [Header("Reference")]
    [SerializeField] private Rigidbody2D rig;
    
    [Header("Runtime variable")]
    [SerializeField] private float speed;


    public override void OtherInit()
    {
        GetModel<PlayerInfo>().stat.speed.ValueChange().AddListener(OnSpeedChanged);
    }

    public override void OtherRelease()
    {
    }

    public override void UpdateView()
    {
        speed = GetModel<PlayerInfo>().stat.speed.Value;
    }

    private void FixedUpdate()
    {
        if(IsRelease)
            return;
        
        var model = GetModel<PlayerInfo>();
        if (model is { IsAlive: true })
        {
            var direction = JoystickInput.Direction;
            if (direction.magnitude == 0)
                return;

            var pos = direction * (speed * Time.fixedDeltaTime);
            var newPos = pos + rig.position;
            rig.MovePosition(newPos);
        }
    }

    private void OnSpeedChanged(float spd)
    {
        speed = spd;
    }
}