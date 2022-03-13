using System.Collections;

namespace Assignment3_HashDictionary
{
    internal class HashDictionary : IDictionary<int, int>
    {
        private List<KeyValuePair<int, int>>[] _htable = new List<KeyValuePair<int, int>>[10];

        int _count;

        public HashDictionary()
        {
            for (int i = 0; i < _htable.Length; i++)
            {
                _htable[i] = new List<KeyValuePair<int, int>>();
            }
        }

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
            if (ContainsKey(key)) return;//throw new ArgumentException("Key already exists in the table.");
            _count++;
            _htable[GetHash(key)].Add(new KeyValuePair<int, int>(key, value));
        }

        public void Add(KeyValuePair<int, int> item)
        {
            if (Contains(item)) throw new ArgumentException("Key already exists in the table.");
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
                        if (i == array.Length) break;
                        array[i] = pair;
                        i++;
                    }
                if (i == _count) break;
            }
            PrintTable();
        }

        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            return new HashDictEnum(_htable);
        }

        public bool Remove(int key)
        {
            if (!ContainsKey(key)) return false;
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
            if (!Contains(item)) return false;
            if (_htable[GetHash(item.Key)].Remove(item))
            {
                _count--;
                return true;
            }
            return false;
        }
        
        public bool TryGetValue(int key,  out int value)
        {
            if (!ContainsKey(key))
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
            return GetEnumerator();
        }

        public void PrintTable()
        {
            foreach(var chain in _htable)
                foreach (var kvp in chain)
                {
                    Console.WriteLine($" [ {kvp.Key.ToString()}, {kvp.Value.ToString()} ] ");
                }
        }
    }
}
