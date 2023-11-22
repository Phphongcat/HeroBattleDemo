using System.Collections.Generic;
using UnityEngine;

public class WeaponInfoSOContainer : MonoBehaviour
{
    [SerializeField] private List<WeaponInfoSO> weaponInfoSOs;
    private int _index;
    
    
    public List<WeaponInfoSO> Full => weaponInfoSOs;
    public WeaponInfoSO Selected => weaponInfoSOs[_index];
    public bool IsMin => _index == default;
    public bool IsMax => _index == weaponInfoSOs.Count - 1;
    
    public void Restore()
    {
        _index = default;
    }
    
    public WeaponInfoSO Next()
    {
        var next = _index + 1;
        _index = Mathf.Clamp(next, default,  weaponInfoSOs.Count - 1);
        return weaponInfoSOs[_index];
    }

    public WeaponInfoSO Previous()
    {
        var next = _index - 1;
        _index = Mathf.Clamp(next, default,  weaponInfoSOs.Count - 1);
        return weaponInfoSOs[_index];
    }
}