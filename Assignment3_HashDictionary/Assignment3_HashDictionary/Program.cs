using Assignment3_HashDictionary;
using HashtableTester;

IDictionary<int, int> d = new GenericHashDictionary<int, int>();
TestDriver.Instance.Run(d, 10000);