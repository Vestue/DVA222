using Assignment3_HashDictionary;
using HashtableTester;

//IDictionary<int, int> d = new HashDictionary<int, int>();
//TestDriver.Instance.Run(d, 10000);

IDictionary<GeoLocation, int> test = new HashDictionary<GeoLocation, int>();
test.Add(new GeoLocation(1, 2), 1);
var x = test[new GeoLocation(1, 2)];
Console.WriteLine(x);
Console.WriteLine((test.ContainsKey(new GeoLocation(1, 2))));
Console.WriteLine(test.Contains(new KeyValuePair<GeoLocation, int>(new GeoLocation(1, 2), 1)));