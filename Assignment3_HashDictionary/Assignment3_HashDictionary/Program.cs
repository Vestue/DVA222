using Assignment3_HashDictionary;
using HashtableTester;

IDictionary<int, int> d = new HashDictionary();
Random random = new Random();
TestDriver.Instance.Run(d, 10000);

/*
for (int i = 0; i < 5; i++)
{
    d.Add(random.Next(1, 500000), random.Next(4242, 4000000));
}


// MoveNext borde bli false om count går över mängden i _htable_count, det som fås av length()

KeyValuePair<int, int> testPair = new KeyValuePair<int, int>(); 
IEnumerator<KeyValuePair<int, int>> enueemerator = d.GetEnumerator();
while (enueemerator.MoveNext())
{
    testPair = enueemerator.Current;
    Console.WriteLine(testPair.ToString());
    Console.Write(".");
}



KeyValuePair<int, int>[] arr = new KeyValuePair<int, int>[10];
d.CopyTo(arr, 0);


foreach (var item in arr)
{
    Console.Write(item.ToString());
}
*/