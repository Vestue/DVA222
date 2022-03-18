using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class GenericHashDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private static readonly int _arraySize = 10000;
        private List<KeyValuePair<TKey, TValue>>[] _hashTable = new List<KeyValuePair<TKey, TValue>>[_arraySize];
        private int _nodeCount;

        public GenericHashDictionary()
        {
            for (int i = 0; i < _arraySize; i++)
                _hashTable[i] = new List<KeyValuePair<TKey, TValue>>();
        }

        // Implementera metoder från föreläsnings-instruktioner
        // private List<KeyValuePair<K, V>> Find(K key), kan returnera null om ingen nyckel hittas
        // uint GetIndex(K key)

        private int GetIndex(TKey key)
        {
            if (key == null) return -1;
            return key.GetHashCode() % _arraySize;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key != null)
                {
                    foreach (KeyValuePair<TKey, TValue> pair in _hashTable[GetIndex(key)])
                        if (key.Equals(pair.Value)) return pair.Value;
                }
                return default(TValue)!;
            }
            set
            {
                if (ContainsKey(key))
                {
                    Remove(key);
                    Add(key, value);
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                ICollection<TKey> keys = new List<TKey>();
                foreach(List<KeyValuePair<TKey, TValue>> chain in _hashTable)
                    foreach (KeyValuePair<TKey, TValue> pair in chain)
                        keys.Add(pair.Key);
                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                ICollection<TValue> values = new List<TValue>();
                foreach(List<KeyValuePair<TKey, TValue>> chain in _hashTable)
                    foreach(KeyValuePair<TKey, TValue> pair in chain)
                        values.Add(pair.Value);
                return values;
            }
        }

        public int Count => _nodeCount;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key)) throw new ArgumentException("Key already exists in the hash dictionary.");
            _nodeCount++;
            _hashTable[GetIndex(key)].Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item)) throw new ArgumentException("Key already exists in hash dictionary.");
            _nodeCount++;
            _hashTable[GetIndex(item.Key)].Add(new KeyValuePair<TKey, TValue>(item.Key, item.Value));
        }

        private KeyValuePair<TKey, TValue> FindKey(TKey key)
        {
            if (key == null) return default(KeyValuePair<TKey, TValue>);
            foreach (KeyValuePair<TKey, TValue> pair in _hashTable[GetIndex(key)])
                if (key.Equals(pair.Key)) return pair;
            return default(KeyValuePair<TKey, TValue>);
        }

        public void Clear()
        {
            Array.Clear(_hashTable, 0, _hashTable.Length);
            _nodeCount = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _hashTable[GetIndex(item.Key)].Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            if (key != null)
                foreach(KeyValuePair<TKey, TValue> keyValuePair in _hashTable[GetIndex(key)])
                    if (key.Equals(keyValuePair.Key)) return true;
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < array.Length; i++)
            {
                foreach(List<KeyValuePair<TKey, TValue>> chain in _hashTable)
                    foreach(KeyValuePair<TKey, TValue> pair in chain)
                    {
                        if (i == array.Length) break;
                        array[i] = pair;
                        i++;
                    }
                if (i == _nodeCount) break;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new GenericHashDictEnum<TKey, TValue>(_hashTable);
        }

        public bool Remove(TKey key)
        {
            if (key == null) return false;
            foreach(KeyValuePair<TKey, TValue> pair in _hashTable[GetIndex(key)])
            {
                if (key.Equals(pair.Key))
                {
                    _nodeCount--;
                    return _hashTable[GetIndex(key)].Remove(pair);
                }
            }
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null) return false;
            if (_hashTable[GetIndex(item.Key)].Remove(item))
            {
                _nodeCount--;
                return true;
            }
            return false;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key == null)
            {
                value = default(TValue);
                return false;
            }
            foreach(KeyValuePair<TKey, TValue> pair in _hashTable[GetIndex(key)])
            {
                if (key.Equals(pair.Key))
                {
                    value = pair.Value;
                    return true;
                }
            }
            value = default(TValue);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
