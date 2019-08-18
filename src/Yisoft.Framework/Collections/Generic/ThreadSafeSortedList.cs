// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Yisoft.Framework.Collections.Generic
{
    public class ThreadSafeSortedList<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly SortedList<TKey, TValue> _items = new SortedList<TKey, TValue>();
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        public bool TryGetValue(TKey key, out TValue value) { return _items.TryGetValue(key, out value); }

        public TValue this[TKey key]
        {
            get
            {
                _lock.EnterReadLock();

                try
                {
                    return _items.ContainsKey(key) ? _items[key] : default;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }

            set
            {
                _lock.EnterWriteLock();

                try
                {
                    _items[key] = value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                _lock.EnterWriteLock();

                try
                {
                    return _items.Keys;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                _lock.EnterWriteLock();

                try
                {
                    return _items.Values;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item) { this[item.Key] = item.Value; }

        public void Clear()
        {
            _lock.EnterReadLock();

            try
            {
                _items.Clear();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            _lock.EnterReadLock();

            try
            {
                return _items.ContainsKey(item.Key);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { throw new NotImplementedException(); }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            _lock.EnterReadLock();

            try
            {
                return _items.Remove(item.Key);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int Count
        {
            get
            {
                _lock.EnterReadLock();

                try
                {
                    return _items.Count;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
        }

        public bool IsReadOnly => false;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            _lock.EnterReadLock();

            try
            {
                return _items.GetEnumerator();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public bool ContainsKey(TKey key)
        {
            _lock.EnterReadLock();

            try
            {
                return _items.ContainsKey(key);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Add(TKey key, TValue value) { this[key] = value; }

        public bool Remove(TKey key)
        {
            _lock.EnterWriteLock();

            try
            {
                if (!_items.ContainsKey(key)) return false;

                _items.Remove(key);

                return true;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool ContainsValue(TValue item)
        {
            _lock.EnterReadLock();

            try
            {
                return _items.ContainsValue(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public List<TValue> GetAllItems()
        {
            _lock.EnterReadLock();

            try
            {
                return new List<TValue>(_items.Values);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void ClearAll()
        {
            _lock.EnterWriteLock();

            try
            {
                _items.Clear();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public List<TValue> GetAndClearAllItems()
        {
            _lock.EnterWriteLock();

            try
            {
                var list = new List<TValue>(_items.Values);

                _items.Clear();

                return list;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}