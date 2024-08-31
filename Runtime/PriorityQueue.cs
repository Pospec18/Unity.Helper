using System;
using System.Collections;
using System.Collections.Generic;

namespace Pospec.Helper
{
    /// <summary>
    /// Minimum heap, sorted by default or custom comparer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> : IReadOnlyCollection<T>, ICollection
    {
        private readonly IList<T> items = new List<T>();
        private readonly IComparer<T> comparer = Comparer<T>.Default;

        public int Count => items.Count;
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        public PriorityQueue() {}
        public PriorityQueue(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public PriorityQueue(List<T> items) : this(items, Comparer<T>.Default) {}
        public PriorityQueue(List<T> items, IComparer<T> comparer)
        {
            this.comparer = comparer;
            this.items = items;
            HealBuild();
        }

        public PriorityQueue(IEnumerable<T> items) : this(items, Comparer<T>.Default) {}
        public PriorityQueue(IEnumerable<T> items, IComparer<T> comparer)
        {
            this.comparer = comparer;
            this.items = new List<T>(items);
            HealBuild();
        }

        /// <summary>
        /// Add item to queue
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            int idx = items.Count;
            items.Add(item);
            BubbleUp(idx);
        }

        /// <summary>
        /// Get first element of queue
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return items[0];
        }

        /// <summary>
        /// Tries to get first element of queue
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryPeek(out T item)
        {
            if (items.Count > 0)
            {
                item = items[0];
                return true;
            }

            item = default(T);
            return false;
        }

        /// <summary>
        /// Removes first element of queue and returns it
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            var peek = items[0];
            items[0] = items[items.Count - 1];
            items.RemoveAt(Count - 1);
            BubbleDown(0);

            return peek;
        }

        /// <summary>
        /// Tries to remove first element of queue and return it
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryDequeue(out T item)
        {
            if (items.Count > 0)
            {
                item = Dequeue();
                return true;
            }

            item = default(T);
            return false;
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array is null");
            if (index < 0)
                throw new ArgumentOutOfRangeException("arrayIndex is less than 0");
            if (index + Count < array.Length)
                throw new ArgumentException("not enough space in array to copy Heap");

            for (int i = 0; i < Count; i++)
                array.SetValue(items[i], i + index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        private void HealBuild()
        {
            for (int i = items.Count / 2; i >= 0; i--)
                BubbleDown(i);
        }

        private void BubbleUp(int i)
        {
            while (i > 0)
            {
                int p = Parent(i);
                var parent = items[p];
                if (comparer.Compare(parent, items[i]) <= 0)
                    return;

                items[p] = items[i];
                items[i] = parent;
                i = p;
            }
        }

        private void BubbleDown(int i)
        {
            int c;
            while ((c = SmallerChild(i)) > 0)
            {
                var child = items[c];
                if (comparer.Compare(items[i], child) <= 0)
                    return;

                items[c] = items[i];
                items[i] = child;
                i = c;
            }
        }

        private static int Parent(int i)
        {
            return i / 2;
        }

        private int SmallerChild(int i)
        {
            int left = 2 * i;
            int right = 2 * i + 1;

            if (items.Count > right)
                return comparer.Compare(items[left], items[right]) < 0 ? left : right;
            else if (items.Count > left)
                return left;

            return -1;
        }
    }
}
