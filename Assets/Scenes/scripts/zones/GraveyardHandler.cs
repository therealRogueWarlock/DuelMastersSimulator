using Scenes.scripts.CardGO;
using UnityEngine;

namespace Scenes.scripts.zones
{
    public class GraveyardHandler : MonoBehaviour
    {

        private DraggablePositionManager _draggablePositionManager;
    
    
        // Start is called before the first frame update
        void Start()
        {

            _draggablePositionManager = GetComponent<DraggablePositionManager>();

        }

        // Update is called once per frame
        void Update()
        {
        
        }


        public void OnCardDestroyListener(Component sender, CardDisplay cardDisplay)
        {
        
            if(sender.gameObject.layer != gameObject.layer) return;

        
            Debug.Log("Gaveyard on destoy card " + cardDisplay.CardModelData.name);
            cardDisplay.CardModelData.isUsed = true;
            cardDisplay.GetComponent<HighLightController>().RemoveOutline();
        
        
            _draggablePositionManager.AddDraggable(cardDisplay.GetComponent<Draggable>());
        
        }
    
    }
}
