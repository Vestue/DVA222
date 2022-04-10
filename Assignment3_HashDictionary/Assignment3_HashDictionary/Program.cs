using Assignment3_HashDictionary;
using HashtableTester;

IDictionary<int, int> d = new HashDictionary<int, int>();
//TestDriver.Instance.Run(d, 10000);

var t = new HashDictionary<GeoLocation, int>();
t.Add(new GeoLocation(1, 2), 1);
//t.Add(new GeoLocation(1, 2), 1);

var x = t[new GeoLocation(1, 2)];
Console.WriteLine(x);