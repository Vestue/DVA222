﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class HashDictionary : IDictionary<int, string>
    {
        readonly int _tableSize = 10000; 
        List<KeyValuePair<int, string>>[] _htable = new List<KeyValuePair<int, string>>[10000];

        // Teststuff
        ICollection<List<KeyValuePair<int, string>>> _table;

        int _count = 0;

        public string this[int key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<int> Keys => throw new NotImplementedException();

        public ICollection<string> Values => throw new NotImplementedException();

        public int Count => _count;

        public bool IsReadOnly => false;

        private int GetHash(int key) => key.GetHashCode() % _tableSize;

        public void Add(int key, string value)
        {
            if (ContainsKey(key)) return;
            _count++;
            _htable[GetHash(key)].Add(new KeyValuePair<int, string>(key, value));
        }

        public void Add(KeyValuePair<int, string> item)
        {
            if (Contains(item)) return;
            _count++;
            _htable[GetHash(item.Key)].Add(new KeyValuePair<int, string>(item.Key, item.Value));
        }

        public void Clear()
        {
            Array.Clear(_htable, 0, _tableSize);
            _count = 0;
        }

        public bool Contains(KeyValuePair<int, string> item)
        {
            return _htable[GetHash(item.Key)].Contains(item);
        }

        public bool ContainsKey(int key)
        {
            foreach(KeyValuePair<int, string> item in _htable[GetHash(key)])
            {
                if (item.Key == key) return true;
            }
            return false;
        }

        public void CopyTo(KeyValuePair<int, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int key)
        {
            foreach( KeyValuePair<int, string> item in _htable[GetHash(key)])
            {
                if (item.Key == key)
                {
                    _count--;
                    return _htable[GetHash(key)].Remove(item);
                }
            }
            return false;
        }

        public bool Remove(KeyValuePair<int, string> item)
        {
            if (_htable[GetHash(item.Key)].Remove(item))
            {
                _count--;
                return true;
            }
            return false;
        }

        public bool TryGetValue(int key, [MaybeNullWhen(false)] out string value)
        {
            foreach (KeyValuePair<int, string> item in _htable[GetHash(key)])
            {
                if (item.Key == key)
                {
                    value = item.Value;
                    return true;
                }
            }
            value = null;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
