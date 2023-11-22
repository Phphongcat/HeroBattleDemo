using Cysharp.Threading.Tasks;
using QtNameSpace;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.Common
{
    public class ThirdScene : MonoBehaviour
    {
        [SerializeField] private Installer installer;
        [SerializeField] private QtEventListener listener;

        private async void Awake()
        {
            Application.targetFrameRate = 60;

            await installer.Install();
            
            listener.StartListening(GameEventID.PlayerDead, OnPlayerDead);
            listener.StartListening(GameEventID.EnemyDead, OnEnemyDead);
            
            InitModel();
            await QtUIContainer.Instance.LoadContainer(UIName.GamePlayScreen);
            
            EnemySpawner.Instance.StartSpawning();
        }

        private void InitModel()
        {
            var model = new ScoreCounter
            {
                score = new VariableInt()
            };
            model.Restore();
            GameModel.Instance.AddOrUpdateModel(model);
        }

        private async void OnPlayerDead()
        {
            EnemySpawner.Instance.Release();
            await QtUIContainer.Instance.LoadContainer(UIName.EndGamePopup);
        }

        private void OnEnemyDead()
        {
            var data = ObserverEvent.GetData(GameEventID.EnemyDead);
            var score = data is not int value ? default : value;
            
            if (GameModel.Instance != null && GameModel.Instance.IsDestroyed() is false)
                GameModel.Instance.GetModel<ScoreCounter>().score.IncrementOverSize(score);
        }
    }
}