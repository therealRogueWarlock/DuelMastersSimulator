using UnityEngine;
using UnityEngine.Events;

namespace Scenes.scripts.CardGO
{
    public class Tabbable : MonoBehaviour
    {
        private Draggable draggable;
        private CardDisplay cardDisplay;
        private CardModel cardModel;

        public UnityEvent<Tabbable> onTapCard; 
    
        public bool isTapped;
        // Start is called before the first frame update
        void Start()
        {
            isTapped = false;
            draggable = GetComponent<Draggable>();
            cardDisplay = GetComponent<CardDisplay>();
            cardModel = cardDisplay.CardModelData;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnMouseUpAsButton()
        {
            if (!enabled) return;
        
            Debug.Log(cardModel.name);
        
            if (cardModel.isUsed) return;
        
            isTapped = !isTapped;
            onTapCard.Invoke(this);
            draggable.targetRotation *= Quaternion.Euler(0, isTapped?90:-90, 0);
        
        }


        public void UnTap()
        {

            cardModel.isUsed = false;
            if(!isTapped)return;
            isTapped = false;
            draggable.targetRotation *= Quaternion.Euler(0, -90, 0);
        }
    
    }
}
