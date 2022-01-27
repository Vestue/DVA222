namespace assignment1_n_puzzle;

public class Board
{
    private int _chosenTileAmount;
    private int _inversions;
    private int _totalTileAmount;
    private int _playerX;
    private int _playerY;

    public Board(int tileAmount)
    {
        _chosenTileAmount = tileAmount;
        _totalTileAmount = tileAmount * tileAmount;
    }
}