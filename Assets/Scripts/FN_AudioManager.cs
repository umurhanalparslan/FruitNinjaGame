namespace OUAPP
{
    using System;
    using UnityEngine;
    using UnityEngine.Audio;

    // FN_AudioManager sinifi, oyun icerisindeki sesleri yonetir
    public class FN_AudioManager : MonoBehaviour
    {
        public static FN_AudioManager instance;

        [Header("Sounds")]
        public FN_Sound[] Sounds; // Oyun icinde calinacak seslerin listesi

        private void Awake()
        {
            instance = this;

            // Tum sesleri AudioSource olarak ayarla
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

        // Belirli isimdeki sesi cal
        public void Play(string Name)
        {
            FN_Sound s = Array.Find(Sounds, Sound => Sound.Name == Name);
            if (s == null) return;
            s.Source.Play();
        }

        // Belirli isimdeki sesi durdur
        public void Stop(string Name)
        {
            FN_Sound s = Array.Find(Sounds, Sound => Sound.Name == Name);
            if (s == null) return;
            s.Source.Stop();
        }

        // Efekt sesini aktif et
        public void SoundEffectsActive(string Name)
        {
            FN_Sound s = Array.Find(Sounds, Sound => Sound.Name == Name);
            if (s == null) return;
            s.Source.mute = false;
        }

        // Efekt sesini pasif yap
        public void SoundEffectsPassive(string Name)
        {
            FN_Sound s = Array.Find(Sounds, Sound => Sound.Name == Name);
            if (s == null) return;
            s.Source.mute = true;
        }
    }
}
