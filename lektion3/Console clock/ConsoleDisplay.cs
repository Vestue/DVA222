namespace Console_clock
{
    public class ConsoleDisplay
    {
        public void Display(int hour, int minute, int second)
        {
            Console.Clear();
            Console.WriteLine($"{hour}:{minute}:{second}");
        }
    }
}