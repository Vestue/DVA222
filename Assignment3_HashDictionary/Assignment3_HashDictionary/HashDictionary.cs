using System.Collections;

namespace Assignment3_HashDictionary
{
    internal class HashDictionary : IDictionary<int, int>, IDictionary<GeoLocation, string>
    {
        private List<KeyValuePair<int, int>>[] _htable = new List<KeyValuePair<int, int>>[10000];
        private List<KeyValuePair<GeoLocation, string>>[] _geoTable = new List<KeyValuePair<GeoLocation, string>>[10000];
        int _count;

        public HashDictionary()
        {
            for (int i = 0; i < _htable.Length; i++)
            {
                _htable[i] = new List<KeyValuePair<int, int>>();
                _geoTable[i] = new List<KeyValuePair<GeoLocation, string>>();
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

        public string this[GeoLocation key]
        {
            get
            {
                foreach(KeyValuePair<GeoLocation, string> kvp in _geoTable[GetHash(key)])
                    if (kvp.Key == key) return kvp.Value;
                return "";
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

        ICollection<GeoLocation> IDictionary<GeoLocation, string>.Keys
        {
            get
            {
                ICollection<GeoLocation> keys = new List<GeoLocation>();
                foreach(List<KeyValuePair<GeoLocation, string>> chain in _geoTable)
                foreach(KeyValuePair<GeoLocation, string> pair in chain)
                    keys.Add(pair.Key);
                return keys;
            }  
        }
        
        ICollection<string> IDictionary<GeoLocation, string>.Values
        {
            get
            {
                ICollection<string> values = new List<string>();
                foreach(List<KeyValuePair<GeoLocation, string>> chain in _geoTable)
                foreach(KeyValuePair<GeoLocation, string> pair in chain)
                    values.Add(pair.Value);
                return values;
            } 
        }

        public int Count => _count;

        public bool IsReadOnly => false;

        private int GetHash(object key) => key.GetHashCode() % _htable.Length;

        public void Add(int key, int value)
        {
            if (ContainsKey(key)) throw new ArgumentException("Key already exists in the table.");
            _count++;
            _htable[GetHash(key)].Add(new KeyValuePair<int, int>(key, value));
        }

        public void Add(KeyValuePair<int, int> item)
        {
            if (Contains(item)) throw new ArgumentException("Key already exists in the table.");
            _count++;
            _htable[GetHash(item.Key)].Add(new KeyValuePair<int, int>(item.Key, item.Value));
        }
        
        public void Add(GeoLocation key, string value)
        {
            if (ContainsKey(key)) throw new ArgumentException("Key already exists in the table.");
            _count++;
            _geoTable[GetHash(key)].Add(new KeyValuePair<GeoLocation, string>(key, value));
        }
        
        public void Add(KeyValuePair<GeoLocation, string> item)
        {
            if (Contains(item)) throw new ArgumentException("Key already exists in the table.");
            _count++;
            _geoTable[GetHash(item.Key)].Add(new KeyValuePair<GeoLocation, string>(item.Key, item.Value));
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
        
        public bool Contains(KeyValuePair<GeoLocation, string> item)
        {
            return _geoTable[GetHash(item.Key)].Contains(item);
        }

        public bool ContainsKey(int key)
        {
            foreach(KeyValuePair<int, int> item in _htable[GetHash(key)])
            {
                if (item.Key == key) return true;
            }
            return false;
        }
        public bool ContainsKey(GeoLocation key)
        {
            foreach(KeyValuePair<GeoLocation, string> item in _geoTable[GetHash(key)])
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
        }

        public void CopyTo(KeyValuePair<GeoLocation, string>[] array, int arrayIndex)
        {
            for(int i = arrayIndex; i < array.Length; i++)
            {
                foreach(List<KeyValuePair<GeoLocation, string>> chain in _geoTable)
                    foreach(KeyValuePair<GeoLocation, string> pair in chain)
                    {
                        if (i == array.Length) break;
                        array[i] = pair;
                        i++;
                    }
                if (i == _count) break;
            }
        }
        
        IEnumerator<KeyValuePair<GeoLocation, string>> IEnumerable<KeyValuePair<GeoLocation, string>>.GetEnumerator()
        {
            return new HashDictEnum(_geoTable);
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
        
        public bool Remove(GeoLocation key)
        {
            if (!ContainsKey(key)) return false;
            foreach( KeyValuePair<GeoLocation, string> item in _geoTable[GetHash(key)])
            {
                if (item.Key == key)
                {
                    _count--;
                    return _geoTable[GetHash(key)].Remove(item);
                }
            }
            return false;
        }
        
        public bool Remove(KeyValuePair<GeoLocation, string> item)
        {
            if (!Contains(item)) return false;
            if (_geoTable[GetHash(item.Key)].Remove(item))
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

        public bool TryGetValue(GeoLocation key, out string value)
        {
            if (!ContainsKey(key))
            {
                value = "";
                return false;
            }
            foreach (KeyValuePair<GeoLocation, string> item in _geoTable[GetHash(key)])
            {
                if (item.Key == key)
                {
                    value = item.Value;
                    return true;
                }
            }
            value = "";
            return false;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
