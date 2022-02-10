namespace assignment1_n_puzzle;

public class Board
{
    private int _chosenTileAmount;
    private int _totalTileAmount;
    private Tile[,] _tiles;
    private Tile[,] _tilesCompletionState;
    private int _blankX;
    private int _blankY;
    
    public Board()
    {
        _chosenTileAmount = 0;
        _totalTileAmount = 0;
    }

    public Board(int tileAmount)
    {
        _chosenTileAmount = tileAmount;
        _totalTileAmount = tileAmount * tileAmount;

        _tiles = new Tile[tileAmount, tileAmount];
        
        // Generate a dynamic array of tiles and randomize their location.
        // The tiles will randomly switch places with each other until the puzzle is in a solvable state.
        GenerateTiles();
        GenerateCompletionState();
        
        do
        {
            RandomizeTileOrder();
        } while (CheckSolvable() == false || BoardCompleted());
    }

    private void GenerateTiles()
    {
        int x = 0, y = 0;
        for (int i = 1; i < _totalTileAmount; i++, x++)
        {
            if (x == _chosenTileAmount)
            {
                y++;
                x = 0;
            }
            
            _tiles[x, y] = new Tile(i, y);
        }
        // Generates the last blank tile
        _tiles[x, y] = new Tile(0, y);

        _blankX = x;
        _blankY = y;
    }
    
    // Create a copy of the desired list for comparison between moves
    private void GenerateCompletionState()
    {
        _tilesCompletionState = (Tile[,]) _tiles.Clone();
    }
    
    // Each tile will switch places with another randomly selected tile
    private void RandomizeTileOrder()
    {
        Random rand = new Random();
        
        for (int i = 0, x = 0, y = 0; i < _totalTileAmount; i++, x++)
        {
            if (x == _chosenTileAmount)
            {
                y++;
                x = 0;
            }

            int randomX = rand.Next(0, _chosenTileAmount - 1);
            int randomY = rand.Next(0, _chosenTileAmount - 1);

            SwapTiles(x, y, randomX, randomY);
        }
    }

    private bool CheckSolvable()
    {
        int inversions = CountInversions();
        
        // N is odd and inversions are even
        if (_chosenTileAmount % 2 == 1 && inversions % 2 == 0)
        {
            return true;
        }
        if (_chosenTileAmount % 2 == 0)
        {
            int positionFromBottom = _chosenTileAmount - _blankY;
            // Blank is on even row from bottom and inversions are odd
            if (positionFromBottom % 2 == 0 && inversions % 2 == 1)
            {
                return true;
            }
            // Blank is on odd row from bottom and inversions are even
            if (positionFromBottom % 2 == 1 && inversions % 2 == 0)
            {
                return true;
            }
        }

        return false;
    }

    // Count the amount of times where the number of a tile is greater than the number of the tiles after it.
    // This is used in an algorithm to see if the current board is solvable.
    // As "0" represents the blank tile, counting of it is skipped.
    private int CountInversions()
    {
        int inversions = 0;
        for (int i = 0, x = 0, y = 0; i < _totalTileAmount - 1; i++, x++)
        {
            if (x == _chosenTileAmount)
            {
                y++;
                x = 0;
            }

            if (_tiles[x, y].GetNumber() != 0)
            {
                for (int j = i, jx = x + 1, jy = y; j < _totalTileAmount; j++, jx++)
                {
                    if (jx == _chosenTileAmount)
                    {
                        jy++;
                        jx = 0;
                    }
                    
                    if (jy == _chosenTileAmount)
                        break;

                    if (_tiles[x, y].GetNumber() > _tiles[jx, jy].GetNumber() && _tiles[jx, jy].GetNumber() != 0)
                    {
                        inversions++;
                    }
                } 
            }
        }

        return inversions;
    }

    // Compares the current list of tiles with the generated list of the desired completion state
    public bool BoardCompleted()
    {
        for (int i = 0, x = 0, y = 0; i < _totalTileAmount; i++, x++)
        {
            if (x == _chosenTileAmount)
            {
                y++;
                x = 0;
            }
            
            if (_tiles[x, y].GetNumber() != _tilesCompletionState[x, y].GetNumber())
            {
                return false;
            }
        }

        return true;
    }

    public void DisplayGrid()
    {
        for (int x = 0, y = 0, i = 0; i < _totalTileAmount; i++, x++)
        {
            Console.ForegroundColor = _tiles[x, y].GetColor();
            if (x == _chosenTileAmount - 1)
            {
                Console.WriteLine($"{_tiles[x, y].GetNumber(), 6}");
                Console.WriteLine("");
                y++;
                x = 0;
                i++;

                if (y < _chosenTileAmount)
                    Console.ForegroundColor = _tiles[x, y].GetColor();
            }
            if (y < _chosenTileAmount)
                Console.Write($"{_tiles[x ,y].GetNumber(), 6}");
        } 
        Console.ResetColor();
    }

    // Move the tile if it is within the frames of the board.
    // Everything is contained within if statements to prevent improper use.
    public void Move(Movement move)
    {
        switch (move)
        {
            case Movement.Up:
                if (_blankY != 0)
                    SwapTiles(_blankX, _blankY, _blankX, _blankY - 1);
                break;
            case Movement.Down:
                if (_blankY < _chosenTileAmount - 1)
                    SwapTiles(_blankX, _blankY, _blankX, _blankY + 1);
                break;
            case Movement.Left:
                if (_blankX != 0)
                    SwapTiles(_blankX, _blankY, _blankX - 1, _blankY);
                break;
            case Movement.Right:
                if (_blankX < _chosenTileAmount - 1)
                    SwapTiles(_blankX, _blankY, _blankX + 1, _blankY);
                break;
        }
    }

    private void SwapTiles(int x1, int y1, int x2, int y2)
    {
        (_tiles[x1, y1], _tiles[x2, y2]) = (_tiles[x2, y2], _tiles[x1, y1]);

        // Coordinates of the blank has to be updated if it is moved.
        // This to have direct access of the blank in other methods.
        if (_tiles[x1, y1].GetNumber() == 0)
        {
            _blankX = x1;
            _blankY = y1;
        }
        else if (_tiles[x2, y2].GetNumber() == 0)
        {
            _blankX = x2;
            _blankY = y2;
        }
    }
}