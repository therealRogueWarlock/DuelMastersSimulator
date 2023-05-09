using TMPro;
using UnityEngine;

namespace Scenes.scripts.GUI
{
    public class GameInfoManager : MonoBehaviour
    {
    
        private static TextMeshProUGUI _infoText;

        // Start is called before the first frame update
        void Start()
        {
            _infoText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public static void DisplayInfoText(string infoString)
        {
            _infoText.text = infoString;
        }
    
    
    }
}
