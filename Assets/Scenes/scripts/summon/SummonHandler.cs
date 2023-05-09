using Scenes.Events.CardEvents;
using Scenes.scripts.CardGO;
using Scenes.scripts.Rounds;
using UnityEngine;

namespace Scenes.scripts.summon
{
    public class SummonHandler : MonoBehaviour
    {
        [SerializeField] private CardGameEvent onCardPlaySpell;
        [SerializeField] private CardGameEvent onCardPlayBattleZone;
        [SerializeField] private CardGameEvent onCardPlayManaZone;
        [SerializeField] private CardGameEvent onCardPlay;
        [SerializeField] private CardGameEvent onEnterCardSummonTrigger;
        [SerializeField] private CardGameEvent onLeaveCardSummonTrigger;
    
        [SerializeField] private ManaHandler _manaHandler;
        private Collider CardCollider { get; set; }

        [SerializeField] private PhaseManager _phaseManager;
    
        // Start is called before the first frame update
        void Start()
        {
            CardCollider = null;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Card"))
            {
                CardCollider = other;
                onEnterCardSummonTrigger.InvokeGameEvent( _manaHandler,other.GetComponent<CardDisplay>());
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Card"))
            {
                onLeaveCardSummonTrigger.InvokeGameEvent( _manaHandler,other.GetComponent<CardDisplay>());
                CardCollider = null;
            }
        }
    
        public void CardDropListener(Component sender, CardDisplay cardDisplay)
        {
        
            if(sender.gameObject.layer != _manaHandler.gameObject.layer) return;

            if (CardCollider is null) return;
      
            if (!CardCollider.tag.Equals("Card")) return;
        
            if (_phaseManager.GetCurrentPhase().PhaseEnumName == PhaseNames.Charge) PlayCardInChargePhase(cardDisplay);
        
            if (_phaseManager.GetCurrentPhase().PhaseEnumName == PhaseNames.Main) PlayCardInMainPhase(cardDisplay);
        }

        private void PlayCardInChargePhase(CardDisplay card)
        {
            onCardPlay.InvokeGameEvent(_manaHandler, card);
            onCardPlayManaZone.InvokeGameEvent(_manaHandler, card);
        }

        private void PlayCardInMainPhase(CardDisplay cardDisplay)
        {
        
            CardModel cardModel = cardDisplay.CardModelData;
            if (!_manaHandler.CanPlay(cardModel)) return;
        
            Debug.Log("played " + cardDisplay.CardModelData.name + "mana cost: " + cardDisplay.CardModelData.manaCost);
            onCardPlay.InvokeGameEvent(_manaHandler,cardDisplay);
            if (cardModel.type.Equals("Creature")) onCardPlayBattleZone.InvokeGameEvent(_manaHandler,cardDisplay);
            if (cardModel.type.Equals("Spell")) onCardPlaySpell.InvokeGameEvent(_manaHandler,cardDisplay);
        }
    }
}
