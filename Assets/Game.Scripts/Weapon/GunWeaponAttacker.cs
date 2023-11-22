using QtNameSpace;
using UnityEngine;

public class GunWeaponAttacker : ABaseElement, IAttacker
{
    [SerializeField] private Transform spawnTrans;
    [SerializeField] private GameObject prefab;


    public override void OtherInit()
    {
    }

    public override void OtherRelease()
    {
    }

    public override void UpdateView()
    {
        prefab = GameConfig.WeaponInfoContainer.Selected.bulletPrefab;
    }
    
    public void Attack()
    {
        if(prefab is null)
            return;
        
        spawnTrans.GetPositionAndRotation(out var position, out var rotation);

        var weaponModel = GameModel.Instance.GetModel<PlayerInfo>().weapon;
        var projectile = Instantiate(prefab, position, rotation).GetComponent<QtNameSpace.Projectile>();
        projectile.SetScaleSize(weaponModel.bulletSizeExtra.RuntimeValue);
        projectile.SetDamaged(weaponModel.atk.RuntimeValue);
    }
}