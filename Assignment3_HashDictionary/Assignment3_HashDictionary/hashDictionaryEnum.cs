public class HashDictEnum : IEnumerator
{
	public list<KeyValuePair<int, string>>[] _htable;
	
	int _position = -1;
	
	public HashDictEnum(list<KeyValuePair<int, string>>[] table) => _htable = table;
	
	public bool MoveNext()
	{
		// Måste på nåt sätt även hantera att det går att röra sig sidleds
		// i de länkade listorna.
		_position++;
		return (_position < _htable.Length);
	}
	
	public void Reset() => _position = -1;
	
	object IEnumerator.Current { get Current; }
	
	public KeyValuePair Current
	{
		get
		{
			try
			{
				return _htable[position];
			}
			catch (IndexOutOfRangeException)
			{
				throw new InvalidOperationException();
			}
		}
	}
}