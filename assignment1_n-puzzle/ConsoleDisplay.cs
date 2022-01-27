namespace assignment1_n_puzzle;

public class ConsoleDisplay
{
    public void Setup()
    {
        Header();
        Console.WriteLine("Welcome!");
        Console.WriteLine("Enter a number to select the size of the board: ");
    }

    private void Header()
    {
        Console.Clear();
        Console.WriteLine("\tN-Puzzle\n");
        Console.WriteLine("Press ESC to exit.");
    }
}