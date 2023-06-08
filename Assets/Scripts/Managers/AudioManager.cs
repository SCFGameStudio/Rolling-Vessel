using System;
using Data.ValueObjects;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public SoundData[] Sounds;

        void Awake ()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            } 
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            foreach (SoundData s in Sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void Play(string sound)
        {
            SoundData s = Array.Find(Sounds, soundData => soundData.name == sound);
            s.source.Play();
        }
        public void Stop(string sound)
        {
            SoundData s = Array.Find(Sounds, soundData => soundData.name == sound);
            s.source.Stop();
        }

    }
}