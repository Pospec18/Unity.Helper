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
                if (pair.Value is Component c && !c.Exists())
                    toRemove.Add(pair.Key);

            foreach (var type in toRemove)
                RemoveInternal(type);
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
                if (val == toAdd)
                    return;

                if (val != null && val is Component c)
                {
                    if (c.Exists())
                    {
                        Debug.LogWarning($"Singleton of {typeof(T)} already exists. Deleting currently provided component.", c);
                        UnityEngine.Object.Destroy(toAdd as Component);
                        return;
                    }
                }
                else
                {
                    Debug.LogWarning("Overwriting singleton of " + typeof(T));
                    singletons.Remove(typeof(T));
                }
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
        /// <typeparam name="T">type of object to be stored</typeparam>
        /// <typeparam name="I">type of singleton</typeparam>
        /// <param name="toAdd">object to be stored as singleton</param>
        public static void AddPersistentObject<T, I>(T toAdd) where T : Component, I where I : class
        {
            Initialize();
            if (singletons.TryGetValue(typeof(T), out object val))
            {
                if (val == toAdd)
                {
                    UnityEngine.Object.DontDestroyOnLoad(toAdd);
                    return;
                }

                if (val != null && val is Component c)
                {
                    if (c.Exists())
                    {
                        Debug.LogWarning($"Persistent Singleton of {typeof(I)} already exists. Deleting currently provided object.", c);
                        UnityEngine.Object.Destroy(toAdd.gameObject);
                        return;
                    }
                }
                else
                {
                    Debug.LogWarning("Overwriting singleton of " + typeof(T));
                    singletons.Remove(typeof(T));
                }
            }

            singletons.Add(typeof(I), toAdd);
            if (onChangeCallbacks.TryGetValue(typeof(I), out EventWrapper<object> callback))
                callback?.Invoke(toAdd);
            UnityEngine.Object.DontDestroyOnLoad(toAdd);
        }

        /// <summary>
        /// When Singleton changes it's value, onChangeFunction is called with new Singleton
        /// </summary>
        /// <typeparam name="T">type of singleton</typeparam>
        /// <param name="onChange">function to be called</param>
        private static void AddChangeCallback<T>(Action<object> onChange) where T : class
        {
            if (onChange == null)
                return;

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
            RemoveInternal(typeof(T));
        }

        private static void RemoveInternal(Type type)
        {
            singletons.Remove(type);
            if (onChangeCallbacks.TryGetValue(type, out EventWrapper<object> callback))
                callback?.Invoke(null);
        }

        /// <summary>
        /// Get singleton for given type
        /// </summary>
        /// <typeparam name="T">type of singleton</typeparam>
        /// <param name="data">returned singleton</param>
        /// <param name="onChange">when singleton changes it's value, onChange is called with new singleton</param>
        /// <returns>does singleton exist for given type</returns>
        public static bool TryGet<T>(out T data, Action<object> onChange = null) where T : class
        {
            AddChangeCallback<T>(onChange);
            if (singletons.TryGetValue(typeof(T), out object val))
            {
                if (val == null || (val is Component c && !c.Exists()))
                {
                    RemoveInternal(typeof(T));
                }
                else
                {
                    data = (T)val;
                    return true;
                }
            }

            data = default;
            return false;
        }

        /// <summary>
        /// Get singleton or create new one if singleton doesn't exist.
        /// Do not pass Components, MonoBehaviours etc. without providing createFunc
        /// </summary>
        /// <typeparam name="T">type of singleton</typeparam>
        /// <param name="createFunc">function to create new singleton, if no function is provided singleton is created by new()</param>
        /// <param name="onChange">when singleton changes it's value, onChange is called with new singleton</param>
        /// <returns>valid singleton for given type</returns>
        public static T GetOrCreateNonComponent<T>(Func<T> createFunc = null, Action<object> onChange = null) where T : class, new()
        {
            if (TryGet(out T val, onChange))
                return val;

            Debug.Log($"No instance of {typeof(T)} found, creating new one");
            T obj;
            if (createFunc != null)
                obj = createFunc();
            else
                obj = new T();
            Add(obj);
            return obj;
        }

        /// <summary>
        /// Get singleton or create new one if singleton doesn't exist.
        /// </summary>
        /// <typeparam name="T">type of created singleton</typeparam>
        /// <typeparam name="I">type of singleton</typeparam>
        /// <param name="createFunc">function to create new singleton, if no function is provided new GameObject with given Component is created</param>
        /// <param name="onChange">when singleton changes it's value, onChange is called with new singleton</param>
        /// <returns>valid singleton for given type</returns>
        public static I GetOrCreateComponent<T, I>(Func<T> createFunc = null, Action<object> onChange = null) where T : Component, I where I : class
        {
            if (TryGet(out I val, onChange))
                return val;

            Debug.Log($"No instance of {typeof(I)} found, creating new one");
            T obj;
            if (createFunc != null)
                obj = createFunc();
            else
            {
                var go = new GameObject(typeof(I).Name);
                obj = go.AddComponent<T>();
                var returner = go.AddComponent<ReturnSingleton>();
                returner.returnCallback = () => Remove<I>(obj);
            }
            Add<I>(obj);
            return obj;
        }

        /// <summary>
        /// Get singleton or create new one if singleton doesn't exist.
        /// Makes object not destructible when changing scenes.
        /// </summary>
        /// <typeparam name="T">type of created singleton</typeparam>
        /// <typeparam name="I">type of singleton</typeparam>
        /// <param name="createFunc">function to create new singleton, if no function is provided new GameObject with given Component is created</param>
        /// <param name="onChange">when singleton changes it's value, onChange is called with new singleton</param>
        /// <returns>valid singleton for given type</returns>
        public static I GetOrCreatePersistentObject<T, I>(Func<T> createFunc = null, Action<object> onChange = null) where T : Component, I where I : class
        {
            if (TryGet(out I val, onChange))
            {
                if (val is Component c)
                    UnityEngine.Object.DontDestroyOnLoad(c);
                return val;
            }

            Debug.Log($"No instance of {typeof(I)} found, creating new one");
            T obj;
            if (createFunc != null)
                obj = createFunc();
            else
            {
                var go = new GameObject(typeof(I).Name);
                obj = go.AddComponent<T>();
                var returner = go.AddComponent<ReturnSingleton>();
                returner.returnCallback = () => Remove<I>(obj);
            }
            AddPersistentObject<T, I>(obj);
            return obj;
        }
    }
}
