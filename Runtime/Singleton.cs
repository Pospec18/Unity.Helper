using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pospec.Helper
{
    /// <summary>
    /// Provider for accessing singleton classes.
    /// </summary>
    public static class SingletonProvider
    {
        private static readonly Dictionary<Type, object> singletons = new();
        private static readonly Dictionary<Type, EventWrapper<object>> onChangeCallbacks = new();
        private static bool initialized = false;

        private static void Initialize()
        {
            if (initialized)
                return;

            SceneManager.sceneUnloaded += (_) => OnSceneChanged();
            initialized = true;
        }

        private static void OnSceneChanged()
        {
            List<Type> toRemove = new();
            foreach (var pair in singletons)
                if (pair.Value.GetType().IsSubclassOf(typeof(Component)) && !((Component)pair.Value).Exists())
                    toRemove.Add(pair.Key);

            foreach (var type in toRemove)
            {
                singletons.Remove(type);
                if (onChangeCallbacks.TryGetValue(type, out EventWrapper<object> callback))
                    callback?.Invoke(null);
            }
        }

        /// <summary>
        /// Stores object as singleton for given type.
        /// If singleton already exists, deletes currently provided object and keeps the old one.
        /// If T is MonoBehaviour add `SingletonProvider.Remove(this);` to OnDestroy callback.
        /// </summary>
        /// <typeparam name="T">type of singleton</typeparam>
        /// <param name="toAdd">object to be stored as singleton</param>
        public static void Add<T>(T toAdd) where T : class
        {
            Initialize();
            if (singletons.TryGetValue(typeof(T), out object val))
            {
                if (val != null && val.GetType().IsSubclassOf(typeof(Component)))
                {
                    Component c = (Component)val;
                    if (c.Exists())
                    {
                        Debug.LogWarning($"Singleton of {typeof(T)} already exists. Deleting currently provided component.", c);
                        UnityEngine.Object.Destroy(toAdd as Component);
                        return;
                    }
                    else
                    {
                        singletons.Add(typeof(T), toAdd);
                        if (onChangeCallbacks.TryGetValue(typeof(T), out EventWrapper<object> callback1))
                            callback1?.Invoke(toAdd);
                        return;
                    }
                }

                Debug.LogWarning("Overwriting singleton of " + typeof(T));
                singletons.Remove(typeof(T));
            }

            singletons.Add(typeof(T), toAdd);
            if (onChangeCallbacks.TryGetValue(typeof(T), out EventWrapper<object> callback2))
                callback2?.Invoke(toAdd);
        }

        /// <summary>
        /// Stores object as singleton for given type.
        /// Makes object not destructible when changing scenes.
        /// If singleton already exists, deletes currently provided gameObject and keeps the old one.
        /// If T is MonoBehaviour add `SingletonProvider.Remove(this);` to OnDestroy callback.
        /// </summary>
        /// <typeparam name="T">type of singleton</typeparam>
        /// <param name="toAdd">object to be stored as singleton</param>
        public static void AddPersistentObject<T>(T toAdd) where T : Component
        {
            Initialize();
            if (singletons.ContainsKey(typeof(T)))
            {
                UnityEngine.Object.Destroy(toAdd.gameObject);
                Debug.LogWarning($"Persistent Singleton of {typeof(T)} already exists. Deleting currently provided object.", toAdd);
                return;
            }

            singletons.Add(typeof(T), toAdd);
            if (onChangeCallbacks.TryGetValue(typeof(T), out EventWrapper<object> callback))
                callback?.Invoke(toAdd);
            UnityEngine.Object.DontDestroyOnLoad(toAdd);
        }

        /// <summary>
        /// Stores object as singleton for given type.
        /// Makes object not destructible when changing scenes.
        /// If singleton already exists, deletes currently provided gameObject and keeps the old one.
        /// If T is MonoBehaviour add `SingletonProvider.Remove(this);` to OnDestroy callback.
        /// </summary>
        /// <typeparam name="T">type of object to be stored</typeparam>
        /// <typeparam name="I">type of singleton</typeparam>
        /// <param name="toAdd">object to be stored as singleton</param>
        public static void AddPersistentObject<T, I>(T toAdd) where T : Component, I
        {
            Initialize();
            if (singletons.ContainsKey(typeof(I)))
            {
                UnityEngine.Object.Destroy(toAdd.gameObject);
                Debug.LogWarning($"Persistent Singleton of {typeof(I)} already exists. Deleting currently provided object.", toAdd);
                return;
            }

            singletons.Add(typeof(I), toAdd);
            if (onChangeCallbacks.TryGetValue(typeof(T), out EventWrapper<object> callback))
                callback?.Invoke(toAdd);
            UnityEngine.Object.DontDestroyOnLoad(toAdd);
        }


        public static void AddChangeCallback<T>(Action<object> onChange) where T : class
        {

            if (onChangeCallbacks.TryGetValue(typeof(T), out EventWrapper<object> callback))
            {
                callback.Subscribe(onChange);
            }
            else
            {
                callback = new EventWrapper<object>();
                callback.Subscribe(onChange);
                onChangeCallbacks.Add(typeof(T), callback);
            }
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
            if (onChangeCallbacks.TryGetValue(typeof(T), out EventWrapper<object> callback))
                callback?.Invoke(null);
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
