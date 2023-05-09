using System;
using System.Collections.Generic;
using System.Linq;
using Scenes.Events;
using Scenes.scripts.CardGO;
using Scenes.scripts.Database;
using UnityEngine;

namespace Scenes.scripts.player
{
    public class DeckHandler : MonoBehaviour
    {
   
  
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private GameObject cardDatabase;
        [SerializeField] private GenericGameEvent OnLoseGame;
        private System.Random _random;
        private CardDatabaseManager _cardDatabaseManager;
        private List<CardModel> _cardDeck;
        private List<CardModel> _shuffledCardDeck;
        void Start()
        {
            Debug.Log("DeckHandler start");
            _random = new System.Random();
            _cardDatabaseManager = CardDatabaseManager.GetInstance();
            _cardDeck = _cardDatabaseManager.GetTestDeck();
        
            _shuffledCardDeck = _cardDeck.OrderBy(card => _random.Next()).ToList();

        }
    
        private CardModel GetNextCardModel()
        {
            CardModel cardModelData = null;
        
            try
            {
                cardModelData = _shuffledCardDeck[0];
                _shuffledCardDeck.RemoveAt(0);
            }
            catch 
            {
                Console.WriteLine("Deck out of cards!");
            }
        
            return cardModelData;
        
        }
    
        public CardDisplay GetNextCard()
        {
            var cardModelData = GetNextCardModel();

            if (cardModelData == null)
            {
                OnLoseGame.InvokeGameEvent(this, null);
                return null;
            }
        
            GameObject cardObject = Instantiate(cardPrefab,
                transform.position, transform.rotation);
        
            cardObject.layer = gameObject.layer;
        
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            cardDisplay.CardModelData = cardModelData;
        
            return cardDisplay;
        }
    
    
        private void ShuffleDeck()
        {
            _shuffledCardDeck = _shuffledCardDeck.OrderBy(card => _random.Next()).ToList();
        }
    
    }
}
