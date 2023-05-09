using UnityEngine;

namespace Scenes.scripts.GUI
{
    public class GameHubController : MonoBehaviour
    {
    
        private CameraHandler _cameraHandler;

        private bool _deckBuilding;

        private void Awake()
        {
            _cameraHandler = GameObject.Find("CameraHandler").GetComponent<CameraHandler>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

    
        public void DeckBuilding()
        {
            _deckBuilding = !_deckBuilding;
            if(_deckBuilding) _cameraHandler.DeckBuildingView();
            else _cameraHandler.PlayerOneView();
        }
    
    }
}
