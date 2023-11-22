using QtNameSpace;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : ABaseElement
{
    [SerializeField] private Transform weaponContainerTrans;
    [SerializeField] private Transform weaponPoint;
    
    [Header("Debug")]
    [SerializeField] private QtNameSpace.Weapon weapon;


    public override void OtherInit()
    {
        if (weapon != null && weapon.IsDestroyed() is false)
            Destroy(weapon);
        
        if(weaponContainerTrans.GetComponentInChildren<QtNameSpace.Weapon>() != null)
            return;

        var ins = Instantiate(
            GameConfig.WeaponInfoContainer.Selected.weaponPrefab,
            Vector3.zero, Quaternion.identity, weaponContainerTrans);
        weapon = ins.GetComponent<QtNameSpace.Weapon>();
        weapon.transform.position = weaponPoint.position;
    }

    public override void OtherRelease()
    {
        weapon.Release();
        if (weapon != null && weapon.IsDestroyed() is false)
            Destroy(weapon);
    }
    
    public override void UpdateView()
    {
    }
}