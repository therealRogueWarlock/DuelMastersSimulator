using Scenes.scripts.Rounds;
using TMPro;
using UnityEngine;

namespace Scenes.scripts.GUI
{
    public class TextHandler : MonoBehaviour
    {

        private TextMeshProUGUI _currentPhaseInfo;
    
    
        // Start is called before the first frame update
        void Start()
        {
            _currentPhaseInfo = GetComponent<TextMeshProUGUI>();
        }
    

        public void OnNextPhaseListener(Component sender, object data)
        {
            if(sender.gameObject.layer != gameObject.layer) return;
            Phase phase = (Phase) data;
            _currentPhaseInfo.text = phase.PhaseEnumName.ToString();
        }
    
    }
}
