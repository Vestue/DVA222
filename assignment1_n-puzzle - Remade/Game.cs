namespace assignment1_n_puzzle;

public class Game
{
    private int _chosenTiles;
    private ConsoleDisplay _display = new ConsoleDisplay();
    private UserInput _userInput = new UserInput();
    private Board _board = new Board();
    private int _moveAmount;

    public Game()
    {
        Start();

        if (Move() == 0)
        {
            _display.VictoryScreen(_moveAmount);
            Console.ReadKey(true);
        }
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
            _display.Game(_board);

            switch (_userInput.Movement())
            {
                case ConsoleKey.UpArrow:
                    _board.Move(Movement.Up);
                    break;
                case ConsoleKey.DownArrow:
                    _board.Move(Movement.Down);
                    break;
                case ConsoleKey.LeftArrow:
                    _board.Move(Movement.Left);
                    break;
                case ConsoleKey.RightArrow:
                    _board.Move(Movement.Right);
                    break;
                case ConsoleKey.Escape:
                    return 1;
            }

            _moveAmount++;
        }

        return 0;
    }

    private void Exit()
    {
        Console.WriteLine("Exiting..");
        Environment.Exit(0);
    }
}