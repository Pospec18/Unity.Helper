using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

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

        #region Vector Extentions

        public static Vector3 SetX(this Vector3 v, float x) => new Vector3(x, v.y, v.z);
        public static Vector3 SetY(this Vector3 v, float y) => new Vector3(v.x, y, v.z);
        public static Vector3 SetZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);

        public static Vector2 SetX(this Vector2 v, float x) => new Vector2(x, v.y);
        public static Vector2 SetY(this Vector2 v, float y) => new Vector2(v.x, y);
        public static Vector2Int SetX(this Vector2Int v, int x) => new Vector2Int(x, v.y);
        public static Vector2Int SetY(this Vector2Int v, int y) => new Vector2Int(v.x, y);

        public static Vector2 AddX(this Vector2 v, float x) => new Vector2(v.x + x, v.y);
        public static Vector2 AddY(this Vector2 v, float y) => new Vector2(v.x, v.y + y);

        public static float Angle(this Vector2 v) => Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        public static Vector3 PlaneProjection(this Vector2 v) => new Vector3(v.x, 0, v.y);

        public static Vector3 Multiply(this Vector3 v, Vector3 a) => new Vector3(v.x * a.x, v.y * a.y, v.z * a.z);

        public static Vector2 Mod(this Vector2 v, int m) => new Vector2(v.x % m, v.y % m);
        public static Vector2Int Mod(this Vector2Int v, int m) => new Vector2Int(v.x % m, v.y % m);
        public static Vector3 Mod(this Vector3 v, int m) => new Vector3(v.x % m, v.y % m, v.z % m);

        public static List<Vector2Int> GetNeighbours(this Vector2Int center, bool allowDiagonal = true)
        {
            List<Vector2Int> neighbours = new List<Vector2Int>();
            neighbours.Add(center + Vector2Int.left);
            neighbours.Add(center + Vector2Int.right);
            neighbours.Add(center + Vector2Int.up);
            neighbours.Add(center + Vector2Int.down);

            if (allowDiagonal)
            {
                neighbours.Add(center + new Vector2Int(1, 1));
                neighbours.Add(center + new Vector2Int(1, -1));
                neighbours.Add(center + new Vector2Int(-1, 1));
                neighbours.Add(center + new Vector2Int(-1, -1));
            }

            return neighbours;
        }

        public static List<Vector2> GetNeighbours(this Vector2 center, bool allowDiagonal = true)
        {
            List<Vector2> neighbours = new List<Vector2>();
            neighbours.Add(center + Vector2.left);
            neighbours.Add(center + Vector2.right);
            neighbours.Add(center + Vector2.up);
            neighbours.Add(center + Vector2.down);

            if (allowDiagonal)
            {
                neighbours.Add(center + new Vector2(1, 1));
                neighbours.Add(center + new Vector2(1, -1));
                neighbours.Add(center + new Vector2(-1, 1));
                neighbours.Add(center + new Vector2(-1, -1));
            }

            return neighbours;
        }

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

        public static Vector3 CountAveragePosition(this IEnumerable<Component> components)
        {
            Vector3 sum = Vector3.zero;
            int count = 0;
            foreach (var item in components)
            {
                sum += item.transform.position;
                count++;
            }
            return sum / count;
        }

        public static bool Exists(this Component component)
        {
            return component != null && component.gameObject != null && component.gameObject.scene != null;
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

        #region LayerMask Extentions

        public static bool Contains(this LayerMask mask, int layerNumber)
        {
            return mask == (mask | (1 << layerNumber));
        }

        #endregion

        #region Numeric Extentions

        public static float Normalize(this float x)
        {
            if (x == 0)
                return 0;
            return x > 0 ? 1 : -1;
        }

        #endregion

        #region Input Extensions

        public static bool IsMouseOverLayer(int layer)
        {
            var eventSystemRaysastResults = GetEventSystemRaycastResults();
            foreach (var raycastResult in eventSystemRaysastResults)
                if (raycastResult.gameObject.layer == layer)
                    return true;
            return false;
        }


        public static bool IsMouseOverLayer(LayerMask mask)
        {
            var eventSystemRaysastResults = GetEventSystemRaycastResults();
            foreach (var raycastResult in eventSystemRaysastResults)
                if (mask.Contains(raycastResult.gameObject.layer))
                    return true;
            return false;
        }

        // From https://forum.unity.com/threads/how-to-detect-if-mouse-is-over-ui.1025533/
        private static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
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
        #endregion
    }
}