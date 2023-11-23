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
        private Transform _target;
        private bool _isDone = true;
        private float _timing;

        
        public override void OtherInit()
        {
            lightning.gameObject.SetActive(false);
            model.Restore();
            GameModel.Instance.AddOrUpdateModel(model);
        }

        public override void OtherRelease()
        {
            if(GameModel.Instance != null)
                GameModel.Instance.RemoveModel<MageSkillInfo>();
            
            Destroy(gameObject);
        }

        public override void UpdateView()
        {
        }

        public void EmitSpell()
        {
            lightning.gameObject.SetActive(true);
            _timing = model.second.RuntimeValue;
            _isDone = false;
        }

        public void ReleaseSpell()
        {
            _isDone = true;
            lightning.gameObject.SetActive(false);
        }

        private void Update()
        {
            if(IsRelease || _isDone)
                return;

            InvokeLightning();
        }

        private void InvokeLightning()
        {
            if (_target != null)
            {
                _timing -= Time.deltaTime;
                if (_timing > (float)default)
                    return;
                
                _target.GetComponent<IHeart>().TakeDamage(model.damaged.RuntimeValue);
            }
            _timing = model.second.RuntimeValue;
            
            var enemyEmitPoints = FindObjectsByType<EnemyEmitPoint>(FindObjectsSortMode.None);
            if (enemyEmitPoints is null || enemyEmitPoints.Any() is false)
            {
                lightning.ForceUpdate(gameObject);
                return;
            }
        
            var nearest = GetClosestEnemy(enemyEmitPoints.ToList());
            if(nearest is null || CheckInRange(nearest.transform) is false)
            {
                lightning.ForceUpdate(gameObject);
                return;
            }
            
            _target = nearest.transform.root != null ? nearest.transform.root : nearest.transform;
            lightning.ForceUpdate(nearest.GetEmitPoint().gameObject);
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