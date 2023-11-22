using System;

namespace QtNameSpace
{
    [Serializable]
    public abstract class ABaseModel : IModel
    {
        public abstract void Restore();
    }
}