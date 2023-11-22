using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace QtNameSpace
{
    public sealed class QtUIContainer : Singleton<QtUIContainer>
    {
        [SerializeField] private UIContainerConfigSO config;
        [SerializeField] private Transform wallpaperContainer;
        [SerializeField] private Transform screenContainer;
        [SerializeField] private Transform boxContainer;
        [SerializeField] private Transform popupContainer;

        private readonly Dictionary<string, IContainer> _containers = new();
        
        
        public void CloseAllContainerExcept(string nameContainer)
        {
            foreach (var keyValuePair in _containers)
            {
                if(keyValuePair.Key.Equals(nameContainer))
                    continue;
            
                keyValuePair.Value.Close();
            }
        }

        public void CloseAllContainer()
        {
            foreach (var keyValuePair in _containers)
                keyValuePair.Value.Close();
        }

        public async UniTask<IContainer> LoadContainer(string nameContainer, bool useAddressable = false, bool isOpen = true)
        {
            if (_containers.TryGetValue(nameContainer, out var container))
                return container;

            if (useAddressable)
            {
                // do something load asset with addressable
            }
            else
            {
                if (await Resources.LoadAsync(nameContainer) is not GameObject prefab)
                    throw new NullReferenceException(nameof(GameObject));
                if(prefab.GetComponent<IContainer>() is not { } prefabContainer)
                    throw new NullReferenceException(nameof(IContainer));
                
                var containerType = prefabContainer.GetContainerType();
                _containers[nameContainer] = Instantiate(prefab, GetParent(containerType)).GetComponent<IContainer>();
            }
            
            if(isOpen) _containers[nameContainer].Open();
            else _containers[nameContainer].Close(false);
            return _containers[nameContainer];
        }

        private void Start()
        {
            wallpaperContainer = Instantiate(config.containerPrefab, transform).transform;
            wallpaperContainer.name = config.GetName(ContainerTypeEnum.Wallpaper);
        
            screenContainer = Instantiate(config.containerPrefab, transform).transform;
            screenContainer.name = config.GetName(ContainerTypeEnum.Screen);
        
            boxContainer = Instantiate(config.containerPrefab, transform).transform;
            boxContainer.name = config.GetName(ContainerTypeEnum.Box);
        
            popupContainer = Instantiate(config.containerPrefab, transform).transform;
            popupContainer.name = config.GetName(ContainerTypeEnum.Popup);
        }

        private Transform GetParent(ContainerTypeEnum typeEnum)
        {
            return typeEnum switch
            {
                ContainerTypeEnum.Wallpaper => wallpaperContainer,
                ContainerTypeEnum.Screen => screenContainer,
                ContainerTypeEnum.Box => boxContainer,
                ContainerTypeEnum.Popup => popupContainer,
                _ => throw new ArgumentOutOfRangeException(nameof(typeEnum), typeEnum, null)
            };
        }
    }
}
