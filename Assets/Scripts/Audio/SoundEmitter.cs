using System;
using System.Collections;
using UnityEngine;
namespace AudioSystem
{
    public class SoundEmitter : MonoBehaviour
    {
        public AudioSource audioSource;
        private Coroutine playingCoroutine;
        public Action OnEnd;
        bool returnedToPool;
        private void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void Initialize(SoundData data) 
        {
            data.InitAudioSource(audioSource);
        }

        public void Play() 
        {
            returnedToPool = false;
            if (playingCoroutine != null)
            {
                StopCoroutine(playingCoroutine);
                playingCoroutine = null;
            }

            audioSource.Play();
            playingCoroutine = StartCoroutine(WaitForSoundToEnd());
        }

        private IEnumerator WaitForSoundToEnd()
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
            OnEnd?.Invoke();
            if(!returnedToPool)SoundManager.Instance.ReturnToPool(this);
            returnedToPool = true;
        }

        public void Stop() 
        {
            if (playingCoroutine != null)
            {
                StopCoroutine(playingCoroutine);
                playingCoroutine = null;
            }
            
            audioSource.Stop();
            if (!returnedToPool) SoundManager.Instance.ReturnToPool(this);
            returnedToPool = true;
        }
        public void Cleanup()
        {
            OnEnd = null;
        }
    }
}
