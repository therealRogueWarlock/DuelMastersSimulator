namespace Scenes.scripts.Rounds
{
    public class Phase
    {

        public PhaseNames PhaseEnumName;
        
        public Phase NextPhase { get; set; }

        public Phase(PhaseNames phaseEnumName)
        {
            PhaseEnumName = phaseEnumName;

        }
        
        
    }
}