using UnityEngine;

namespace QtNameSpace
{
    [System.Serializable]
    public class PlayerInfo : ABaseModel
    {
        public CharacterStat stat = new();
        public CharacterSkin skin = new();
        public SkillInfo skill = new();
        public WeaponInfo weapon = new();
        public Transform target;

        public bool CanMove => stat.canMove.RuntimeValue;
        public bool IsAttackCountDown => weapon.IsCountdown;
        public bool IsAlive => stat.Alive();
        public float AttackCountdown => weapon.CountdownTime;

        public override void Restore()
        {
            stat?.Restore();
            skin?.Restore();
            skill?.Restore();
            weapon?.Restore();
        }
    }
}