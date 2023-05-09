using Scenes.scripts.CardGO;
using Scenes.scripts.player;
using Scenes.scripts.Rounds;
using UnityEngine;

namespace Scenes.scripts.zones
{
    public class HandManager : MonoBehaviour
    {

        private DraggablePositionManager _draggablePositionManager;
    
        [SerializeField] private DeckHandler _deckHandler;
    
        private Draggable[] _draggablesInHand;
    
    
        // Start is called before the first frame update
        void Start()
        {

            _draggablePositionManager = GetComponent<DraggablePositionManager>();
        }
        
        public void DrawCard()
        {
        
            var cardModelData = _deckHandler.GetNextCard();
        
            if (cardModelData == null) return;

            Draggable draggable = cardModelData.gameObject.GetComponent<Draggable>();
        
            _draggablePositionManager.AddDraggable(draggable);
        }

        public void OnShieldBreak(CardDisplay cardDisplay)
        {
        
            if(cardDisplay.gameObject.layer != gameObject.layer) return;
        
            Debug.Log("Shield break!");
            Debug.Log(cardDisplay.gameObject.layer);
            Debug.Log(gameObject.layer);
            cardDisplay.CardModelData.isShield = false;
        
            cardDisplay.CardModelData.isUsed = false;
            Draggable draggable = cardDisplay.gameObject.GetComponent<Draggable>();
        
            cardDisplay.GetComponent<HighLightController>().ToggleOutline(false);
        
            draggable.isDisableDrag = false;
        
            _draggablePositionManager.AddDraggable(draggable);
        }
    
    
        public void OnStartGameListener(Component sender, object data)
        {
        
            // draw 5 cards on start game
            for (int i = 0; i < 5; i++)
            {
                DrawCard();
            } 
        }

  

        public void OnNextPhaseListener(Component sender, object data)
        {
            if(sender.gameObject.layer != gameObject.layer) return;
        
            Phase phase = (Phase) data;
            if (phase.PhaseEnumName == PhaseNames.Draw)
            {
                DrawCard();
            }
        
        }
    
        public void OnCardPlayListener(Component sender , CardDisplay data)
        {
            if(sender.gameObject.layer != gameObject.layer) return;
        
            Draggable draggable = data.GetComponent<Draggable>();
        
            _draggablePositionManager.RemoveDraggable(draggable);
        
        }
    
    
    
    }
}
