// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Collections;
using System.Collections.Generic;

namespace Yisoft.Framework.Collections.Generic
{
    public class BidirectionalDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IEqualityComparer<TKey> _keyComparer;
        private readonly Dictionary<TKey, TValue> _keysToValues;
        private readonly IEqualityComparer<TValue> _valueComparer;
        private readonly Dictionary<TValue, TKey> _valuesToKeys;

        public BidirectionalDictionary() : this(10, null, null) { }

        public BidirectionalDictionary(int capacity) : this(capacity, null, null) { }

        public BidirectionalDictionary(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
            : this(10, keyComparer, valueComparer)
        {
        }

        public BidirectionalDictionary(int capacity, IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "capacity cannot be less than 0");

            _keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
            _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;

            _keysToValues = new Dictionary<TKey, TValue>(capacity, _keyComparer);
            _valuesToKeys = new Dictionary<TValue, TKey>(capacity, _valueComparer);

            Inverse = new BidirectionalDictionary<TValue, TKey>(this);
        }

        private BidirectionalDictionary(BidirectionalDictionary<TValue, TKey> inverse)
        {
            Inverse = inverse ?? throw new ArgumentNullException(nameof(inverse));
            _keyComparer = inverse._valueComparer;
            _valueComparer = inverse._keyComparer;
            _valuesToKeys = inverse._keysToValues;
            _keysToValues = inverse._valuesToKeys;
        }

        public BidirectionalDictionary<TValue, TKey> Inverse { get; }

        public ICollection<TKey> Keys => _keysToValues.Keys;

        public ICollection<TValue> Values => _keysToValues.Values;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() { return _keysToValues.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>) _keysToValues).CopyTo(array, arrayIndex);
        }

        public bool ContainsKey(TKey key)
        {
            return key == null
                ? throw new ArgumentNullException(nameof(key))
                : _keysToValues.ContainsKey(key);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>) _keysToValues).Contains(item);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return key == null
                ? throw new ArgumentNullException(nameof(key))
                : _keysToValues.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get => _keysToValues[key];
            set
            {
                if (key == null) throw new ArgumentNullException(nameof(key));
                if (value == null) throw new ArgumentNullException(nameof(value));

                if (_ValueBelongsToOtherKey(key, value)) throw new ArgumentException("Value already exists", nameof(value));


                if (_keysToValues.TryGetValue(key, out var oldValue))
                {
                    var oldKey = _valuesToKeys[oldValue];

                    _keysToValues[oldKey] = value;
                    _valuesToKeys.Remove(oldValue);
                    _valuesToKeys[value] = oldKey;
                }
                else
                {
                    _keysToValues[key] = value;
                    _valuesToKeys[value] = key;
                }
            }
        }

        public int Count => _keysToValues.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (_keysToValues.ContainsKey(key)) throw new ArgumentException("Key already exists", nameof(key));
            if (_valuesToKeys.ContainsKey(value)) throw new ArgumentException("Value already exists", nameof(value));

            _keysToValues.Add(key, value);
            _valuesToKeys.Add(value, key);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) { Add(item.Key, item.Value); }

        public bool Remove(TKey key)
        {
            if (key == null) throw new ArgumentNullException("ke" + "y");

            if (!_keysToValues.TryGetValue(key, out var value)) return false;

            _keysToValues.Remove(key);
            _valuesToKeys.Remove(value);

            return true;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            var removed = ((ICollection<KeyValuePair<TKey, TValue>>) _keysToValues).Remove(item);

            if (removed) _valuesToKeys.Remove(item.Value);

            return removed;
        }

        public void Clear()
        {
            _keysToValues.Clear();
            _valuesToKeys.Clear();
        }

        public TValue GetValue(TKey key) { return TryGetValue(key, out var value) ? value : default(TValue); }

        public bool ContainsValue(TValue value)
        {
            return value == null
                ? throw new ArgumentNullException(nameof(value))
                : _valuesToKeys.ContainsKey(value);
        }

        public TKey GetKey(TValue value) { return TryGetKey(value, out var key) ? key : default(TKey); }

        public bool TryGetKey(TValue value, out TKey key)
        {
            return value == null
                ? throw new ArgumentNullException(nameof(value))
                : _valuesToKeys.TryGetValue(value, out key);
        }

        public void Replace(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            Remove(key);

            Inverse.Remove(value);

            Add(key, value);
        }

        private bool _ValueBelongsToOtherKey(TKey key, TValue value)
        {
            return _valuesToKeys.TryGetValue(value, out var otherKey) && !_keyComparer.Equals(key, otherKey);
        }
    }
}