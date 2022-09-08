using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper
{
    [Serializable]
    public class SelectableList<T> : IList<T>
    {
        [SerializeField] private int selectedIdx;
        [SerializeField] private bool someItemSelected = false;
        [SerializeField] private List<T> items;

        public T this[int index] { get => items[index]; set => items[index] = value; }

        public int Count => items.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Clear()
        {
            items.Clear();
            selectedIdx = 0;
            someItemSelected = false;
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        public bool TryGetIndexOf(T item, out int index)
        {
            index = items.IndexOf(item);
            return index >= 0;
        }

        public void Insert(int index, T item)
        {
            if (index <= selectedIdx)
                selectedIdx++;
        }

        public bool Remove(T item)
        {
            if (TryGetIndexOf(item, out int index) && index == selectedIdx)
                someItemSelected = false;
            return items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            if (index == selectedIdx)
                someItemSelected = false;
            items.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public bool TryGetSelectedItem(out T item)
        {
            item = default(T);
            if (!someItemSelected)
                return false;

            item = items[selectedIdx];
            return true;
        }

        public void SetSelectedItem(T item)
        {
            SetSelectedItem(IndexOf(item));
        }

        public void SetSelectedItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                selectedIdx = index;
                someItemSelected = true;
            }
        }
    }

    [Serializable]
    public class CurrentList<T> : List<T>
    {
    }
}
