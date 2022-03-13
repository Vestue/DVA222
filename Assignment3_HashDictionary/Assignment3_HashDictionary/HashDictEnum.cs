using System.Collections;

namespace Assignment3_HashDictionary
{
    internal class HashDictEnum : IEnumerator<KeyValuePair<int, int>>
    {
        private List<KeyValuePair<int, int>>[] _htable;
        int _position = -1;
        int _count;

        public HashDictEnum(List<KeyValuePair<int, int>>[] table)
        {
            _htable = table;
            _count = Length();
        }
        public KeyValuePair<int, int> Current
        {
            get
            {
                // Current is undefined when it's not within range.
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
			//if (_position >= _count) throw new InvalidOperationException();

			//if (_htable.Count() == 0) return true;
            _position++;
            return (_position < _count);
        }

        public void Reset()
        {
            //if (_position >= _count) throw new InvalidOperationException();
            _position = -1;
        }

        private int Length()
        {
            int count = 0;
            foreach (List<KeyValuePair<int, int>> chain in _htable)
                count += chain.Count;
            return count;
        }
    }
}
