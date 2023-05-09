using Scenes.Events.CardEvents;
using Scenes.scripts.CardGO;
using Scenes.scripts.GUI;
using Scenes.scripts.Rounds;
using UnityEngine;
using UnityEngine.Events;

namespace Scenes.scripts.zones
{
    public class BattleZoneHandler : MonoBehaviour
    {
        [SerializeField] private DraggablePositionManager _draggablePositionManager;
        [SerializeField] private CardGameEvent onCardDestroy;
    
        [SerializeField] private UnityEvent<CardDisplay> onShieldBreak;
    
        [SerializeField] private CardSelectHandler cardSelectHandler;
    
        private PhaseManager _phaseManager;

        private CardDisplay _attackingCard;

        // Start is called before the first frame update
        void Start()
        {
        
            _phaseManager = gameObject.transform.parent.GetComponentInChildren<PhaseManager>();
        
        }
    
        void Update()
        {
        
            if(!_phaseManager.GetCurrentPhase().PhaseEnumName.Equals(PhaseNames.Attack)) return;
        
            if (Input.GetKeyUp(KeyCode.A))
            {

                if (!_attackingCard)
                {
                    GameInfoManager.DisplayInfoText("No attacker selected");
                    return;
                }
            
            
                var targetCard = cardSelectHandler.selectedCard;
                if (!targetCard)
                {
                
                    GameInfoManager.DisplayInfoText("No target selected");
                    return;
                }

                if (targetCard.CardModelData.isShield)
                {
                    onShieldBreak.Invoke(targetCard);
                }
                else if (_attackingCard.CardModelData.power > targetCard.CardModelData.power)
                {
                    Debug.Log(_attackingCard.CardModelData.name + " won the fight!!!");
                    Debug.Log(targetCard.CardModelData.name + " lose!");
                    _attackingCard.CardModelData.isUsed = true;
                    CardDestroyInBattle(targetCard);
                
                }else if (_attackingCard.CardModelData.power < targetCard.CardModelData.power)
                {
                    Debug.Log(_attackingCard.CardModelData.name + " lose!");
                    Debug.Log(targetCard.CardModelData.name + "won the fight!");
                    CardDestroyInBattle(_attackingCard);

                }else if (_attackingCard.CardModelData.power == cardSelectHandler.selectedCard.CardModelData.power)
                {
                    Debug.Log(_attackingCard.CardModelData.name + " die!");
                    Debug.Log(targetCard.CardModelData.name + " die!");
                    CardDestroyInBattle(_attackingCard);
                    CardDestroyInBattle(targetCard);
                }
                cardSelectHandler.TakeInput(gameObject.layer,false);
                _attackingCard.GetComponent<HighLightController>().RemoveOutline();
                targetCard.GetComponent<HighLightController>().RemoveOutline();
            }
        }
    
    
        private void HandleCardTap(Tabbable tabbable)
        {
            if(!_phaseManager.GetCurrentPhase().PhaseEnumName.Equals(PhaseNames.Attack)) return;
            Debug.Log("Battle zone on tap handler");
            if (tabbable.isTapped)
            {
                cardSelectHandler.TakeInput(gameObject.layer,true);
                _attackingCard = tabbable.GetComponent<CardDisplay>();
                return;
            }
        
            cardSelectHandler.TakeInput(gameObject.layer,false);
            _attackingCard = null;
        }
    
    
        public void OnCardPlayBattleZoneListener(Component sender , CardDisplay data)
        {
        
            if(sender.gameObject.layer != gameObject.layer) return;

        
            Debug.Log("OnPlayCardBattleZoneListener");
        
            Draggable draggable = data.GetComponent<Draggable>();
        
            draggable.isDisableDrag = true;
            Tabbable tabbable = draggable.gameObject.GetComponent<Tabbable>();
            tabbable.enabled = true;
        
            tabbable.onTapCard.AddListener(HandleCardTap);
        
            _draggablePositionManager.AddDraggable(draggable);
        }


        private void CardDestroyInBattle(CardDisplay cardDisplay)
        {
            
            cardDisplay.CardModelData.isUsed = true;
            onCardDestroy.InvokeGameEvent(cardDisplay,cardDisplay); 
            
        }
    
    
        public void OnNewPhaseListener(Component sender, object data)
        {
            if(sender.gameObject.layer != gameObject.layer) return;
        
            Phase phase = (Phase) data;
            if(!phase.PhaseEnumName.Equals(PhaseNames.Untap))return;

            foreach (var draggable in _draggablePositionManager.GetDraggables())
            {
                Tabbable tabbable = draggable.GetComponent<Tabbable>();

                tabbable.UnTap();
            }
        }
        
        public void OnCardDestroyListener(Component sender, CardDisplay cardDisplay)
        {
        
            if(sender.gameObject.layer != gameObject.layer) return;
            
            _draggablePositionManager.RemoveDraggable(cardDisplay.GetComponent<Draggable>());
            
        }
    
    }
}
