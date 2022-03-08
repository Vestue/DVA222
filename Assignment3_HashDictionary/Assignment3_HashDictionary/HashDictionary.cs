using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class HashDictionary : IDictionary<int, string>
    {
        // En array av länkade listor av KeyValuePairs?
        int _tableSize = 10000; // Useless atm
        LinkedList<KeyValuePair<int, string>>[] _htable = new LinkedList<KeyValuePair<int, string>>[10000];

        int count;

        public string this[int key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<int> Keys => throw new NotImplementedException();

        public ICollection<string> Values => throw new NotImplementedException();

        public int Count => count;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(int key, string value)
        {
            count++;
        }

        public void Add(KeyValuePair<int, string> item)
        {
            count++;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<int, string> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(int key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<int, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int key)
        {
            // if found return true and count--

            return false;
        }

        public bool Remove(KeyValuePair<int, string> item)
        {
            // if found return true and count--

            return false;
        }

        public bool TryGetValue(int key, [MaybeNullWhen(false)] out string value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
