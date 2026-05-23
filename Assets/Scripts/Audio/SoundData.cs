using System;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    [Serializable]
    public class SoundData
    {
        public AudioClip clip;
        public bool loop;
        public bool playOnAwake;
        public AudioMixerGroup mixer;
        public bool isFrequentSound;
        public bool randomizePitch;
        public float volume = 1;
        public SoundData() { }
        public SoundData(AudioClip clip, bool loop, bool playOnAwake, AudioMixerGroup mixer, bool isFrequentSound, bool randomizePitch)
        {
            this.clip = clip;
            this.loop = loop;
            this.playOnAwake = playOnAwake;
            this.mixer = mixer;
            this.isFrequentSound = isFrequentSound;
            this.randomizePitch = randomizePitch;
        }
        public SoundData(AudioClip clip, bool loop, bool playOnAwake, AudioMixerGroup mixer, bool isFrequentSound, bool randomizePitch, float volume)
        {
            this.clip = clip;
            this.loop = loop;
            this.playOnAwake = playOnAwake;
            this.mixer = mixer;
            this.isFrequentSound = isFrequentSound;
            this.randomizePitch = randomizePitch;
            this.volume = volume;
        }

        public void InitAudioSource(AudioSource audioSource) 
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.playOnAwake = playOnAwake;
            audioSource.outputAudioMixerGroup = mixer;
        }
    }
}
