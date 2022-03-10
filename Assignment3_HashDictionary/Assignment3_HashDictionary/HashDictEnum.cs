using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class HashDictEnum : IEnumerator<KeyValuePair<int, int>>
    {
        public List<KeyValuePair<int, int>>[] _htable;
        int _position = -1;

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
                        current = pair;
                        if (count == _position) break;
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
            _position++;
            return (_position < _htable.Count());
        }

        public void Reset()
        {
            _position = -1;
        }

        // Might be useful later. Remove if not
        private int Length()
        {
            int count = 0;
            foreach(List<KeyValuePair<int, int>> chain in _htable)
                foreach(KeyValuePair<int, int> pair in chain)
                    count++;
            return count;
        }
    }
}
