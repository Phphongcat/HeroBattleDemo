using UnityEngine.Events;

namespace QtNameSpace
{
    public interface IContainer
    {
        public ContainerTypeEnum GetContainerType();
        public ContainerStatusEnum GetContainerStatus();
        public void Open(bool useAmin = true, UnityAction openAction = null);
        public void Close(bool useAmin = true, UnityAction openAction = null);
        public void SetEnable(bool enable);
    }
}