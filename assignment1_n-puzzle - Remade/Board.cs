namespace assignment1_n_puzzle;

public class Board
{
    private int _chosenTileAmount;
    private int _totalTileAmount;
    //private List<Tile> _tiles = new List<Tile>();
    //private List<Tile> _tilesCompletionState = new List<Tile>();
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
        for (int x = 0, y = 0, i = 0; i < _totalTileAmount; i++, x++)
        {
            if (x == _chosenTileAmount)
            {
                y++;
                x = 0;
            }

            _tiles[x, y] = new Tile(i);
        }

        _blankX = _blankY = 0;
    }
    
    // Create a copy of the desired list for comparison between moves
    private void GenerateCompletionState()
    {
        _tilesCompletionState = (Tile[,]) _tiles.Clone();
        
        // Move the blank tile to the last spot
        
        for (int x = 1, y = 0, i = 1; i < _totalTileAmount - 1 ; i++, x++)
        {
            if (x == _chosenTileAmount)
            {
                y++;
                x = 0;
            }
            
            // Switch places with the two tiles
            (_tilesCompletionState[_blankX, _blankY], _tilesCompletionState[x, y]) = (_tilesCompletionState[x, y],
                _tilesCompletionState[_blankX, _blankY]);

            _blankX = x;
            _blankY = y;
        }
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

            int randomX = rand.Next(x, _chosenTileAmount - 1);
            int randomY = rand.Next(y, _chosenTileAmount - 1);

            (_tiles[x, y], _tiles[randomX, randomY]) = (_tiles[randomX, randomY], _tiles[x, y]);
            
            // Coordinates of the blank has to be updated if it is moved.
            // This to have direct access of the blank in other methods.
            if (_tiles[randomX, randomY].GetNumber() == 0)
            {
                _blankX = randomX;
                _blankY = randomY;
            }
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
            int positionFromBottom = _chosenTileAmount - _tiles[0].GetY();
            
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
        for (int i = 0, x = 0, y = 0; i < _totalTileAmount; i++, x++)
        {
            if (x == _chosenTileAmount)
            {
                y++;
                x = 0;
            }

            int currentIndex = FindTileIndex(x, y);

            if (currentIndex != 0)
            {
                for (int j = i, jx = x + 1, jy = y; j < _totalTileAmount; j++, jx++)
                {
                    if (jx >= _chosenTileAmount)
                    {
                        jy++;
                        jx = 0;
                    }
                    if (jy == _chosenTileAmount)
                        break;

                    int nextIndex = FindTileIndex(jx, jy);

                    if (_tiles[currentIndex].GetNumber() > _tiles[nextIndex].GetNumber() && nextIndex != 0)
                    {
                        inversions++;
                    }
                } 
            }
        }

        return inversions;
    }
    
    // To find the number of the tile, the program needs to find which index holds the current x and y values.
    // This is used if only x and y values are known but the number and index itself is unknown.
    private int FindTileIndex(int x, int y)
    {
        int i = 0;
        for (; i < _totalTileAmount; i++)
        {
            if (_tiles[i].GetX() == x && _tiles[i].GetY() == y)
            {
                break;
            }
        }

        return i;
    }

    // Compares the current list of tiles with the generated list of the desired completion state
    public bool BoardCompleted()
    {
        for (int i = 0; i < _totalTileAmount; i++)
        {
            if (_tiles[i].GetX() != _tilesCompletionState[i].GetX())
                return false;
            if (_tiles[i].GetY() != _tilesCompletionState[i].GetY())
                return false;
        }

        return true;
    }

    public void DisplayGrid()
    {
        for (int x = 0, y = 0, i = 0; i < _totalTileAmount; i++, x++)
        {
            if (x == _chosenTileAmount - 1)
            {
                Console.WriteLine($"{_tiles[FindTileIndex(x, y)].GetNumber(), 6}");
                Console.WriteLine("");
                y++;
                x = 0;
                i++;
                //Console.Write("\n{0} ", _tiles[FindTileIndex(x, y)].GetNumber());
            }
            if (y < _chosenTileAmount)
                Console.Write($"{_tiles[FindTileIndex(x, y)].GetNumber(), 6}");
        } 
    }

    // Move the tile if it is within the frames of the board.
    // Everything is contained within if statements to prevent improper use.
    public void Move(Movement move)
    {
        if (move == Movement.Up && _tiles[0].GetY() != 0)
        {
            int replaceIndex = FindTileIndex(_tiles[0].GetX(), _tiles[0].GetY() - 1);
            _tiles[0].SwitchPlaces(_tiles[replaceIndex]);
        }
        else if (move == Movement.Down && _tiles[0].GetY() < _chosenTileAmount - 1)
        {
            int replaceIndex = FindTileIndex(_tiles[0].GetX(), _tiles[0].GetY() + 1);
            _tiles[0].SwitchPlaces(_tiles[replaceIndex]); 
        }
        else if (move == Movement.Left && _tiles[0].GetX() != 0)
        {
            int replaceIndex = FindTileIndex(_tiles[0].GetX() - 1, _tiles[0].GetY());
            _tiles[0].SwitchPlaces(_tiles[replaceIndex]);  
        }
        else if (move == Movement.Right && _tiles[0].GetX() < _chosenTileAmount - 1)
        {
            int replaceIndex = FindTileIndex(_tiles[0].GetX() + 1, _tiles[0].GetY());
            _tiles[0].SwitchPlaces(_tiles[replaceIndex]);   
        }
    }
}