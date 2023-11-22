using System.Collections.Generic;
using UnityEngine;

namespace QtNameSpace
{
    public class GameModel : Singleton<GameModel>
    {
        private readonly Dictionary<string, ABaseModel> _models = new();


        public void AddOrUpdateModel<T>(T model) where T : ABaseModel
        {
            if(model is null)
                return;

            _models[typeof(T).Name] = model;
        }

        public void RemoveModel<T>()
        {
            _models.Remove(typeof(T).Name);
        }

        public bool HasModel<T>() => _models.TryGetValue(typeof(T).Name, out _);

        public T GetModel<T>() where T : ABaseModel
        {
            if (_models.TryGetValue(typeof(T).Name, out var model))
            {
                return model as T;
            }
            Debug.LogWarning($"Model: {typeof(T).Name} not found");
            return null;
        }
    }
}