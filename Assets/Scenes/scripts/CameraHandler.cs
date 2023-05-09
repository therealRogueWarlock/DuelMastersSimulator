using UnityEngine;

namespace Scenes.scripts
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private Transform _optionsCameraPos;
        [SerializeField] private Transform _deckbuilderCameraPos;
        [SerializeField] private Transform _playCameraPos;
        [SerializeField] private Transform _play2CameraPos;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _currentPosition;
        private Transform _prevPosition;

        // Start is called before the first frame update
        void Start()
        {
            _prevPosition = _currentPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                SwitchCameraDeckBuilding();
            }

            if (Input.GetKeyUp(KeyCode.W)) SwitchPlayerCamera();
        
            _mainCamera.transform.rotation = Quaternion.Lerp(_mainCamera.transform.rotation, _currentPosition.rotation, Time.deltaTime * 5.0f);
            _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, _currentPosition.position, Time.deltaTime * 5.0f);
        
        }

        public void SwitchCameraDeckBuilding()
        {
            if (_currentPosition.Equals(_playCameraPos))
            {
                SetCurrent(_deckbuilderCameraPos);
                return;
            }
            SetCurrent(_playCameraPos);
        }
    
        public void SwitchPlayerCamera()
        {
            if (_currentPosition.Equals(_playCameraPos))
            {
                SetCurrent(_play2CameraPos);
                return;
            }
            SetCurrent(_playCameraPos);
        }
    
        public void GoToPrevView()
        {
            SetCurrent(_prevPosition);
        }

        public void OptionsView()
        {
            SetCurrent(_optionsCameraPos);
        }

        public void DeckBuildingView()
        {
            SetCurrent(_deckbuilderCameraPos);
        }

        public void PlayerOneView()
        {
            SetCurrent(_playCameraPos);
        }

        private void SetCurrent(Transform newCurrent)
        {
            _prevPosition = _currentPosition;
            _currentPosition = newCurrent;

        }

    
    }
}
