using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class GenericHashDictEnum<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private List<KeyValuePair<TKey, TValue>>[] _hashTable;
       
        private int _nodeCount;
        private int _position = -1;
        public GenericHashDictEnum(List<KeyValuePair<TKey, TValue>>[] hashTable)
        {
            _hashTable = hashTable;
            _nodeCount = GetLength();
        }

        private int GetLength()
        {
            int count = 0;
            if (_hashTable != null)
            {
                foreach (List<KeyValuePair<TKey, TValue>> chain in _hashTable)
                    count += chain.Count;
            }
            return count;
        }

        public KeyValuePair<TKey, TValue> Current
        {
            get
            {
                if (_nodeCount == 0)
                {
                    throw new InvalidOperationException("Operation not possible.");
                }
                int count = 0;
                bool runLoop = true;
                KeyValuePair<TKey, TValue> tempPair = new KeyValuePair<TKey, TValue>();
                foreach (List<KeyValuePair<TKey, TValue>> chain in _hashTable)
                {
                    foreach (KeyValuePair<TKey, TValue> pair in chain)
                    {
                        if (count == _position)
                        {
                            tempPair = pair;
                            runLoop = false;
                            break;
                        }
                    }
                    count++;
                    if (runLoop == false) break;
                }
                return tempPair;
            }
        }

        object IEnumerator.Current => Current;

       public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _position++;
            return (_position < _nodeCount);
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
