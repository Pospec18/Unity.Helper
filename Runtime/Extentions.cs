using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper
{
    public static class Extentions
    {
        #region Collection Extention

        /// <summary>
        /// Return a random item from the list.
        /// Sampling with replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Rand<T>(this IList<T> list)
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("Cannot select random item from an empty list");
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Return a random item from the array.
        /// Sampling with replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T Rand<T>(this T[] arr)
        {
            if (arr.Length == 0)
                throw new IndexOutOfRangeException("Cannot select random item from an empty array");
            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }

        /// <summary>
        /// Return a random item from the collection.
        /// Sampling with replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T Rand<T>(this IEnumerable<T> collection)
        {
            List<T> list = new List<T>(collection);
            return list.Rand();
        }

        /// <summary>
        /// Removes a random item from the list, returning that item.
        /// Sampling without replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RemoveRandom<T>(this IList<T> list)
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("Cannot remove a random item from an empty list");
            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        /// <summary>
        /// Shuffle the list in place using the Fisher-Yates method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                T value = list[j];
                list[j] = list[i];
                list[i] = value;
            }
        }

        public static T Last<T>(this IList<T> list)
        {
            if (!list.TryLast(out T item))
                throw new IndexOutOfRangeException("Cannot select last item from an empty list");
            return item;
        }

        public static bool TryLast<T>(this IList<T> list, out T item)
        {
            item = default(T);
            if (list.Count == 0)
                return false;
            item = list[list.Count - 1];
            return true;
        }

        #endregion

        #region Vector Extentions

        public static Vector3 SetX(this Vector3 v, float x) => new Vector3(x, v.y, v.z);
        public static Vector3 SetY(this Vector3 v, float y) => new Vector3(v.x, y, v.z);
        public static Vector3 SetZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);

        public static Vector2 SetX(this Vector2 v, float x) => new Vector2(x, v.y);
        public static Vector2 SetY(this Vector2 v, float y) => new Vector2(v.x, y);
        public static Vector2Int SetX(this Vector2Int v, int x) => new Vector2Int(x, v.y);
        public static Vector2Int SetY(this Vector2Int v, int y) => new Vector2Int(v.x, y);

        public static float Angle(this Vector2 v) => Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        public static Vector3 PlaneProjection(this Vector2 v) => new Vector3(v.x, 0, v.y);

        public static Vector3 Multiply(this Vector3 v, Vector3 a) => new Vector3(v.x * a.x, v.y * a.y, v.z * a.z);

        public static Vector2 Mod(this Vector2 v, int m) => new Vector2(v.x % m, v.y % m);
        public static Vector2Int Mod(this Vector2Int v, int m) => new Vector2Int(v.x % m, v.y % m);
        public static Vector3 Mod(this Vector3 v, int m) => new Vector3(v.x % m, v.y % m, v.z % m);

        #endregion

        #region Component Extentions

        public static float DistanceTo(this Component a, Component b) => Vector3.Distance(a.transform.position, b.transform.position);
        public static float Distance2DTo(this Component a, Component b) => Vector2.Distance(a.transform.position, b.transform.position);

        public static Component GetClosest(this Component component, IEnumerable<Component> components)
        {
            float minDist = float.MaxValue;
            Component closest = null;
            foreach (Component item in components)
            {
                float dist = item.DistanceTo(component);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = item;
                }
            }

            if (closest == null)
                throw new Exception("The collection of other Components is empty");
            return closest.transform;
        }
        
        public delegate int Dist(Component a, Component b);

        public static Component GetClosest(this Component component, IEnumerable<Component> components, Dist distFunction)
        {
            float minDist = float.MaxValue;
            Component closest = null;
            foreach (Component item in components)
            {
                float dist = distFunction(component, item);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = item;
                }
            }

            if (closest == null)
                throw new Exception("The collection of other Components is empty");
            return closest;
        }

        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            T result = component.GetComponent<T>();
            if (result == null)
                return component.gameObject.AddComponent<T>();
            return result;
        }

        public static bool HasComponent<T>(this Component component) where T : Component
        {
            return component.GetComponent<T>() != null;
        }

        #endregion

        #region Transform Extentions
        
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        public static void ResetTransform(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        #endregion

        #region LayerMask Extentions

        public static bool Contains(this LayerMask mask, int layerNumber)
        {
            return mask == (mask | (1 << layerNumber));
        }

        #endregion
    }
}