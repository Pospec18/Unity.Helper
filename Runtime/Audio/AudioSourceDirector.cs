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

        public void Play(Sound sound)
        {
            if (source == null || sound.clips == null || sound.clips.Count == 0)
                return;

            AudioClip clip = sound.clips.Rand();
            if (clip == null)
                return;
            source.clip = clip;
            source.volume = sound.volume;
            source.pitch = Random.Range(sound.minPitch, sound.maxPitch);
            source.loop = sound.looping;
            source.Play();

            if (!sound.looping)
                Invoke(nameof(ReturnToPool), clip.length);
        }

        private void ReturnToPool()
        {
            if (pool != null)
                pool.Release(this);
        }
    }
}
