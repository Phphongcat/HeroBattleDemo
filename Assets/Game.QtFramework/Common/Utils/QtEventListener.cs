using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QtEventListener : MonoBehaviour
{
    private Dictionary<string, UnityAction> _events = new();


    public void StartListening(string eventName, UnityAction eventInput)
    {
        StopListening(eventName);
        _events[eventName] = eventInput;
        ObserverEvent.StartListening(eventName, eventInput);
    }

    public void StopListening(string eventName)
    {
        if (_events.TryGetValue(eventName, out var callback) is false) 
            return;
        if (_events.Remove(eventName))
            ObserverEvent.StopListening(eventName, callback);
    }

    private void OnDestroy()
    {
        foreach (var keyValuePair in _events)
            ObserverEvent.StopListening(keyValuePair.Key, keyValuePair.Value);
        
        _events = new Dictionary<string, UnityAction>();
    }
}