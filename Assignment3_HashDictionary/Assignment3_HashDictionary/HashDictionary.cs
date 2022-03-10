using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class HashDictionary : IDictionary<int, int>
    {
        List<KeyValuePair<int, int>>[] _htable = new List<KeyValuePair<int, int>>[10000];

        int _count = 0;

        public int this[int key]
        {
            get
            {
                foreach(KeyValuePair<int, int> kvp in _htable[GetHash(key)])
                    if (kvp.Key == key) return kvp.Value;
                return default(int);
            }
            set
            {
                if(!ContainsKey(key)) Add(key, value);
            }
        }

        public ICollection<int> Keys
        {
            get
            {
                ICollection<int> keys = new List<int>();
                foreach (List<KeyValuePair<int, int>> chain in _htable)
                    foreach (KeyValuePair<int, int> pair in chain)
                        keys.Add(pair.Key);
                return keys;
            }
        }

        public ICollection<int> Values
        {
            get
            {
                ICollection<int> values = new List<int>();
                foreach(List<KeyValuePair<int, int>> chain in _htable)
                    foreach(KeyValuePair<int, int> pair in chain)
                        values.Add(pair.Value);
                return values;
            }
        }

        public int Count => _count;

        public bool IsReadOnly => false;

        private int GetHash(object key) => key.GetHashCode() % _htable.Length;

        public void Add(int key, int value)
        {
            if (ContainsKey(key)) return;
            _count++;
            _htable[GetHash(key)].Add(new KeyValuePair<int, int>(key, value));
        }

        public void Add(KeyValuePair<int, int> item)
        {
            if (Contains(item)) return;
            _count++;
            _htable[GetHash(item.Key)].Add(new KeyValuePair<int, int>(item.Key, item.Value));
        }

        public void Clear()
        {
            Array.Clear(_htable, 0, _htable.Length);
            _count = 0;
        }

        public bool Contains(KeyValuePair<int, int> item)
        {
            return _htable[GetHash(item.Key)].Contains(item);
        }

        public bool ContainsKey(int key)
        {
            if (key >= _htable.Length) return false;
            foreach(KeyValuePair<int, int> item in _htable[GetHash(key)])
            {
                if (item.Key == key) return true;
            }
            return false;
        }

        public void CopyTo(KeyValuePair<int, int>[] array, int arrayIndex)
        {
            for(int i = arrayIndex; i < array.Length; i++)
            {
                foreach(List<KeyValuePair<int, int>> chain in _htable)
                    foreach(KeyValuePair<int, int> pair in chain)
                    {
                        array[i] = pair;
                        i++;
                        if(i == array.Length) break;
                    }
            }
        }

        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            return new HashDictEnum(_htable);
        }

        public bool Remove(int key)
        {
            foreach( KeyValuePair<int, int> item in _htable[GetHash(key)])
            {
                if (item.Key == key)
                {
                    _count--;
                    return _htable[GetHash(key)].Remove(item);
                }
            }
            return false;
        }

        public bool Remove(KeyValuePair<int, int> item)
        {
            if (_htable[GetHash(item.Key)].Remove(item))
            {
                _count--;
                return true;
            }
            return false;
        }
        
        public bool TryGetValue(int key, [MaybeNullWhen(false)] out int value)
        {
            if (key >= _htable.Length)
            {
                value = default(int);
                return false;
            }
            foreach (KeyValuePair<int, int> item in _htable[GetHash(key)])
            {
                if (item.Key == key)
                {
                    value = item.Value;
                    return true;
                }
            }
            value = default(int);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }
}
