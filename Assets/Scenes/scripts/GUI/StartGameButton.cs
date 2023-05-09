using Scenes.Events;
using UnityEngine;

namespace Scenes.scripts.GUI
{
    public class StartGameButton : MonoBehaviour
    {

        [SerializeField] private GenericGameEvent onStartGame;
       

        public void InvokeOnStartGameEvent()
        {
            onStartGame.InvokeGameEvent(this, null);
        
            gameObject.SetActive(false);
        }
    
    
    }
}
