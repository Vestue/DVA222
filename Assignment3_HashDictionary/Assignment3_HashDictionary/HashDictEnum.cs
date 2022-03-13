using System.Collections;

namespace Assignment3_HashDictionary
{
    internal class HashDictEnum : IEnumerator<KeyValuePair<int, int>>, IEnumerator<KeyValuePair<GeoLocation, string>>
    {
        List<KeyValuePair<int, int>>[]? _htable;
        List<KeyValuePair<GeoLocation, string>>[]? _geoTable;
        int _position = -1;
        int _count;

        public HashDictEnum(List<KeyValuePair<int, int>>[] table)
        {
            _htable = table;
            _count = Length();
        }

        public HashDictEnum(List<KeyValuePair<GeoLocation, string>>[] geoTable)
        {
            _geoTable = geoTable;
            _count = LengthGeo();
        }
        
        public KeyValuePair<int, int> Current
        {
            get
            {
                if (_count == 0)
                {
                    throw new InvalidOperationException("Operation not possible");
                }
                int count = 0;
                bool runLoop = true;
                KeyValuePair<int, int> current = new KeyValuePair<int, int>();
                foreach (List<KeyValuePair<int, int>> chain in _htable)
                {
                    foreach (KeyValuePair<int, int> pair in chain)
                    {
                        if (count == _position)
                        {
                            current = pair;
                            runLoop = false;
                            break;
                        }
                        count++;
                    }
                    if (runLoop == false) break;
                }
                return current;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _position++;
            return (_position < _count);
        }

        public void Reset()
        {
            _position = -1;
        }

        KeyValuePair<GeoLocation, string> IEnumerator<KeyValuePair<GeoLocation, string>>.Current
        {
            get
            {
                if (_count == 0)
                {
                    throw new InvalidOperationException("Operation not possible");
                }
                int count = 0;
                bool runLoop = true;
                KeyValuePair<GeoLocation, string> current = new KeyValuePair<GeoLocation, string>();
                foreach (List<KeyValuePair<GeoLocation, string>> chain in _geoTable)
                {
                    foreach (KeyValuePair<GeoLocation, string> pair in chain)
                    {
                        if (count == _position)
                        {
                            current = pair;
                            runLoop = false;
                            break;
                        }
                        count++;
                    }
                    if (runLoop == false) break;
                }
                return current;
            }
        }

        private int Length()
        {
            int count = 0;
            foreach (List<KeyValuePair<int, int>> chain in _htable)
                count += chain.Count;
            return count;
        }
        private int LengthGeo()
        {
            int count = 0; 
            foreach (List<KeyValuePair<GeoLocation, string>> chain in _geoTable)
                count += chain.Count;
            return count;
        }
    }
}
