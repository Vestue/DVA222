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

        private int GetIndex(TKey key) => key.GetHashCode() % _arraySize;

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => _nodeCount;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        private KeyValuePair<TKey, TValue> GetKey(TKey key)
        {
            if (key == null) return default(KeyValuePair<TKey, TValue>);
            foreach (KeyValuePair<TKey, TValue> pair in _hashTable[GetIndex(key)])
                if (key.Equals(pair.Key)) return pair;
            return default(KeyValuePair<TKey, TValue>);
        }

        private void SetKey(TKey key)
        {

        }

        public void Clear()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
