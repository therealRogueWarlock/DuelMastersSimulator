using Scenes.scripts.CardGO;
using Scenes.scripts.player;
using UnityEngine;

namespace Scenes.scripts.zones
{
    public class ShieldManager : MonoBehaviour
    {
        private DraggablePositionManager _draggablePositionManager;

        [SerializeField] private DeckHandler _deckHandler;

        // Start is called before the first frame update
        void Start()
        {
            _draggablePositionManager = GetComponent<DraggablePositionManager>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void DrawFiveShields()
        {
            for (int i = 0; i < 5; i++)
            {
                CardDisplay cardDisplay = _deckHandler.GetNextCard();
            
                if(cardDisplay == null) return;

                cardDisplay.CardModelData.isShield = true;
            
                Draggable draggable = cardDisplay.GetComponent<Draggable>();
                draggable.targetRotation = Quaternion.Euler(0,0,180);
                draggable.isDisableDrag = true;
                _draggablePositionManager.AddDraggable(draggable);
            }
        }


        public void OnStartGameHandler(Component sender, object data)
        {
            DrawFiveShields();
        }
    
    
    }
}
