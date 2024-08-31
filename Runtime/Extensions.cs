using Pospec.Helper.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pospec.Helper
{
    public static class Extensions
    {
        #region Collection Extension

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

        public static bool TryUpperBound<T>(this IList<T> list, T value, out T item)
        {
            return list.TryUpperBound(value, Comparer<T>.Default, out item);
        }

        public static bool TryUpperBound<T>(this IList<T> list, T value, IComparer<T> comp, out T item)
        {
            item = default(T);
            int i = list.UpperBoundIndex(value, comp);
            if (i >= list.Count)
                return false;

            item = list[i];
            return true;
        }

        public static bool TryLowerBound<T>(this IList<T> list, T value, out T item)
        {
            return list.TryLowerBound(value, Comparer<T>.Default, out item);
        }

        public static bool TryLowerBound<T>(this IList<T> list, T value, IComparer<T> comp, out T item)
        {
            item = default(T);
            int i = list.LowerBoundIndex(value, comp);
            if (i < 0)
                return false;

            item = list[i];
            return true;
        }

        public static int UpperBoundIndex<T>(this IList<T> list, T value)
        {
            return list.UpperBoundIndex(value, Comparer<T>.Default);
        }

        public static int UpperBoundIndex<T>(this IList<T> list, T value, IComparer<T> comp)
        {
            int i = list.BinarySearch(value, comp);
            if (comp.Compare(list[i], value) < 0)
                i++;

            return i;
        }

        public static int LowerBoundIndex<T>(this IList<T> list, T value)
        {
            return list.LowerBoundIndex(value, Comparer<T>.Default);
        }

        public static int LowerBoundIndex<T>(this IList<T> list, T value, IComparer<T> comp)
        {
            int i = list.BinarySearch(value, comp);
            if (comp.Compare(list[i], value) > 0)
                i--;

            return i;
        }

        public static int BinarySearch<T>(this IList<T> list, T value)
        {
            return list.BinarySearch(value, Comparer<T>.Default);
        }

        public static int BinarySearch<T>(this IList<T> list, T value, IComparer<T> comp)
        {
            if (list == null)
                throw new ArgumentNullException("list is null");
            int low = 0, high = list.Count - 1;
            while (low < high)
            {
                int m = (high + low) / 2;
                if (comp.Compare(list[m], value) < 0)
                    low = m + 1;
                else
                    high = m - 1;
            }
            return low;
        }

        #endregion

        #region Component Extensions

        public static float DistanceTo(this Component a, Component b) => Vector3.Distance(a.transform.position, b.transform.position);
        public static float DistanceTo(this Component a, Component b, INorm norm) => norm.Length(a.transform.position - b.transform.position);
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

        public static Component GetClosest(this Component component, IEnumerable<Component> components, INorm norm)
        {
            float minDist = float.MaxValue;
            Component closest = null;
            foreach (Component item in components)
            {
                float dist = item.DistanceTo(component, norm);
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

        public static bool Exists(this Component component)
        {
            return component != null && component.gameObject != null && component.gameObject.scene != null;
        }

        #endregion

        #region Transform Extensions

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

        #region Camera Extensions

        public static bool InCameraView2D(this Camera cam, Bounds bounds)
        {
            return cam.OrthographicBounds().Intersects(bounds);
        }

        public static Bounds OrthographicBounds(this Camera cam)
        {
            Vector2 size = new Vector2(cam.aspect, 1) * cam.orthographicSize;
            return new Bounds((Vector2)cam.transform.position, size * 2);
        }

        #endregion

        #region LayerMask Extensions

        public static bool Contains(this LayerMask mask, Component component)
        {
            return mask.Contains(component.gameObject.layer);
        }


        public static bool Contains(this LayerMask mask, int layerNumber)
        {
            return mask == (mask | (1 << layerNumber));
        }

        #endregion

        #region String Extensions
        public static MemoryStream ToStream(this string str)
        {
            return ToStream(str, Encoding.UTF8);
        }

        public static MemoryStream ToStream(this string str, Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(str ?? ""), false);
        }

        /// <summary>
        /// Puts the string into the Clipboard.
        /// From https://thatfrenchgamedev.com/785/unity-2018-how-to-copy-string-to-clipboard/
        /// </summary>
        public static void CopyToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }
        #endregion

        #region Mouse Over
        public static bool IsMouseOverObject(this GameObject gameObject)
        {
            var eventSystemRaycastResults = GetEventSystemRaycastResults();
            foreach (var raycastResult in eventSystemRaycastResults)
                if (raycastResult.gameObject == gameObject)
                    return true;
            return false;
        }

        public static bool IsMouseOverLayer(int layer)
        {
            var eventSystemRaycastResults = GetEventSystemRaycastResults();
            foreach (var raycastResult in eventSystemRaycastResults)
                if (raycastResult.gameObject.layer == layer)
                    return true;
            return false;
        }

        public static bool IsMouseOverLayer(this LayerMask mask)
        {
            var eventSystemRaycastResults = GetEventSystemRaycastResults();
            foreach (var raycastResult in eventSystemRaycastResults)
                if (mask.Contains(raycastResult.gameObject.layer))
                    return true;
            return false;
        }

        // From https://forum.unity.com/threads/how-to-detect-if-mouse-is-over-ui.1025533/
        private static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            return raycastResults;
        }
        #endregion
    }
}