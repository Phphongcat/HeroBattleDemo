using UnityEngine;

public class JoystickInput
{
    private static Joystick _joystick = null;

    public static Vector2 Direction =>
        _joystick
            ? _joystick.Direction.normalized
            : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    public static void SetJoystick(Joystick joystick)
    {
        _joystick = joystick;
    }
}
