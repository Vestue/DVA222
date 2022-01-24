using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_clock
{
    using System.Timers;
    class Clock
    {
        int Hour, Minute, Second;
        Timer Timer;
        ConsoleDisplay display = new ConsoleDisplay();

        public Clock(int hour, int minute, int second)
        {   
            Set(hour, minute, second);
            Timer = new Timer();
            Timer.Interval = 1000;
            Timer.AutoReset = true;
            Timer.Elapsed += Tick;
        }

        private void Tick(object? sender, ElapsedEventArgs e)
        {
            Second++;
            if (Second >= 60)
            {
                Second = 0;
                Minute++;
            }

            if (Minute >= 60)
            {
                Minute = 0;
                Hour++;
            }

            if (Hour >= 24)
            {
                Hour = 0;
            }

            display.Display(Hour, Minute, Second);
        }

        public void Start()
        {
            Timer.Enabled = true;
        }

        public void Stop()
        {
            Timer.Enabled = false;
        }
        private int Truncate(int value, int low, int upper)
        {
            if (value < low) return low;
            if (value > upper) return upper;
            return value;
        }
        public void Set(int hour, int minute, int second)
        {
            Hour = Truncate(hour, 0, 23);
            Minute = Truncate(minute, 0, 59);
            Second = Truncate(second, 0, 59);
        }
    }
}
