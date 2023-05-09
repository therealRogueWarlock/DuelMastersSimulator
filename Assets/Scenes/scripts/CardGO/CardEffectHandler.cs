using UnityEngine;

namespace Scenes.scripts.CardGO
{
    public class CardEffectHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] selectEffect;
        [SerializeField] private ParticleSystem[] dragEffect;
        
        private void PlayCardEffect()
        {
            Debug.Log("Drag effect");
            foreach (var effect in dragEffect)
            {
                effect.Play();
            }
        
        }

        private void StopPlayCardEffect()
        {
            foreach (var effect in dragEffect)
            {
                effect.Stop();
            }
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag("SummonTriggerCollider")) return;
            PlayCardEffect();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if(!other.gameObject.CompareTag("SummonTriggerCollider")) return;
            StopPlayCardEffect();
        }
        
        
        // Update is called once per frame
        public void SelectCardEffect()
        {
            foreach (var effect in selectEffect)
            {
                effect.Play();
            }
        }

        public void DeselectCardEffect()
        {
            foreach (var effect in selectEffect)
            {
                effect.Stop();
            }
        }

    }
}
