using System;
using UnityEngine;

namespace Scenes.scripts.sound
{
    [Serializable]
    public class SoundEffect
    {

        public string name;
        
        [Range(0f,1f)]
        public float volume;
        [Range(-3f,3f)]
        public float pitch;

        public bool loop;
        public bool fadeIn;
        public float fadeInDuration;
        public bool fadeOut;
        public float fadeOutDuration;
        public AudioClip audioClip;
        
        [HideInInspector]
        public AudioSource audioSource;
        
    }
}