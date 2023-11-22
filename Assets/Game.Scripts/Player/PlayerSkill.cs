using QtNameSpace;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill : ABaseElement
{
    [SerializeField] private QtEventListener listener;
    [SerializeField] private GameObject prefab;
    [SerializeField] private SkillInfo model;
    private GameObject _skill;
    

    public override void OtherInit()
    {
        prefab = GameConfig.CharacterSkillContainer.Selected.prefab;
        model = GetModel<PlayerInfo>().skill;
        
        listener.StartListening(GameEventID.SkillButton, ActiveSkill);
    }

    public override void OtherRelease()
    {
        if(IsInvoking(nameof(Countdown)))
            CancelInvoke(nameof(Countdown));
        
        model.Restore();
        if (_skill != null && _skill.IsDestroyed() is false)
            Destroy(_skill);
    }

    public override void UpdateView()
    {
    }

    private void ActiveSkill()
    {
        if(model.IsCountdown)
            return;

        var statModel = GetModel<PlayerInfo>().stat;
        if (model.manaCost.RuntimeValue > statModel.mana.RuntimeValue)
            return;
            
        if(IsInvoking(nameof(Countdown)))
            CancelInvoke(nameof(Countdown));

        model.countdown.Restore();
        statModel.mana.Decrement(model.manaCost.RuntimeValue);
        
        _skill ??= Instantiate(prefab);
        _skill.GetComponent<Skill>().EmitSpell();
        InvokeRepeating(nameof(Countdown), default, Time.deltaTime);
    }

    private void Countdown()
    {
        model.countdown.Decrement(Time.deltaTime);
        if (model.IsCountdown is false) CancelInvoke(nameof(Countdown));
    }
}