using System.Runtime.CompilerServices;

namespace assignment1_n_puzzle;

public class Board
{
    private int _chosenTileAmount;
    private int _totalTileAmount;
    private int _playerX;
    private int _playerY;
    private List<Tile> _tiles = new List<Tile>();
    private List<Tile> _tilesCompletionState;

    public Board(int tileAmount)
    {
        _chosenTileAmount = tileAmount;
        _totalTileAmount = tileAmount * tileAmount;
        
        // Generate a dynamic array of tiles and randomize their coordinates until the board is solvable.
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

    private void GenerateCompletionState()
    {
        _tilesCompletionState = _tiles;
        _tilesCompletionState[0].SwitchPlaces(_tilesCompletionState[_totalTileAmount - 1]);
    }

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
        else if (_chosenTileAmount % 2 == 0)
        {
            int positionFromBottom = _chosenTileAmount - _tiles[0].GetY() + 1;
            
            // Blank is on even row from bottom and inversions are odd
            if (positionFromBottom % 2 == 0 && inversions % 2 == 1)
            {
                return true;
            }
            // Blank is on odd row from bottom and inversions are even
            else if (positionFromBottom % 2 == 1 && inversions % 2 == 0)
            {
                return true;
            }
        }

        return false;
    }

    private int CountInversions()
    {
        int inversions = 0;
        for (int ix = 0, iy = 0, jy = 0, jx = 1, n = 0; n < _totalTileAmount; n++, ix++, jx++)
        {
            if (jx > _chosenTileAmount)
            {
                jy++;
                jx = 0;
            }
            else if (ix > _chosenTileAmount)
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
}