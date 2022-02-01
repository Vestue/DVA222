namespace assignment1_n_puzzle;

public class ConsoleDisplay
{
    public void Setup()
    {
        Header();
        Console.WriteLine("Welcome!");
        Console.WriteLine("Enter a number to select the size of the board.");
        Footer();
    }

    private void Header()
    {
        Console.Clear();
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("                   N-Puzzle\n");
        Console.WriteLine("Press ESC to exit.");
        Console.WriteLine("------------------------------------------------\n");
    }

    public void Game(Board board)
    {
        Header();
        board.DisplayGrid();
        Console.WriteLine("Use arrow-keys to move the blank space.");
        Footer();
    }

    public void VictoryScreen(int moves)
    {
        Header();
        Console.WriteLine("Congratulations!");
        Console.WriteLine($"Amount of moves: {moves}");
        Console.WriteLine("\nPress any key to exit.");
    }

    private void Footer()
    {
        Console.WriteLine("------------------------------------------------");
    }
}