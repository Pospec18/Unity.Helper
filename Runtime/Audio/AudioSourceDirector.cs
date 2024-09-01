using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Pospec.Helper.Audio
{
    public class AudioSourceDirector : MonoBehaviour
    {
        private IObjectPool<AudioSourceDirector> pool;
        [SerializeField] private AudioSource source;

        public void Setup(IObjectPool<AudioSourceDirector> pool, AudioSource source)
        {
            this.pool = pool;
            this.source = source;
        }

        public void Play(AudioClip clip, AudioProperties properties = default)
        {
            if (source == null || clip == null)
                return;

            if (properties == null)
                properties = new AudioProperties();

            source.clip = clip;
            source.volume = properties.volume;
            source.pitch = Random.Range(properties.minPitch, properties.maxPitch);
            source.Play();

            Invoke(nameof(ReturnToPool), clip.length);
        }

        public void Play(List<AudioClip> clips, AudioProperties properties = default)
        {
            if (source == null || clips == null || clips.Count == 0)
                return;
            Play(clips.Rand(), properties);
        }

        private void ReturnToPool()
        {
            if (pool != null)
                pool.Release(this);
        }
    }
}
