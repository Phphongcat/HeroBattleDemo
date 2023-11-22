using QtNameSpace;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectCharacterConfirmButton : ABaseElement
{
    public override void OtherInit()
    {
    }

    public override void OtherRelease()
    {
    }

    public override void UpdateView()
    {
    }
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener( () =>
        {
            if (GetModel<SelectCharacterBoxModel>().selectType is SelectHeroType.Stat)
                GameConfig.Instance.ConfirmSelectCharacter();
            else
                GameConfig.Instance.ConfirmSelectSkin();
        });
    }
}