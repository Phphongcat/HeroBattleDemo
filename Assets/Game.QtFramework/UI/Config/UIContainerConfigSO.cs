using System.Collections.Generic;
using UnityEngine;

namespace QtNameSpace
{
    [CreateAssetMenu(fileName = nameof(UIContainerConfigSO), menuName = "QtConfig/UIContainer")]
    public class UIContainerConfigSO : ScriptableObject
    {
        public GameObject containerPrefab;
        public List<UIContainerConfig> configs = new();

        [System.Serializable]
        public class UIContainerConfig
        {
            public ContainerTypeEnum typeEnum;
            public string containerName;
        }

        public string GetName(ContainerTypeEnum t)
        {
            return configs.Find(config => config.typeEnum == t).containerName;
        }
    }
}