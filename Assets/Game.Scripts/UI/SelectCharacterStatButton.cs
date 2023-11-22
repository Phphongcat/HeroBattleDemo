using System;
using QtNameSpace;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectCharacterStatButton : ABaseElement
{
    [SerializeField] private NavigatorType navigatorType;
    
    
    [Serializable]
    public enum NavigatorType
    {
        Previous = 0,
        Next = 1,
    }


    public override void OtherInit()
    {
    }

    public override void OtherRelease()
    {
    }

    public override void UpdateView()
    {
        if (GetModel<SelectCharacterBoxModel>().selectType is SelectHeroType.Stat)
        {
            if (navigatorType is NavigatorType.Next)
                GetComponent<Button>().interactable = GameConfig.CharacterStatContainer.IsMax is false;
            else
                GetComponent<Button>().interactable = GameConfig.CharacterStatContainer.IsMin is false;
        }
        else
        {
            if (navigatorType is NavigatorType.Next)
                GetComponent<Button>().interactable = GameConfig.CharacterSkinContainer.IsMax is false;
            else
                GetComponent<Button>().interactable = GameConfig.CharacterSkinContainer.IsMin is false;
        }
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Action);
    }

    private void Action()
    {
        if (GetModel<SelectCharacterBoxModel>().selectType is SelectHeroType.Stat)
        {
            if (navigatorType is NavigatorType.Next)
                GameConfig.CharacterStatContainer.Next();
            else
                GameConfig.CharacterStatContainer.Previous();
        }
        else
        {
            if (navigatorType is NavigatorType.Next)
                GameConfig.CharacterSkinContainer.Next();
            else
                GameConfig.CharacterSkinContainer.Previous();
        }

        GetModel<SelectCharacterBoxModel>().UpdateEvent?.Invoke();
    }
}