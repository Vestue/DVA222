using Assignment3_HashDictionary;
using HashtableTester;

IDictionary<object, object> d = new HashDictionary();
TestDriver.Instance.Run((IDictionary<int, int>)d, 10000);