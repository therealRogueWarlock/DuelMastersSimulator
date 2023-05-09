using Scenes.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Scenes.scripts.Events
{
    [System.Serializable]
    public class CustomUnityEvent<T> : UnityEvent<Component, T>{}
    
    public class GameEventListener<T> : MonoBehaviour
    {
        [SerializeField]
        private GameEvent<T> gameEvent;
        [SerializeField]
        private CustomUnityEvent<T> response;
        
        private void OnEnable()
        {
            gameEvent.AddListener(this);
        }

        private void OnDisable()
        {
            gameEvent.RemoveListener(this);
        }

        public void OnGameEventInvoke(Component sender, T data)
        {
            response.Invoke(sender, data);
        }
    
    }


}
