// See https://aka.ms/new-console-template for more information

using System;

namespace lektion2
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock clock1 = new Clock(12, 33, 54);
            Console.WriteLine(clock1.Read());
            clock1.Tick();
            Console.WriteLine(clock1.Read());
        }
    }

    class Clock
    {
        private int Hour, Minute, Second;

        public Clock(int hour, int minute, int second)
        {
            Set(hour, minute, second);
        }
        public void Tick()
        {
            Second++;
            if (Second >= 60)
            {
                Second = 0;
            }

            if (Minute >= 60)
            {
                Minute = 0;
            }

            if (Hour >= 60)
            {
                Hour = 0;
            }
        }

        int Truncate(int value, int lower, int upper)
        {
            if (value < lower) return lower;
            if (value > upper) return upper;
            return value;
        }

        public void Set(int hour, int minute, int second)
        {
            Hour = Truncate(hour, 0, 23);
            Minute = Truncate(minute, 0, 59);
            Second = Truncate(second, 0, 59);
        }

        public string Read()
        {
            return $"{Hour}:{Minute}:{Second}";
        }
    }
}

