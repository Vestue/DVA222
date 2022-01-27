namespace assignment1_n_puzzle;

public class Game
{
    private int _chosenTiles;
    private ConsoleDisplay _display = new ConsoleDisplay();
    private UserInput _userInput = new UserInput();
    private Board _board;
    private int _moveAmount = 0;

    public Game()
    {
        Start();
    }
    private void Start()
    {
        _display.Setup();
        char input = _userInput.Init();
        
        if (char.IsDigit(input))
        {
            _chosenTiles = int.Parse(input.ToString());
        }
        else
        {
            Exit();
        }

        _board = new Board(_chosenTiles);
    }

    private int Move()
    {
        while (_board.BoardCompleted() == false)
        {
            _display.Header();
            
        }

        return 1;
    }

    // !! COULD BE REMOVED AND REPLACED WITH A RETURN !!
    private void Exit()
    {
        Console.WriteLine("Exiting..");
        Environment.Exit(0);
    }
}