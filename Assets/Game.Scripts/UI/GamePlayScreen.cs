using Cinemachine;
using QtNameSpace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayScreen : QtScreen
{
    private static readonly int Shake = Animator.StringToHash("shake");
    private static readonly int Hurt = Animator.StringToHash("hurt");

    [SerializeField] private QtEventListener listener;
    [SerializeField] private Animator hurtPanel;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TMP_Text mpText;
    [SerializeField] private Slider mpSlider;
    [SerializeField] private ExtraButton attackButton;
    [SerializeField] private Image attackCountdownImageFill;
    [SerializeField] private ExtraButton skillButton;
    [SerializeField] private Image skillCountdownImageFill;
    [SerializeField] private TMP_Text score;
    [SerializeField] private string scoreFormat = "Score: {0}";
    [SerializeField] private TMP_Text enemyCount;
    [SerializeField] private string enemyCountFormat = "Enemy: {0}";


    private void Awake()
    {
        var model =  GameModel.Instance.GetModel<PlayerInfo>();
        model.stat.health.ValueChange().AddListener(OnPlayerHPChanged);
        model.stat.mana.ValueChange().AddListener(OnPlayerMPChanged);
        model.weapon.countdown.ValueChange().AddListener(AttackCountdown);
        model.skill.countdown.ValueChange().AddListener(SkillCountdown);

        var scoreModel = GameModel.Instance.GetModel<ScoreCounter>();
        scoreModel.score.ValueChange().AddListener(OnScoreChanged);
        
        hpText.SetText($"{model.stat.health.RuntimeValue}/{model.stat.health.Value}");
        hpSlider.minValue = default;
        hpSlider.maxValue = model.stat.health.Value;
        hpSlider.value = model.stat.health.RuntimeValue;
        mpText.SetText($"{model.stat.mana.RuntimeValue}/{model.stat.mana.Value}");
        mpSlider.minValue = default;
        mpSlider.maxValue = model.stat.mana.Value;
        mpSlider.value = model.stat.mana.RuntimeValue;
        attackButton.OnPress.AddListener(CallPlayerAttack);
        attackButton.OnExit.AddListener(CallEndPlayerAttack);
        skillButton.OnPress.AddListener(CallPlayerSkill);
        
        listener.StartListening(GameEventID.PlayerHurt, OnPlayerHurt);
    }

    private void Update()
    {
        if (EnemySpawner.Instance != null && EnemySpawner.Instance.IsDestroyed() is false)
            enemyCount.SetText(string.Format(enemyCountFormat, EnemySpawner.Instance.PoolCount));
    }

    private void OnDestroy()
    {
        CallEndPlayerAttack();
    }

    private void OnPlayerHurt()
    {
        var cam = FindObjectOfType<CinemachineVirtualCamera>();
        if(cam != null && cam.GetComponent<Animator>() is {} camAnim)
            camAnim.SetTrigger(Shake);
        hurtPanel.SetTrigger(Hurt);
    }

    private void OnScoreChanged(int value)
    {
        score.SetText(string.Format(scoreFormat, value));
    }

    private void CallEndPlayerAttack()
    {
        ObserverEvent.EmitEvent(GameEventID.EndAttackButton);
    }

    private void CallPlayerAttack()
    {
        ObserverEvent.EmitEvent(GameEventID.StartAttackButton);
    }

    private void CallPlayerSkill()
    {
        ObserverEvent.EmitEvent(GameEventID.SkillButton);
    }

    private void OnPlayerHPChanged(int value)
    {
        var maxHealth = GameModel.Instance.GetModel<PlayerInfo>().stat.health.Value;
        hpSlider.maxValue = maxHealth;
        hpSlider.value = value;
        hpText.SetText($"{value}/{maxHealth}");
    }
    
    private void OnPlayerMPChanged(int value)
    {
        var maxMana = GameModel.Instance.GetModel<PlayerInfo>().stat.mana.Value;
        mpSlider.maxValue = maxMana;
        mpSlider.value = value;
        mpText.SetText($"{value}/{maxMana}");

        skillButton.GetComponent<Button>().interactable =
            value >= GameModel.Instance.GetModel<PlayerInfo>().skill.manaCost.RuntimeValue;
    }

    private void AttackCountdown(float value)
    {
        var model = GameModel.Instance.GetModel<PlayerInfo>().weapon;
        attackCountdownImageFill.fillAmount = value / model.CountdownTime;
    }
    
    private void SkillCountdown(float value)
    {
        var model = GameModel.Instance.GetModel<PlayerInfo>().skill;
        skillCountdownImageFill.fillAmount = value / model.CountdownTime;
    }
}
