using Scenes.scripts.CardGO;
using Scenes.scripts.Rounds;
using Scenes.scripts.summon;
using UnityEngine;

namespace Scenes.scripts.zones
{
    public class ManaZoneHandler : MonoBehaviour
    {
    
        private DraggablePositionManager _draggablePositionManager;
    
        [SerializeField] private PhaseManager phaseManager;

        [SerializeField] private ManaHandler manaHandler;
        
        // Start is called before the first frame update
        void Start()
        {
            _draggablePositionManager = GetComponent<DraggablePositionManager>();
        }

        public void OnCardPlayManaZoneListener(Component sender , CardDisplay data)
        {
            if(sender.gameObject.layer != gameObject.layer) return;
            
            Debug.Log("HandleOnPlayCardAsMana");
        
            phaseManager.GoToNextPhase();
        
            Draggable draggable = data.GetComponent<Draggable>();
        
            draggable.targetRotation = Quaternion.Euler(0,180,0);
            draggable.isDisableDrag = true;
            _draggablePositionManager.AddDraggable(draggable);
        
            Tabbable tabbable = draggable.gameObject.GetComponent<Tabbable>();
            tabbable.enabled = true;
        
            tabbable.onTapCard.AddListener(manaHandler.HandleCardTap);
            
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
    
    }
}
