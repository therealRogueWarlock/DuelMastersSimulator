using System;
using Scenes.Events;
using UnityEngine;

namespace Scenes.scripts.CardGO
{
    public class CardDisplay : MonoBehaviour
    {
        public CardModel CardModelData { get;set; }
        
        [SerializeField] private GameObject cardFront;
        private Renderer _cardFrontRenderer;
        private CardEffectHandler _cardEffectHandler;
        
        // Start is called before the first frame update
        void Start()
        {
            _cardFrontRenderer = cardFront.GetComponent<Renderer>();
            _cardEffectHandler = GetComponent<CardEffectHandler>();
            name = CardModelData.name;
        }
        
        private void LateUpdate()
        {
            _cardFrontRenderer.material.SetTexture("_MainTex", CardModelData.cardImage);
        }
        
    }
}
