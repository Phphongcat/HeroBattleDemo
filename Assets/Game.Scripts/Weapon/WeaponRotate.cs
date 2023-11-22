using UnityEngine;

namespace QtNameSpace
{
    public class WeaponRotate : ABaseElement
    {
        [SerializeField] private Transform viewTrans;
        
        
        public override void OtherInit()
        {
        }

        public override void OtherRelease()
        {
        }

        public override void UpdateView()
        {
        }

        private void Update()
        {
            if(IsRelease)
                return;

            var model = GetModel<PlayerInfo>();
            if (model.weapon != null && 
                model.weapon.isAttackHolding.RuntimeValue &&
                model.target != null)
            {
                LookTarget(model.target.position);
                return;
            }

            var direction = JoystickInput.Direction;
            if (direction.magnitude == 0)
                viewTrans.rotation = Quaternion.Euler(default, viewTrans.eulerAngles.y, default);
            else
                LookTarget(viewTrans.position + (Vector3)direction);
        }

        private void LookTarget(Vector3 target)
        {
            var look = viewTrans.InverseTransformPoint(target);
            var angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
            viewTrans.Rotate(default, default, angle);
        }
    }
}