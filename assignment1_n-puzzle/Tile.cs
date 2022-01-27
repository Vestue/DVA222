namespace assignment1_n_puzzle;

public class Tile
{
   private int _number;
   private int _x;
   private int _y;

   public Tile(int x, int y, int number)
   {
      _x = x;
      _y = y;
      _number = number;
   }

   public int GetX() => _x;
   public int GetY() => _y;
   public int GetNumber() => _number;

   public void SwitchPlaces(Tile other)
   {
      (_number, other._number) = (other._number, _number);
   }
}