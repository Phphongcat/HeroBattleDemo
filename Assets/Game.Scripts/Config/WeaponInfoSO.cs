using UnityEngine;

[CreateAssetMenu(fileName = nameof(WeaponInfoSO), menuName = "ConfigSO/Weapon")]
public class WeaponInfoSO : ScriptableObject
{
    public WeaponID weaponID;
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;
}
