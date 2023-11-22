using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillSOContainer : MonoBehaviour
{
    [SerializeField] private List<CharacterSkillSO> characterSkillSOs;
    private int _index;


    public bool IsMax => _index == characterSkillSOs.Count - 1;
    
    public bool IsMin => _index == default;

    public List<CharacterSkillSO> Full => characterSkillSOs;
    
    public CharacterSkillSO Selected =>
        Full.Find(item => item.characterID == GameConfig.CharacterStatContainer.Selected.id);

    public void Restore()
    {
        _index = default;
    }

    public CharacterSkillSO Next()
    {
        var next = _index + 1;
        _index = Mathf.Clamp(next, default,  characterSkillSOs.Count - 1);
        return characterSkillSOs[_index];
    }

    public CharacterSkillSO Previous()
    {
        var next = _index - 1;
        _index = Mathf.Clamp(next, default,  characterSkillSOs.Count - 1);
        return characterSkillSOs[_index];
    }
}