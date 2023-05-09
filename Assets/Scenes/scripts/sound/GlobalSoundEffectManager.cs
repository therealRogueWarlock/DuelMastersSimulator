using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.scripts.sound
{
    public class GlobalSoundEffectManager : MonoBehaviour
    {

        [SerializeField]
        public SoundEffect[] soundEffects;
    
        //
        private Dictionary<string, SoundEffect> soundEffectDict;

        // Start is called before the first frame update
        private void Awake()
        {
            soundEffectDict = new();

            foreach (var soundEffect in soundEffects)
            {
                soundEffect.audioSource = gameObject.AddComponent<AudioSource>();
            
                soundEffect.audioSource.clip = soundEffect.audioClip;
                soundEffect.audioSource.pitch = soundEffect.pitch;
                soundEffect.audioSource.volume = soundEffect.volume;
                soundEffect.audioSource.loop = soundEffect.loop;

                soundEffectDict.Add(soundEffect.name,soundEffect);
            
            }
        
        }

        public void OnPlayCardListener(Component component, object data)
        {
            PlaySoundEffect(soundEffectDict["PlayCardEffect"]);
            PlaySoundEffect(soundEffectDict["OnPlayCard"]);
        }
    
        public void OnCardEnterSummonTriggerListener(Component sender, object data)
        {
            PlaySoundEffect(soundEffectDict["DragEffect"]);
        }
    
        public void OnCardLeaveSummonTriggerListener(Component sender, object data)
        {
            StopSoundEffect(soundEffectDict["DragEffect"]);
        }
    
    
        public void PlaySoundEffect(SoundEffect soundEffect)
        {
            if (soundEffect.audioSource.isPlaying) return;
            if (soundEffect.fadeIn)
            {
                soundEffect.audioSource.volume = 0;
                StartCoroutine(StartFade(soundEffect.audioSource, soundEffect.fadeInDuration, soundEffect.volume));
            }
            soundEffect.audioSource.Play();
        }
        
        public void StopSoundEffect(SoundEffect soundEffect)
        {
            if (!soundEffect.audioSource.isPlaying) return;
        
            if (soundEffect.fadeOut)
            {
                StartCoroutine(StartFade(soundEffect.audioSource, soundEffect.fadeOutDuration, 0f));
            }
            else
            {
                soundEffect.audioSource.Stop();
            }

        }
    
        
        private IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            if (targetVolume==0) audioSource.Stop();
                
        }
    
    }
}
