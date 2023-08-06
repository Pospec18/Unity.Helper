using System;
using System.Collections.Generic;
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

    /// <summary>
    /// Provider for acessing singleton classes.
    /// </summary>
    public static class SingletonProvider
    {
        private static readonly Dictionary<Type, object> singletons = new();

        /// <summary>
        /// Stores object as singleton for given type.
        /// If singleton is going to be overwrited, removes the old one and replaces it with currently provided object.
        /// </summary>
        /// <typeparam name="T">type of singleton</typeparam>
        /// <param name="toAdd">object to be stored as singleton</param>
        public static void Add<T>(T toAdd) where T : class
        {
            if (singletons.TryGetValue(typeof(T), out object val))
            {
                singletons.Remove(typeof(T));
                Debug.LogWarning("Overwriting singleton of " + typeof(T));
                if (val.GetType().IsSubclassOf(typeof(UnityEngine.Object)))
                {
                    Debug.Log("Deleting old singleton of " + typeof(T));
                    UnityEngine.Object.Destroy((UnityEngine.Object)val);
                }
            }

            singletons.Add(typeof(T), toAdd);
        }

        /// <summary>
        /// Stores object as singleton for given type.
        /// Makes object not destructible when changing scenes.
        /// If singleton is going to be overwrited, deletes currently provided object and keeps the old one.
        /// </summary>
        /// <typeparam name="T">type of singleton</typeparam>
        /// <param name="toAdd">object to be stored as singleton</param>
        public static void AddPersistentObject<T>(T toAdd) where T : UnityEngine.Object
        {
            if (singletons.ContainsKey(typeof(T)))
            {
                UnityEngine.Object.Destroy(toAdd);
                Debug.LogWarning($"Singleton of {typeof(T)} already exists. Deleting this one.", toAdd);
                return;
            }

            singletons.Add(typeof(T), toAdd);
            UnityEngine.Object.DontDestroyOnLoad(toAdd);
        }

        /// <summary>
        /// Removes singleton record.
        /// Checks if object to be removed is same as current singleton.
        /// </summary>
        /// <typeparam name="T">type of singleton</typeparam>
        /// <param name="toRemove">object to be removed</param>
        public static void Remove<T>(T toRemove) where T : class
        {
            if (!singletons.TryGetValue(typeof(T), out object val) || !EqualityComparer<T>.Default.Equals((T)val, toRemove))
            {
                Debug.LogWarning(toRemove.ToString() + " not found in singletons");
                return;
            }

            singletons.Remove(typeof(T));
        }

        /// <summary>
        /// Gets current singleton of given type
        /// </summary>
        /// <typeparam name="T">type of singleton</typeparam>
        public static T Get<T>() where T : class
        {
            if (singletons.TryGetValue(typeof(T), out object val))
                return (T)val;

            Debug.LogWarning($"No instance of {typeof(T)} found.");
            return default;
        }
    }
}
