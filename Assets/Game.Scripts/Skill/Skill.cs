using Cysharp.Threading.Tasks;

namespace QtNameSpace
{
    public class Skill : ABasePresenter
    {
        public override void InitModel()
        {
        }

        public void EmitSpell()
        {
            var elementSpell =  elements.Find(e => e is ISpell).GetComponent<ISpell>();
            elementSpell?.EmitSpell();
        }

        public void ReleaseSpell()
        {
            var elementSpell =  elements.Find(e => e is ISpell).GetComponent<ISpell>();
            elementSpell?.ReleaseSpell();
        }

        private void Awake()
        {
            Init();
            
            GameModel.Instance.GetModel<PlayerInfo>().skill.countdown.ValueChange().AddListener(DoneCountdown);

            var player = FindObjectOfType<Player>();
            if (player is null)
            {
                DoneCountdown(default);
            }
            else
            {
                var thisTrans = transform;
                var playerTrans = player.transform;
                thisTrans.position = playerTrans.position;
                thisTrans.rotation = playerTrans.rotation;
                thisTrans.SetParent(playerTrans);
            }
        }

        private void OnDestroy()
        {
            if(GameModel.Instance == null)
                return;
            
            var playerInfoModel = GameModel.Instance.GetModel<PlayerInfo>();
            playerInfoModel?.skill.countdown.ValueChange().RemoveListener(DoneCountdown);
        }

        private void DoneCountdown(float value)
        {
            if (value > 0)
                return;

            ReleaseSpell();
        }
    }
}