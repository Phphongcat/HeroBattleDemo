using UnityEngine;

[CreateAssetMenu(fileName = nameof(CharacterSkillSO), menuName = "ConfigSO/CharacterSkill")]
public class CharacterSkillSO : ScriptableObject
{
    public CharacterID characterID;
    public SkillInfo skillInfo;
    public GameObject prefab;
}