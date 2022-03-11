using System.Collections;

namespace Assignment3_HashDictionary
{
    internal class HashDictEnum : IEnumerator<KeyValuePair<int, int>>
    {
        private List<KeyValuePair<int, int>>[] _htable;
        int _position = - 1;

        public HashDictEnum(List<KeyValuePair<int, int>>[] table)
        {
            _htable = table;
        }
        public KeyValuePair<int, int> Current
        {
            get
            {
                int count = 0;
                KeyValuePair<int, int> current = new KeyValuePair<int, int>();
                foreach (List<KeyValuePair<int, int>> chain in _htable)
                {
                    foreach (KeyValuePair<int, int> pair in chain)
                    {
                        if (count == _position) break;
                        current = pair;
                        count++;
                    }
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
			// If this works I will cry
			if (_htable.Count() == 0) return true;
            _position++;
            return (_position < _htable.Count());
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
