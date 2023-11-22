using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QtNameSpace
{
    [RequireComponent(typeof(Button))]
    public class ExtraButton : MonoBehaviour, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public enum ExtraButtonState
        {
            Exit = 0,
            Hold = 1
        }

        public ExtraButtonEvent OnPress = new();
        public ExtraButtonEvent OnExit = new();
        private ExtraButtonState _state = ExtraButtonState.Exit;
    
    
        public void OnPointerDown(PointerEventData eventData)
        {
            if (CheckBaseButton(eventData) && _state != ExtraButtonState.Hold)
            {
                _state = ExtraButtonState.Hold;
                OnPress?.Invoke();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(_state is ExtraButtonState.Exit)
                return;
        
            _state = ExtraButtonState.Exit;
        
            if(CheckBaseButton(eventData))
                OnExit?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_state is ExtraButtonState.Exit)
                return;
        
            _state = ExtraButtonState.Exit;
        
            if(CheckButton())
                OnExit?.Invoke();
        }

        private bool CheckBaseButton(PointerEventData eventData)
        {
            if(CheckButton() is false)
                return false;
            return eventData.button is PointerEventData.InputButton.Left;
        }

        private bool CheckButton()
        {
            var button = GetComponent<Button>();
            return button.enabled && button.interactable;
        }
    }

    [Serializable]
    public class ExtraButtonEvent : UnityEvent {}
}