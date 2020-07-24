using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OpaqueId
{
    internal class TargetBasePlaceHolderCollection : ICollection<char>, IEnumerable<char>
    {
        public TargetBasePlaceHolderCollection()
        {
            _placeHolders = new List<char>();
        }
        public TargetBasePlaceHolderCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _placeHolders = new List<char>(capacity);
        }
        public TargetBasePlaceHolderCollection(IEnumerable<char> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var items = collection as ICollection<char>;
            if (items != null)
            {
                _placeHolders = new List<char>(items);
            }
            else
            {
                _placeHolders = new List<char>();
                using (var iterator = collection.GetEnumerator())
                {
                    while (iterator.MoveNext())
                    {
                        Add(iterator.Current);
                    }
                }
            }
        }

        private IList<char> _placeHolders = null;

        public char this[int index] => _placeHolders[index];

        public int Count => _placeHolders.Count;

        public bool IsReadOnly => _placeHolders.IsReadOnly;

        public void Add(char item)
        {
            _placeHolders.Add(item);
        }

        public void Clear()
        {
            _placeHolders.Clear();
        }

        public bool Contains(char item)
        {
            return _placeHolders.Contains(item);
        }

        public void CopyTo(char[] array, int arrayIndex)
        {
            _placeHolders.CopyTo(array, arrayIndex);
        }

        public IEnumerator<char> GetEnumerator()
        {
            return _placeHolders.GetEnumerator();
        }

        public bool Remove(char item)
        {
            return _placeHolders.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _placeHolders.GetEnumerator();
        }

        public char[] ToArray()
        {
            return _placeHolders.Select(p => p).ToArray();
        }

        public override string ToString()
        {
            return new string(ToArray());
        }
    }
}
