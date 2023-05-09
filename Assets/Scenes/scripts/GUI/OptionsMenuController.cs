using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes.scripts.GUI
{
    public class OptionsMenuController : MonoBehaviour
    {
    

        [SerializeField]
        private Slider musicVolumeSlider;
        [SerializeField]
        private Slider effectVolumeSlider;
    
        private CameraHandler _cameraHandler;
        private Canvas _gameHud;
        private Canvas _optionsMenu;

        private void Awake()
        {
            _cameraHandler = GameObject.Find("CameraHandler").GetComponent<CameraHandler>();
            _gameHud = GameObject.Find("GameHud").GetComponent<Canvas>();
            _optionsMenu = GameObject.Find("OptionsMenu").GetComponent<Canvas>();
        }
        private void Start()
        {
            if (!PlayerPrefs.HasKey("gameMusicVolume")) PlayerPrefs.SetFloat("gameMusicVolume", 0.5f);
            if (!PlayerPrefs.HasKey("gameEffectVolume")) PlayerPrefs.SetFloat("gameEffectVolume", 0.5f);
        
            Load();
        }

        public void SetVolume(float volume)
        {
            //audioMixer.SetFloat("volume", volume);
            //audioSource.volume = volume;
        
            AudioListener.volume = musicVolumeSlider.value;
        
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("gameMusicVolume", musicVolumeSlider.value);
            PlayerPrefs.SetFloat("gameEffectVolume", effectVolumeSlider.value);
        }

        private void Load()
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("gameMusicVolume");
            effectVolumeSlider.value = PlayerPrefs.GetFloat("gameEffectVolume");
        }


        public void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }



        public void CloseMenu()
        {
            _gameHud.gameObject.SetActive(true);
            _cameraHandler.GoToPrevView();
            _optionsMenu.gameObject.SetActive(false);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("DuelMastersSimulator");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    
    }
}
