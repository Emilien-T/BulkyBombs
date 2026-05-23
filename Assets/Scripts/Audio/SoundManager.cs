using UnityEngine.Pool;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

namespace AudioSystem
{
    public class SoundManager : Controller<SoundManager>
    {
        IObjectPool<SoundEmitter> emitterPool;
        public List<SoundEmitter> activeEmitters = new();
        public SoundBuilder mainMusic;
        public Queue<SoundEmitter> frequentEmitters = new();
        public AudioMixer mixer;

        private float minVolume = -61;
        private float maxVolume = 0;

        private float sfxVolumeOffset = 0;

        [SerializeField] SoundEmitter emitterPrefab;
        [SerializeField] bool collectionCheck = true;
        [SerializeField] int defaultCapacity = 10;
        [SerializeField] int maxPoolSize = 100;
        [SerializeField] int frequentEmitterMax = 15;

        public SoundBuilder CreateSound() => new SoundBuilder();
        protected override void MyAwake()
        {
            DontDestroyOnLoad(gameObject);
            activeEmitters = new List<SoundEmitter>();
            emitterPool = new ObjectPool<SoundEmitter>(
                CreateSoundEmitter,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                collectionCheck,
                defaultCapacity,
                maxPoolSize);
        }
        public bool CanPlaySound(SoundData data) 
        {
            if (!data.isFrequentSound) return true;

            if (frequentEmitters.Count >= frequentEmitterMax && frequentEmitters.TryDequeue(out SoundEmitter soundEmitter)) 
            {
                try
                {
                    soundEmitter.Stop();
                    return true;
                }
                catch { }
                return false;
            }
            return true;
        }
        public SoundEmitter GetEmitter() 
        {
            return emitterPool.Get();
        }
        public void ReturnToPool(SoundEmitter emitter) 
        {
            emitterPool.Release(emitter);
        }
        private SoundEmitter CreateSoundEmitter()
        {
            SoundEmitter emitter = Instantiate(emitterPrefab);
            emitter.transform.SetParent(gameObject.transform, false);
            emitter.gameObject.SetActive(false);
            return emitter;
        }
        private void OnTakeFromPool(SoundEmitter emitter) 
        {
            emitter.gameObject.SetActive(true);
            activeEmitters.Add(emitter);
        }
        private void OnReturnedToPool(SoundEmitter emitter) 
        {
            emitter.Cleanup();
            emitter.gameObject.SetActive(false);
            activeEmitters.Remove(emitter);
        }
        private void OnDestroyPoolObject(SoundEmitter emitter) 
        {
            Destroy(emitter);
        }

        public void SetMasterVolume(float percentage) 
        {
            percentage = Mathf.Clamp(percentage, 0.0000001f, 1);
            mixer.SetFloat("MasterVolume", Mathf.Log10(percentage * 60f));
        }
        public void SetSFXVolume(float percentage)
        {
            percentage = Mathf.Clamp(percentage, 0.0000001f, 1);
            mixer.SetFloat("SFXVolume", Mathf.Log10(percentage) * 60f);
        }
        public void SetMusicVolume(float percentage)
        {
            percentage = Mathf.Clamp(percentage, 0.0000001f, 1);
            mixer.SetFloat("MusicVolume", Mathf.Log10(percentage) * 60f);
        }
        public void SetCarVolume(float percentage)
        {
            percentage = Mathf.Clamp(percentage, 0.0000001f, 1);
            mixer.SetFloat("CarVolume", Mathf.Log10(percentage) * 60f);
        }
    }
}
