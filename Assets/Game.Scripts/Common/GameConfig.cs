using UnityEngine;

public sealed class GameConfig : Singleton<GameConfig>
{
    [Header("References")]
    public CharacterStatSOContainer characterStatSoContainer;
    public CharacterSkinSOContainer characterSkinSoContainer;
    public CharacterSkillSOContainer characterSkillSoContainer;
    public WeaponInfoSOContainer weaponInfoSoContainer;


    public static CharacterStatSOContainer CharacterStatContainer => Instance.characterStatSoContainer;
    public static CharacterSkinSOContainer CharacterSkinContainer => Instance.characterSkinSoContainer;
    public static CharacterSkillSOContainer CharacterSkillContainer => Instance.characterSkillSoContainer;
    public static WeaponInfoSOContainer WeaponInfoContainer => Instance.weaponInfoSoContainer;
    public int SelectedLevel => 1;
    public CharacterID SelectedCharacterID => characterStatSoContainer.Selected.id;

    public void ConfirmSelectCharacter()
    {
        ObserverEvent.EmitEvent(GameEventID.SelectCharacter);
    }

    public void ConfirmSelectSkin()
    {
        ObserverEvent.EmitEvent(GameEventID.SelectSkin);
    }
}
