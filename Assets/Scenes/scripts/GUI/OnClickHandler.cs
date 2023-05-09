using UnityEngine;
using UnityEngine.Events;

namespace Scenes.scripts.GUI
{
    public class OnClickHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnClick;

        private void OnMouseUpAsButton()
        {
            OnClick.Invoke();
        }
    
    }
}
