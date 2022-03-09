using System;
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
        List<KeyValuePair<int, string>>[] _htable = new List<KeyValuePair<int, string>>[10000];

        int _count = 0;

        public string this[int key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<int> Keys
        {
            get
            {
                ICollection<int> keys = new List<int>();
                foreach (List<KeyValuePair<int, string>> chain in _htable)
                    foreach (KeyValuePair<int, string> pair in chain)
                        keys.Add(pair.Key);
                return keys;
            }
        }

        public ICollection<string> Values
        {
            get
            {
                ICollection<string> values = new List<string>();
                foreach(List<KeyValuePair<int, string>> chain in _htable)
                    foreach(KeyValuePair<int, string> pair in chain)
                        values.Add(pair.Value);
                return values;
            }
        }

        public int Count => _count;

        public bool IsReadOnly => false;

        private int GetHash(int key) => key.GetHashCode() % _htable.Length;

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
            Array.Clear(_htable, 0, _htable.Length);
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
            for(int i = arrayIndex; i < array.Length; i++)
            {
                foreach(List<KeyValuePair<int, string>> chain in _htable)
                    foreach(KeyValuePair<int, string> pair in chain)
                    {
                        array[i] = pair;
                        i++;
                        if(i == array.Length) break;
                    }
            }
        }

        public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
        {
            return new HashDictEnum(_htable);
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
            return (IEnumerator) GetEnumerator();
        }
    }
}
