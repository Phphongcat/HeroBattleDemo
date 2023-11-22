using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CharacterSkinSO), menuName = "ConfigSO/CharacterSkin")]
public class CharacterSkinSO : ScriptableObject
{
    public CharacterID characterID;
    public List<CharacterSkin> skins;
}
