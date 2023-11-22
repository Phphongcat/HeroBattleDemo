using System;
using QtNameSpace;
using UnityEngine;

[Serializable]
public class EnemyInfo : ABaseModel
{
    public EnemyClassEnum classEnum;
    public EnemyID id;
    public GameObject prefab;


    public override void Restore()
    {
        
    }
}