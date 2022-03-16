using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class GenericHashDictionary<Tkey, TValue> : IDictionary<Tkey, TValue>
    {
        private static readonly int _arraySize = 10000;
        private List<KeyValuePair<Tkey, TValue>>[] _hashTable = new List<KeyValuePair<Tkey, TValue>>[_arraySize];
        private int _nodeCount;

        public GenericHashDictionary()
        {
            for (int i = 0; i < _arraySize; i++)
                _hashTable[i] = new List<KeyValuePair<Tkey, TValue>>();
        }

        // Implementera metoder från föreläsnings-instruktioner
        // private List<KeyValuePair<K, V>> Find(K key), kan returnera null om ingen nyckel hittas
        // uint GetIndex(K key)

        public TValue this[Tkey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<Tkey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Tkey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<Tkey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<Tkey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(Tkey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<Tkey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<Tkey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Tkey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<Tkey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(Tkey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
