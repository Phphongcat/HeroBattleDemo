using System.Collections.Generic;
using UnityEngine;

public class CharacterStatSOContainer : MonoBehaviour
{
    [SerializeField] private List<CharacterStatSO> characterStatSOs;
    private int _index;


    public bool IsMax => _index == characterStatSOs.Count - 1;
    
    public bool IsMin => _index == default;

    public List<CharacterStatSO> Full => characterStatSOs;

    public CharacterStatSO Selected => characterStatSOs[_index];

    public void Restore()
    {
        _index = default;
    }

    public CharacterStatSO Next()
    {
        var next = _index + 1;
        _index = Mathf.Clamp(next, default,  characterStatSOs.Count - 1);
        return characterStatSOs[_index];
    }

    public CharacterStatSO Previous()
    {
        var next = _index - 1;
        _index = Mathf.Clamp(next, default,  characterStatSOs.Count - 1);
        return characterStatSOs[_index];
    }
}
