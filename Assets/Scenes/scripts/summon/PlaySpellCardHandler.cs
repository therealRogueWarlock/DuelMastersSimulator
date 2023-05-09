using Scenes.Events.CardEvents;
using Scenes.scripts.CardGO;
using UnityEngine;

namespace Scenes.scripts.summon
{
    public class PlaySpellCardHandler : MonoBehaviour
    {

        [SerializeField] private CardGameEvent onCardDestroy;
    
        private CardDisplay _playedCard;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                ResolveCard();
            }
        
        }


        public void OnCardPlaySpellListener(Component sender, CardDisplay cardDisplay)
        {
            if(sender.gameObject.layer != gameObject.layer) return;

        
            Debug.Log("Card spell: " + cardDisplay.CardModelData.cardAbilities);

            _playedCard = cardDisplay;
        
        
            Draggable draggable = cardDisplay.GetComponent<Draggable>();
        
            draggable.isDisableDrag = true;
            SetPosition(draggable);
        }

        private void SetPosition(Draggable draggable)
        {
        
            draggable.targetPosition = transform.position;
            draggable.targetRotation = transform.rotation;
        
        }



        private void ResolveCard()
        {
            if(_playedCard != null) onCardDestroy.InvokeGameEvent(this,_playedCard);

            _playedCard = null;
        }
    
    
    
    
    }
}
