using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class HashDictionary : IDictionary<object, string>
    {
        List<KeyValuePair<object, string>>[] _htable = new List<KeyValuePair<object, string>>[10000];

        int _count = 0;

        public string this[object key]
        {
            get
            {
                foreach(KeyValuePair<object, string> kvp in _htable[GetHash(key)])
                    if (kvp.Key == key) return kvp.Value;
                return null; // Maybe change this
            }
            set
            {
                if(!ContainsKey(key)) Add(key, value);
            }
        }

        public ICollection<object> Keys
        {
            get
            {
                ICollection<object> keys = new List<object>();
                foreach (List<KeyValuePair<object, string>> chain in _htable)
                    foreach (KeyValuePair<object, string> pair in chain)
                        keys.Add(pair.Key);
                return keys;
            }
        }

        public ICollection<string> Values
        {
            get
            {
                ICollection<string> values = new List<string>();
                foreach(List<KeyValuePair<object, string>> chain in _htable)
                    foreach(KeyValuePair<object, string> pair in chain)
                        values.Add(pair.Value);
                return values;
            }
        }

        public int Count => _count;

        public bool IsReadOnly => false;

        private int GetHash(object key) => key.GetHashCode() % _htable.Length;

        public void Add(object key, string value)
        {
            if (ContainsKey(key)) return;
            _count++;
            _htable[GetHash(key)].Add(new KeyValuePair<object, string>(key, value));
        }

        public void Add(KeyValuePair<object, string> item)
        {
            if (Contains(item)) return;
            _count++;
            _htable[GetHash(item.Key)].Add(new KeyValuePair<object, string>(item.Key, item.Value));
        }

        public void Clear()
        {
            Array.Clear(_htable, 0, _htable.Length);
            _count = 0;
        }

        public bool Contains(KeyValuePair<object, string> item)
        {
            return _htable[GetHash(item.Key)].Contains(item);
        }

        public bool ContainsKey(object key)
        {
            foreach(KeyValuePair<object, string> item in _htable[GetHash(key)])
            {
                if (item.Key == key) return true;
            }
            return false;
        }

        public void CopyTo(KeyValuePair<object, string>[] array, int arrayIndex)
        {
            for(int i = arrayIndex; i < array.Length; i++)
            {
                foreach(List<KeyValuePair<object, string>> chain in _htable)
                    foreach(KeyValuePair<object, string> pair in chain)
                    {
                        array[i] = pair;
                        i++;
                        if(i == array.Length) break;
                    }
            }
        }

        public IEnumerator<KeyValuePair<object, string>> GetEnumerator()
        {
            return new HashDictEnum(_htable);
        }

        public bool Remove(object key)
        {
            foreach( KeyValuePair<object, string> item in _htable[GetHash(key)])
            {
                if (item.Key == key)
                {
                    _count--;
                    return _htable[GetHash(key)].Remove(item);
                }
            }
            return false;
        }

        public bool Remove(KeyValuePair<object, string> item)
        {
            if (_htable[GetHash(item.Key)].Remove(item))
            {
                _count--;
                return true;
            }
            return false;
        }

        public bool TryGetValue(object key, [MaybeNullWhen(false)] out string value)
        {
            foreach (KeyValuePair<object, string> item in _htable[GetHash(key)])
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
