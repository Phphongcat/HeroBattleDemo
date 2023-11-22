using System;
using QtNameSpace;
using UnityEngine.Events;

[Serializable]
public class SelectCharacterBoxModel : ABaseModel
{
    public SelectHeroType selectType;
    public UnityEvent UpdateEvent
    {
        get;
        private set;
    } = new();
    

    public override void Restore()
    {
        UpdateEvent.RemoveAllListeners();
    }
}