namespace assignment1_n_puzzle;

public class Board
{
    private int _chosenTileAmount;
    private int _inversions;
    private int _totalTileAmount;
    private int _playerX;
    private int _playerY;
    private List<Tile> _tiles = new List<Tile>();

    public Board(int tileAmount)
    {
        _chosenTileAmount = tileAmount;
        _totalTileAmount = tileAmount * tileAmount;
    }

    private void GenerateTiles()
    {
        int x = 0;
        int y = 0;
        for (var i = 0; i < _totalTileAmount; i++)
        {
            _tiles.Add(new Tile(x, y, i));
            
            if (x == _chosenTileAmount)
            {
                y++;
                x = 0;
            }
        }
    }
}