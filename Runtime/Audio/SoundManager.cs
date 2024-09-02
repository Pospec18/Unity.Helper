using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Pospec.Helper.Audio
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource sourcePrefab;

        private ObjectPool<AudioSourceDirector> _pool;
        private ObjectPool<AudioSourceDirector> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<AudioSourceDirector>(CreatePoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyedPoolObject);
                return _pool;
            }
        }

        private void Awake()
        {
            SingletonProvider.Add(this);
        }

        private void OnDestroy()
        {
            SingletonProvider.Remove(this);
        }

        private AudioSourceDirector CreatePoolItem()
        {
            AudioSource source;
            if (sourcePrefab == null)
            {
                var go = new GameObject("Pooled Audio Source");
                go.transform.parent = transform;                
                source = go.AddComponent<AudioSource>();
                source.playOnAwake = false;
                source.loop = false;
            }
            else
            {
                source = Instantiate(sourcePrefab, transform);
            }

            var poolSource = source.gameObject.AddComponent<AudioSourceDirector>();
            poolSource.Setup(Pool, source);
            return poolSource;
        }

        private void OnTakeFromPool(AudioSourceDirector source)
        {
            source.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(AudioSourceDirector source)
        {
            source.gameObject.SetActive(false);
        }

        private void OnDestroyedPoolObject(AudioSourceDirector source)
        {
            Destroy(source.gameObject);
        }

        public void Play(Sound sound)
        {
            if (sound != null && sound.clips != null && sound.clips.Count > 0)
            {
                try
                {
                    Pool.Get().Play(sound);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }

    [Serializable]
    public class Sound
    {
        public List<AudioClip> clips;

        [Range(.1f, 3f)] public float minPitch = 0.85f;
        [Range(.1f, 3f)] public float maxPitch = 1.15f;
        [Range(0, 1)] public float volume = 1f;
        public bool looping = false;
    }
}
