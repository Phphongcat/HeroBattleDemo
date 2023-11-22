namespace QtNameSpace
{
    public interface IPresenter
    {
        public void UpdateViews();
        public T GetModel<T>() where T : ABaseModel;
    }
}