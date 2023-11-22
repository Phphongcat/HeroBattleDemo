using QtNameSpace;
using UnityEngine;

[RequireComponent(typeof(Skill))]
public class KnightSkill : ABaseElement, ISpell
{
    [SerializeField] private KnightSkillInfo model;
    [SerializeField] private GameObject view;
    
    
    public override void OtherInit()
    {
        view.SetActive(false);
        model.Restore();
        GameModel.Instance.AddOrUpdateModel(model);
    }

    public override void OtherRelease()
    {
        GameModel.Instance.RemoveModel<KnightSkillInfo>();
        Destroy(gameObject);
    }

    public override void UpdateView()
    {
    }

    public void EmitSpell()
    {
        view.SetActive(true);
        
        var weapon = GameModel.Instance.GetModel<PlayerInfo>().weapon;
        var deltaTime = 1f / (weapon.speedAtk.RuntimeValue + model.spdAtkBuff.RuntimeValue);
        var decrementTime = weapon.CountdownTime - deltaTime;

        weapon.countdown.Decrement(decrementTime);
        weapon.speedAtk.IncrementOverSize(model.spdAtkBuff.RuntimeValue);
        weapon.atk.IncrementOverSize(model.damageBuff.RuntimeValue);
        weapon.bulletSizeExtra.IncrementOverSize(model.bulletSizeBuff.RuntimeValue);
    }

    public void ReleaseSpell()
    {
        view.SetActive(false);
        
        GameModel.Instance.GetModel<PlayerInfo>().weapon.atk.Restore();
        GameModel.Instance.GetModel<PlayerInfo>().weapon.speedAtk.Restore();
        GameModel.Instance.GetModel<PlayerInfo>().weapon.bulletSizeExtra.Restore();
    }
}
