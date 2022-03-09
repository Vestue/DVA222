using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class HashDictEnum : IEnumerator<List<KeyValuePair<int, string>>>
    {
        public List<KeyValuePair<int, string>>[] _htable;
        int _position = -1;

        public HashDictEnum(List<KeyValuePair<int, string>>[] table)
        {
            _htable = table;
        }
        public List<KeyValuePair<int, string>> Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

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
            foreach(List<KeyValuePair<int, string>> chain in _htable)
                foreach(KeyValuePair<int, string> pair in chain)
                    count++;
            return count;
        }
    }
}
