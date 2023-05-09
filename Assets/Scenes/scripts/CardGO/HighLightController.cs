using UnityEngine;

namespace Scenes.scripts.CardGO
{
    public class HighLightController : MonoBehaviour
    {
   
        [SerializeField] private Material highLightMatrial;
        [SerializeField] private Renderer targetRenderer;
    
        private Material _originalMaterial;
    
        public void ToggleOutline(bool enable)
        {
            if (enable)
            {
                targetRenderer.material = highLightMatrial;
            }
            else
            {
                targetRenderer.material = _originalMaterial;
            }
        }

        public void RemoveOutline()
        {
            targetRenderer.material = _originalMaterial;
        }
    }
}