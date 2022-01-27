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
        Console.WriteLine("Press ESC to exit.\n");
    }

    public void Game(Board board)
    {
        Header();
        board.DisplayGrid();
        Console.WriteLine("\nUse arrow-keys to move the blank space.");
    }

    public void VictoryScreen(int moves)
    {
        Header();
        Console.WriteLine("Congratulations!");
        Console.WriteLine($"Amount of moves: {moves}");
    }
}