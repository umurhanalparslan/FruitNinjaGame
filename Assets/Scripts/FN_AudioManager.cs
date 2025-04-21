namespace OUAPP
{

    using System;
    using UnityEngine;
    using UnityEngine.Audio;

    public class FN_AudioManager : MonoBehaviour
    {
        public static FN_AudioManager instance;

        [Header("Sounds")]
        public FN_Sound[] Sounds;

        private void Awake()
        {
            instance = this;

            foreach (FN_Sound s in Sounds)
            {
                s.Source = gameObject.AddComponent<AudioSource>();
                s.Source.clip = s.AudioClip;
                s.Source.volume = s.Volume;
                s.Source.pitch = s.Pitch;
                s.Source.mute = s.Mute;
                s.Source.loop = s.Loop;
                s.Source.playOnAwake = s.playOnAwake;
            }
        }

        public void Play(String Name)
        {
            FN_Sound s = Array.Find(Sounds, Sound => Sound.Name == Name);
            if (s == null) return;
            s.Source.Play();
        }

        public void Stop(String Name)
        {
            FN_Sound s = Array.Find(Sounds, Sound => Sound.Name == Name);
            if (s == null) return;
            s.Source.Stop();
        }

        public void SoundEffectsActive(string Name)
        {
            FN_Sound s = Array.Find(Sounds, Sound => Sound.Name == Name);
            if (s == null) return;
            s.Source.mute = false;
        }

        public void SoundEffectsPassive(string Name)
        {
            FN_Sound s = Array.Find(Sounds, Sound => Sound.Name == Name);
            if (s == null) return;
            s.Source.mute = true;
        }
    }
}