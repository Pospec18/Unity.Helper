using UnityEngine;

namespace Pospec.Helper
{
    /// <summary>
    /// Static reference to object of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            Instance = this as T;
        }

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Reference to single object of type T in scene. If more object of type T are in the scene, they will be deleted
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                Debug.LogWarning("More than one " + Instance.ToString() + " scripts at once");
            }
            base.Awake();
        }
    }

    /// <summary>
    /// Singleton that is not destroyed while moving between scenes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// Singleton for not MonoBehaviour classes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonClass<T> where T : new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null) instance = new T();
                return instance;
            }
        }
    }
}