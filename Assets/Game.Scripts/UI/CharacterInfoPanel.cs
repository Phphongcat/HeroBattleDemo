using QtNameSpace;
using TMPro;
using UnityEngine;

public class CharacterInfoPanel : ABaseElement
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text hpRegenText;
    [SerializeField] private TMP_Text mpText;
    [SerializeField] private TMP_Text mpRegenText;
    [SerializeField] private TMP_Text speedText;


    public override void OtherInit(){}
    public override void OtherRelease(){}
    public override void UpdateView()
    {
        var selected = GameConfig.CharacterStatContainer.Selected;
        var model = selected.At(GameConfig.Instance.SelectedLevel);
        nameText.SetText($"Name: {selected.id}");
        levelText.SetText($"Level: {model.level.Value}");
        hpText.SetText($"HP: {model.health.Value}");
        hpRegenText.SetText($"HP regen: {model.healthRegen.Value}");
        mpText.SetText($"MP: {model.mana.Value}");
        mpRegenText.SetText($"MP regen: {model.manaRegen.Value}");
        speedText.SetText($"Speed: {model.speed.Value}");
    }
}