using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class HashDictEnum : IEnumerator<KeyValuePair<object, object>>
    {
        public List<KeyValuePair<object, object>>[] _htable;
        int _position = -1;

        public HashDictEnum(List<KeyValuePair<object, object>>[] table)
        {
            _htable = table;
        }
        public KeyValuePair<object, object> Current
        {
            get
            {
                int count = 0;
                KeyValuePair<object, object> current = new KeyValuePair<object, object>();
                foreach (List<KeyValuePair<object, object>> chain in _htable)
                {
                    foreach (KeyValuePair<object, object> pair in chain)
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
            foreach(List<KeyValuePair<object, object>> chain in _htable)
                foreach(KeyValuePair<object, object> pair in chain)
                    count++;
            return count;
        }
    }
}
