namespace assignment1_n_puzzle;

public class Game
{
    private int ChosenTiles;
    private ConsoleDisplay _display = new ConsoleDisplay();
    private UserInput _userInput = new UserInput();
    private char _input;
    public void Start()
    {
        _display.Setup();
        _input = _userInput.Init();
        if (char.IsDigit(_input))
        {
            ChosenTiles = int.Parse(_input.ToString());
        }
        else
        {
            Exit();
        }
    }

    private void Exit()
    {
        Environment.Exit(0);
    }
}