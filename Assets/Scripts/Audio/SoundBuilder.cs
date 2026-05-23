using System;
using UnityEngine;
namespace AudioSystem
{
    public class SoundBuilder 
    {
        SoundData Data;
        public SoundEmitter emitter;
        Vector3 position = Vector3.zero;
        bool randomPitch;
        Action onEnd;
        float panStereo = 0;
        float volume = 1;
        public SoundBuilder() { }

        public SoundBuilder WithPosition(Vector3 pos) 
        {
            position = pos;
            return this;
        }
        public SoundBuilder WithSoundData(SoundData data) 
        {
            Data = data;
            randomPitch = data.randomizePitch;
            volume = data.volume;
            return this;
        }
        public SoundBuilder WithStereoPan(float panStereo) 
        {
            this.panStereo = panStereo;
            return this;
        }
        public SoundBuilder Play() 
        {
            if (!SoundManager.Instance.CanPlaySound(Data)) return this;
            emitter = SoundManager.Instance.GetEmitter();
            emitter.Initialize(Data);
            emitter.transform.position = position;
            emitter.transform.parent = SoundManager.Instance.transform;
            emitter.audioSource.panStereo = panStereo;
            emitter.audioSource.volume = volume;
            if (randomPitch) WithRandomPitch(emitter);
            else emitter.audioSource.pitch = 1;
            if (Data.isFrequentSound) SoundManager.Instance.frequentEmitters.Enqueue(emitter);
            emitter.Play();
            emitter.OnEnd += onEnd;
            emitter.OnEnd += () => emitter = null;
            return this;
        }
        public SoundBuilder Stop() 
        {
            if (emitter!=null) emitter.Stop();
            return this;
        }
        public SoundBuilder WithEndAction(Action onEnd) 
        {
            this.onEnd = onEnd;
            return this;
        }
        private void WithRandomPitch(SoundEmitter emitter) 
        {
            emitter.audioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        }
    }
}
