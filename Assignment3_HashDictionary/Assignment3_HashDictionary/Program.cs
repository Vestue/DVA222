using Assignment3_HashDictionary;
using HashtableTester;

IDictionary<int, int> d = new HashDictionary();
//TestDriver.Instance.Run(d, 10000);

d.Add(42424,44);
d.Add(25255, 52525);
IEnumerator<KeyValuePair<int, int>> enueemerator = d.GetEnumerator();
while (enueemerator.MoveNext())
{
    Console.Write(".");
}
KeyValuePair<int, int>[] arr = new KeyValuePair<int, int>[10000];
d.CopyTo(arr, 0);
foreach (var item in arr)
{
    Console.Write(item.ToString());
}