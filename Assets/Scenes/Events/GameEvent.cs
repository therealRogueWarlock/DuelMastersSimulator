using System.Collections.Generic;
using Scenes.scripts.CardGO;
using Scenes.scripts.Events;
using UnityEngine;

namespace Scenes.Events
{
    public class GameEvent<T> : ScriptableObject
    {
        public List<GameEventListener<T>> listeners = new();
        
        public void InvokeGameEvent(Component sender, T data)
        {
            foreach (var eventListener in listeners)
            {
                eventListener.OnGameEventInvoke(sender, data);
            }
        }

        public void AddListener(GameEventListener<T> listener)
        {
            if (!listeners.Contains(listener)) listeners.Add(listener);
       
        }

        public void RemoveListener(GameEventListener<T> listener)
        {
            if (listeners.Contains(listener)) listeners.Remove(listener);
        }
   
    }
    
}
