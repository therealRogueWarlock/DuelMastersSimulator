using System.Collections.Generic;
using Scenes.scripts.CardGO;
using UnityEngine;

namespace Scenes.scripts.zones
{
    public class CardSelectHandler : MonoBehaviour
    {

        private int _playerInUse;
        
        public CardDisplay selectedCard;
        public List<CardDisplay> selectedCards;

        private bool _takeInput;
        
        private void Start()
        {
            selectedCards = new();
        }

        public void TakeInput(int playerInUse,bool enable)
        {
            _playerInUse = playerInUse;
            _takeInput = enable;
        }

        void Update()
        {
            if(!_takeInput) return;
            
            if( Input.GetMouseButtonDown(0) )
            {
                Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                RaycastHit hit;
         
                if( Physics.Raycast( ray, out hit, 100 ) )
                {
                    if (!hit.transform.gameObject.CompareTag("Card")) return;
                    SelectCard(hit.transform.GetComponent<CardDisplay>());
                }
            }
        }
        
        private void SelectCard(CardDisplay cardDisplay)
        {
            
            if(cardDisplay.gameObject.layer.Equals(_playerInUse)) return;
            
            if (selectedCards.Contains(cardDisplay))
            {
                selectedCards.Remove(cardDisplay);
                cardDisplay.GetComponent<HighLightController>().ToggleOutline(false);
            }
            else
            {
                selectedCard = cardDisplay;
                cardDisplay.GetComponent<HighLightController>().ToggleOutline(true);
                selectedCards.Add(cardDisplay);
            }
            
        }
        
        
    }
}