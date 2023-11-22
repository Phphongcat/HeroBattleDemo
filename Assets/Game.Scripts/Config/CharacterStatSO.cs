using System.Collections.Generic;
using System.Linq;
using QtNameSpace;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CharacterStatSO), menuName = "ConfigSO/CharacterStat")]
public class CharacterStatSO : ScriptableObject
{
    public CharacterID id;
    public List<CharacterStat> characterStats;
    

    public CharacterStat At(int level)
    {
        level = Mathf.Clamp(level, characterStats.First().level.Value, characterStats.Last().level.Value);
        return characterStats.FirstOrDefault(characterStat => characterStat.level.Value == level);
    }
}