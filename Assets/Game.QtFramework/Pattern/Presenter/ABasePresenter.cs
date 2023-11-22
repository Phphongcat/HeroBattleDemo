using System.Collections.Generic;
using UnityEngine;

namespace QtNameSpace
{
    public abstract class ABasePresenter : MonoBehaviour, IPresenter
    {
        [SerializeField] protected List<ABaseElement> elements;


        public abstract void InitModel();

        public void Init()
        {
            InitModel();
            elements.ForEach(item => item.Init());
            elements.ForEach(item => item.UpdateView());
        }

        public T GetModel<T>() where T : ABaseModel
        {
            return GameModel.Instance.GetModel<T>();
        }

        public void Release()
        {
            elements.ForEach(item => item.Release());
        }
        
        public void UpdateViews()
        {
            elements.ForEach(item => item.UpdateView());
        }
    }
}