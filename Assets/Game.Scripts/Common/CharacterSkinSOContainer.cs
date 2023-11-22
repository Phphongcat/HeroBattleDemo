using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSkinSOContainer : MonoBehaviour
{
    [SerializeField] private List<CharacterSkinSO> characterSkinSOs;
    private int _index;


    public bool IsMax => _index == Selected.skins.Count - 1;
    
    public bool IsMin => _index == default;

    public CharacterSkinSO Selected =>
        characterSkinSOs.Find(item => item.characterID == GameConfig.Instance.SelectedCharacterID);
    
    public List<CharacterSkin> SkinOfHeroFull => Selected.skins;
    
    public CharacterSkin SkinOfHeroSelected => SkinOfHeroFull[_index];
    
    public void Restore()
    {
        _index = default;
    }

    public void Next()
    {
        var next = _index + 1;
        _index = Mathf.Clamp(next, default,  SkinOfHeroFull.Count - 1);
    }

    public void Previous()
    {
        var next = _index - 1;
        _index = Mathf.Clamp(next, default,  SkinOfHeroFull.Count - 1);
    }
}