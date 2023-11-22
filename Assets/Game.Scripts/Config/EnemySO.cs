using UnityEngine;

[CreateAssetMenu(fileName = nameof(EnemySO), menuName = "ConfigSO/Enemy")]
public class EnemySO : ScriptableObject
{
    public EnemyInfo info;
}