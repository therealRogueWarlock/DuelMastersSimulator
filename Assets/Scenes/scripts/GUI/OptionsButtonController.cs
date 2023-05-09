using UnityEngine;

namespace Scenes.scripts.GUI
{
    public class OptionsButtonController : MonoBehaviour
    {
    
        private CameraHandler _cameraHandler;
        private Canvas _gameHud;
        private Canvas _optionsMenu;

        private void Awake()
        {
            _cameraHandler = GameObject.Find("CameraHandler").GetComponent<CameraHandler>();
            _gameHud = GameObject.Find("GameHud").GetComponent<Canvas>();
            _optionsMenu = GameObject.Find("OptionsMenu").GetComponent<Canvas>();
        }

        // Start is calledgsasdfasdf
        void Start()
        {
        
            _optionsMenu.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        public void OpenOptions()
        {
            _gameHud.gameObject.SetActive(false);
            _cameraHandler.OptionsView();
            _optionsMenu.gameObject.SetActive(true);
        }
    
    
    
    }
}
