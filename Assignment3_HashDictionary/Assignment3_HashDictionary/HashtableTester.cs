using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace HashtableTester
{

    // if d contains an instance of your dictionary class you can test it using
    //   HashtableTester.TestDriver.Instance.Run(d, 10000);

    // ---

    class TestDriver
    {
        Random Random = new Random();
        Type[] Operations = new Type[]
        {
            typeof(AddOperation),
            typeof(SetOperation),
            typeof(ContainsKeyOperation),
            typeof(RemoveOperation),
            typeof(TryGetValueOperation),
            typeof(GetEnumeratorOperation),
            typeof(CountOperation)
        };

        static TestDriver instance;
        public static TestDriver Instance
        {
            get { if (instance == null) { instance = new TestDriver(); } return instance; }
        }

        protected TestDriver() { }

        public void Run(IDictionary<int, int> testee, int iterations = 1000, bool reportAll = true)
        {
            var model = new Dictionary<int, int>();

            for (int i = 0; i < iterations; i++)
            {
                Type operationType = Operations[Random.Next(Operations.Length)];
                Operation operation = (Operation)Activator.CreateInstance(operationType);



                var modelResult = operation.Run(model);
                var testeeResult = operation.Run(testee);

                if (reportAll)
                {
                    Console.WriteLine($"{operation} {testeeResult}");
                }
                else
                {
                    Console.Write(".");
                }

                if (!testeeResult.Equals(modelResult))
                {
                    ReportFailure(testeeResult, modelResult);
                    return;
                }

            }
            
        }

        void ReportFailure(OperationResult testeeResult, OperationResult modelResult)
        {
            Console.WriteLine("Test failed!");
            Console.WriteLine($"Testee returned {testeeResult}");
            Console.WriteLine($"Model returned {modelResult}");
        }
    }

    // --

    interface OperationResult
    {
    }

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()

    // --

    class OperationResultSequence : OperationResult
    {
        List<OperationResult> Results;

        public OperationResultSequence(List<OperationResult> results)
        {
            Results = results;
        }

        public override bool Equals(Object other)
        {
            if (!(other is OperationResultSequence))
            {
                return false;
            }

            return Results.SequenceEqual<OperationResult>( ((OperationResultSequence)other).Results );
        }

        public override string ToString()
        {
            return Results.Aggregate("", (acc, result) => $"{acc} {result}");
        }
    }

    // --

    class VoidOperationResult : OperationResult
    {
        public override bool Equals(Object other)
        {
            return other is VoidOperationResult;
        }

        public override string ToString()
        {
            return "void";
        }
    }

    // --

    class BoolOperationResult : OperationResult
    {
        bool Value;

        public BoolOperationResult(bool value)
        {
            Value = value;
        }

        public override bool Equals(Object other)
        {
            if (!(other is BoolOperationResult))
            {
                return false;
            }

            return Value == ((BoolOperationResult)other).Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    // --

    class IntOperationResult : OperationResult
    {
        int Value;

        public IntOperationResult(int value)
        {
            Value = value;
        }

        public override bool Equals(Object other)
        {
            if (!(other is IntOperationResult))
            {
                return false;
            }

            return Value == ((IntOperationResult)other).Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    // --

    class ExceptionOperatioResult : OperationResult
    {
        Type Type;

        public ExceptionOperatioResult(Exception exception)
        {
            Type = exception.GetType();
        }

        public override bool Equals(Object other)
        {
            if (!(other is ExceptionOperatioResult))
            {
                return false;
            }

            return Type.ToString().Equals(((ExceptionOperatioResult)other).ToString());
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }

    // --

    class EnumeratorOperationResult : OperationResult
    {
        IEnumerator Enumerator;
        HashSet<object> Content;
        public EnumeratorOperationResult(IEnumerator enumerator)
        {
            Enumerator = enumerator;
            Content = new HashSet<object>();
            while (Enumerator.MoveNext())
            {
                Console.Write(".");
                Content.Add(Enumerator.Current);
            }
        }

        // other is the model, i.e., System.Collection
        public override bool Equals(Object other)
        {
            if (!(other is EnumeratorOperationResult))
            {
                return false;
            }

            var otherOperation = (EnumeratorOperationResult)other;

            var result = Content.All((arg) => otherOperation.Content.Contains(arg)) && otherOperation.Content.All((arg) => Content.Contains(arg));
            return result;
        }

        public override string ToString()
        {
            return $"[ {String.Join(",", Content)} ]";
        }
    }

#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()

    // ---


    delegate T OperationDelegate<T>();
    delegate void VoidOperationDelegate();

    // --

    abstract class Operation
    {
        protected static Random Random = new Random();
        protected static List<int> Keys = new List<int>();

        public abstract OperationResult Run(IDictionary<int, int> d);

        public static OperationResult Lift(OperationDelegate<bool> d)
        {
            try
            {
                var b = d();
                return new BoolOperationResult(b);
            } catch (Exception e)
            {
                return new ExceptionOperatioResult(e);
            }
        }

        public static OperationResult Lift(OperationDelegate<int> d)
        {
            try
            {
                var b = d();
                return new IntOperationResult(b);
            }
            catch (Exception e)
            {
                return new ExceptionOperatioResult(e);
            }
        }

        public static OperationResult Lift(OperationDelegate<IEnumerator> d)
        {
            try
            {
                var b = d();
                return new EnumeratorOperationResult(b);
            }
            catch (Exception e)
            {
                return new ExceptionOperatioResult(e);
            }
        }

        public static OperationResult Lift(VoidOperationDelegate d)
        {
            try
            {
                d();
                return new VoidOperationResult();
            }
            catch (Exception e)
            {
                return new ExceptionOperatioResult(e);
            }
        }
    }

    // --

    class AddOperation : Operation
    {
        int Key, Value;

        public AddOperation()
        {
            if (Random.Next(2) == 1 || Keys.Count == 0)
            {
                Key = Random.Next();
                Keys.Add(Key);
            }
            else
            {
                Key = Keys[Random.Next(Keys.Count)];
            }

            Value = Random.Next();
        }

        public override OperationResult Run(IDictionary<int, int> d)
        {
            return Lift(() => d.Add(Key, Value));
        }

        public override string ToString()
        {
            return $"Add({Key}, {Value})";
        }

    }

    // --

    class SetOperation : Operation
    {
        int Key, Value;

        public SetOperation()
        {
            if (Random.Next(2) == 1 || Keys.Count == 0)
            {
                Key = Random.Next();
                Keys.Add(Key);
            }
            else
            {
                Key = Keys[Random.Next(Keys.Count)];
            }

            Value = Random.Next();
        }

        public override OperationResult Run(IDictionary<int, int> d)
        {
            return Lift(() => { d[Key] = Value; });
        }

        public override string ToString()
        {
            return $"d[{Key}] = {Value}";
        }

    }

    // --

    class ClearOperation : Operation
    {

        public override OperationResult Run(IDictionary<int, int> d)
        {
            Keys = new List<int>();
            return Lift(() => d.Clear());
        }

        public override string ToString()
        {
            return $"Clear";
        }
    }

    // --

    class ContainsKeyOperation : Operation
    {
        int Key;

        public ContainsKeyOperation()
        {
            if (Random.Next(2) == 1 || Keys.Count == 0)
            {
                Key = Random.Next();
            }
            else
            {
                Key = Keys[Random.Next(Keys.Count)];
            }
        }

        public override OperationResult Run(IDictionary<int, int> d)
        {
            return Lift(() => d.ContainsKey(Key));
        }

        public override string ToString()
        {
            return $"ContainsKey({Key})";
        }

    }

    // --

    class RemoveOperation : Operation
    {
        int Key;

        public RemoveOperation()
        {
            if (Random.Next(2) == 1 || Keys.Count == 0)
            {
                Key = Random.Next();
            }
            else
            {
                Key = Keys[Random.Next(Keys.Count)];
            }
        }

        public override OperationResult Run(IDictionary<int, int> d)
        {
            Keys.Remove(Key);
            return Lift(() => d.Remove(Key));
        }

        public override string ToString()
        {
            return $"Remove({Key})";
        }

    }

    // --

    class TryGetValueOperation : Operation
    {
        int Key;

        public TryGetValueOperation()
        {
            if (Random.Next(2) == 1 || Keys.Count == 0)
            {
                Key = Random.Next();
            }
            else
            {
                Key = Keys[Random.Next(Keys.Count)];
            }
        }

        public override OperationResult Run(IDictionary<int, int> d)
        {
            int value = -1;
            var result = Lift(() => d.TryGetValue(Key, out value));

            return new OperationResultSequence(new List<OperationResult>() { result, new IntOperationResult(value) });
        }

        public override string ToString()
        {
            return $"TryGetValue({Key}, out value)";
        }

    }

    // --

    class GetEnumeratorOperation : Operation
    {

        public override OperationResult Run(IDictionary<int, int> d)
        {
            return Lift(() => d.GetEnumerator());
        }

        public override string ToString()
        {
            return $"GetEnumerator()";
        }

    }

    // --

    class CountOperation : Operation
    {

        public override OperationResult Run(IDictionary<int, int> d)
        {
            return Lift(() => d.Count);
        }

        public override string ToString()
        {
            return $"Count";
        }

    }
}
