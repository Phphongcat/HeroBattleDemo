namespace QtNameSpace
{
    public interface IElement
    {
        public void Init();
        public void Release();
        public void UpdateView();
        public T GetModel<T>() where T : ABaseModel;
    }
}