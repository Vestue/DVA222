namespace assignment1_n_puzzle;

public class Board
{
    private int _chosenTileAmount;
    private int _totalTileAmount;
    private List<Tile> _tiles = new List<Tile>();
    private List<Tile> _tilesCompletionState = new List<Tile>();

    public Board(int tileAmount)
    {
        _chosenTileAmount = tileAmount;
        _totalTileAmount = tileAmount * tileAmount;
        
        // Generate a dynamic array of tiles and randomize their location.
        // The tiles will randomly switch places with each other until the puzzle is in a solvable state.
        GenerateTiles();
        GenerateCompletionState();
        
        do
        {
            RandomizeTileOrder();
        } while (CheckSolvable() == false);
    }

    private void GenerateTiles()
    {
        int x = 0;
        int y = 0;
        for (var i = 0; i < _totalTileAmount; i++, x++)
        {
            if (x == _chosenTileAmount)
            {
                y++;
                x = 0;
            }
            
            _tiles.Add(new Tile(x, y, i));
        }
    }
    
    // Create a copy of the desired list for comparison between moves
    private void GenerateCompletionState()
    {
        // Copy the data individually to avoid passing by reference
        for (int i = 0; i < _totalTileAmount; i++)
        {
            _tilesCompletionState.Add(new Tile(_tiles[i].GetX(), _tiles[i].GetY(), i));
        } 
        // Move the blank tile to the bottom and every other tile up
        for (int i = 1; i < _totalTileAmount; i++)
        {
            _tilesCompletionState[0].SwitchPlaces(_tilesCompletionState[i]);
        }
    }
    
    // Each tile will switch places with another randomly selected tile
    private void RandomizeTileOrder()
    {
        Random rand = new Random();
        
        for (int i = 0; i < _totalTileAmount; i++)
        {
            int randomTileIndex = rand.Next(i, _totalTileAmount);
            _tiles[i].SwitchPlaces(_tiles[randomTileIndex]);
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
            int positionFromBottom = _chosenTileAmount - _tiles[0].GetY() + 1;
            
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

    // Count the amount of times where the number of a tile is greater than the number of the tile after it.
    // This is used in an algorithm to see if the current board is solvable.
    private int CountInversions()
    {
        int inversions = 0;
        for (int ix = 0, iy = 0, jy = 0, jx = 1, n = 1; n < _totalTileAmount; n++, ix++, jx++)
        {
            if (jx == _chosenTileAmount)
            {
                jy++;
                jx = 0;
            }
            else if (ix == _chosenTileAmount)
            {
                iy++;
                ix = 0;
            }

            int iIndex = FindTileIndex(ix, iy);
            int jIndex = FindTileIndex(jx, jy);

            if (_tiles[iIndex].GetNumber() > _tiles[jIndex].GetNumber())
            {
                inversions++;
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
    public void Move(Enum movement)
    {
        if (movement.Equals(Movement.Up) && _tiles[0].GetY() != 0)
        {
            int replaceIndex = FindTileIndex(_tiles[0].GetX(), _tiles[0].GetY() - 1);
            _tiles[0].SwitchPlaces(_tiles[replaceIndex]);
        }
        else if (movement.Equals(Movement.Down) && _tiles[0].GetY() < _chosenTileAmount - 1)
        {
            int replaceIndex = FindTileIndex(_tiles[0].GetX(), _tiles[0].GetY() + 1);
            _tiles[0].SwitchPlaces(_tiles[replaceIndex]); 
        }
        else if (movement.Equals(Movement.Left) && _tiles[0].GetX() != 0)
        {
            int replaceIndex = FindTileIndex(_tiles[0].GetX() - 1, _tiles[0].GetY());
            _tiles[0].SwitchPlaces(_tiles[replaceIndex]);  
        }
        else if (movement.Equals(Movement.Right) && _tiles[0].GetX() < _chosenTileAmount - 1)
        {
            int replaceIndex = FindTileIndex(_tiles[0].GetX() + 1, _tiles[0].GetY());
            _tiles[0].SwitchPlaces(_tiles[replaceIndex]);   
        }
    }
}