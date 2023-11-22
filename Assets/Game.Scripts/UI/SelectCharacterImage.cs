using QtNameSpace;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SelectCharacterImage : ABaseElement
{
    public override void OtherInit()
    {
    }

    public override void OtherRelease()
    {
    }

    public override void UpdateView()
    {
        GetComponent<Image>().sprite = GameConfig.CharacterSkinContainer.SkinOfHeroSelected.avatar;
        GetComponent<Image>().SetNativeSize();
    }
}