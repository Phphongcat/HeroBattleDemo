using System.Collections.Generic;
using System.Linq;
using QtNameSpace;
using UnityEngine;

public class PlayerTargetLooker : ABaseElement
{
    public override void OtherInit()
    {
    }

    public override void OtherRelease()
    {
    }

    public override void UpdateView()
    {
    }

    private void Update()
    {
        if(IsRelease)
            return;
        
        var enemyEmitPoints = FindObjectsByType<EnemyEmitPoint>(FindObjectsSortMode.None);
        if(enemyEmitPoints is null || enemyEmitPoints.Any() is false)
            return;
        
        var nearest = GetClosestEnemy(enemyEmitPoints.ToList());
        if(nearest is null || CheckInRange(nearest.transform) is false)
            return;

        GetModel<PlayerInfo>().target = nearest.GetEmitPoint();
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