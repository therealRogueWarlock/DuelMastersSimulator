using Scenes.Events;
using UnityEngine;

namespace Scenes.scripts.Rounds
{
    public class PhaseManager : MonoBehaviour
    {
        
        private Phase _currentPhase;

        public GenericGameEvent onNextPhase;

        // Start is called before the first frame update
        void Start()
        {
            Phase untapPhase = new Phase(PhaseNames.Untap);
            Phase drawPhase = new Phase(PhaseNames.Draw);
            Phase chargePhase = new Phase(PhaseNames.Charge);
            Phase mainPhase = new Phase(PhaseNames.Main);
            Phase attackPhase = new Phase(PhaseNames.Attack);
            Phase endPhase = new Phase(PhaseNames.End);
            Phase passTurn = new Phase(PhaseNames.PassTurn);

            untapPhase.NextPhase = drawPhase;
            drawPhase.NextPhase = chargePhase;
            chargePhase.NextPhase = mainPhase;
            mainPhase.NextPhase = attackPhase;
            attackPhase.NextPhase = endPhase;
            endPhase.NextPhase = passTurn;
            passTurn.NextPhase = untapPhase;
            
            _currentPhase = chargePhase;

            if (gameObject.layer == 7) _currentPhase = passTurn;

        }
        
        public void GoToNextPhase()
        {
            _currentPhase = _currentPhase.NextPhase;
            onNextPhase.InvokeGameEvent(this,_currentPhase);
        }
        
        public Phase GetCurrentPhase()
        {
            return _currentPhase;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
               // GoToNextPhase();
            }
        }
        
    }
}
