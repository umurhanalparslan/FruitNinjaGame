namespace OUAPP
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // FN_Sound sinifi, ses verilerini tanimlamak icin kullanilir
    [System.Serializable]
    public class FN_Sound
    {
        [HideInInspector]
        public AudioSource Source; // O sesin AudioSource bileseni
        public AudioClip AudioClip; // Calinacak ses dosyasi
        public string Name; // Bu sese verilen isim
        [Range(0f, 1f)] public float Volume; // Ses seviyesi
        [Range(.1f, 3f)] public float Pitch; // Ses tonu
        public bool Mute; // Sessize alma durumu
        public bool Loop; // Dongulu calma
        public bool playOnAwake; // Oyun basladiginda otomatik calsin mi?
    }
}
