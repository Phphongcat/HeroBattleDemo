using UnityEngine;

namespace QtNameSpace
{
    public class EnemyScore : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        private int _score;
        

        private void Start()
        {
            _score = enemy.GetModel().score;
        }

        private void OnDestroy()
        {
            ObserverEvent.EmitEventData(GameEventID.EnemyDead, _score);
        }
    }
}