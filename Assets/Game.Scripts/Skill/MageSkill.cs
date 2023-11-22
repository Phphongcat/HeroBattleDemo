using System.Collections.Generic;
using System.Linq;
using DigitalRuby.LightningBolt;
using UnityEngine;

namespace QtNameSpace
{
    [RequireComponent(typeof(Skill))]
    public class MageSkill : ABaseElement, ISpell
    {
        [SerializeField] private MageSkillInfo model;
        [SerializeField] private LightningBoltScript lightning;
        [SerializeField] private QtEventListener listener;

        
        public override void OtherInit()
        {
            lightning.gameObject.SetActive(false);
            model.Restore();
            GameModel.Instance.AddOrUpdateModel(model);
        }

        public override void OtherRelease()
        {
            if (IsInvoking(nameof(InvokeLightning)))
                CancelInvoke(nameof(InvokeLightning));
            
            if(GameModel.Instance != null)
                GameModel.Instance.RemoveModel<MageSkillInfo>();
            
            Destroy(gameObject);
        }

        public override void UpdateView()
        {
        }

        public void EmitSpell()
        {
            if (IsInvoking(nameof(InvokeLightning)))
                CancelInvoke(nameof(InvokeLightning));
            
            lightning.gameObject.SetActive(true);
            listener.StartListening(GameEventID.EnemyDead, InvokeLightning);
            InvokeRepeating(nameof(InvokeLightning), default, model.second.RuntimeValue);
        }

        public void ReleaseSpell()
        {
            if (IsInvoking(nameof(InvokeLightning)))
                CancelInvoke(nameof(InvokeLightning));
            
            listener.StopListening(GameEventID.EnemyDead);
            lightning.gameObject.SetActive(false);
        }

        private void InvokeLightning()
        {
            var enemyEmitPoints = FindObjectsByType<EnemyEmitPoint>(FindObjectsSortMode.None);
            if(enemyEmitPoints is null || enemyEmitPoints.Any() is false)
                return;
        
            var nearest = GetClosestEnemy(enemyEmitPoints.ToList());
            if(nearest is null || CheckInRange(nearest.transform) is false)
                return;
            
            var enemyTarget = nearest.transform.root != null ? nearest.transform.root : nearest.transform;
            lightning.EndObject = nearest.GetEmitPoint().gameObject;
            enemyTarget.GetComponent<IHeart>().TakeDamage(model.damaged.RuntimeValue);
        }
        
        private bool CheckInRange(Transform targetPoint)
        {
            var trans = transform.root != null ? transform.root : transform;
            var distance = Vector3.Distance(targetPoint.position, trans.position);
            var distanceRangeLimit = GetModel<PlayerInfo>().weapon.distanceRange.RuntimeValue;
            return distance <= distanceRangeLimit;
        }
        
        private EnemyEmitPoint GetClosestEnemy(IEnumerable<EnemyEmitPoint> list)
        {
            EnemyEmitPoint tMin = null;
            var minDist = Mathf.Infinity;
            var currentPos = transform.position;
            foreach (var t in list)
            {
                var dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }
    }
}