namespace assignment1_n_puzzle;

public class Tile
{
   private int _number;

   public Tile(int number)
   {
      _number = number;
   }

   public int GetNumber() => _number;

   public void SwitchPlaces(Tile other)
   {
      (_number, other._number) = (other._number, _number);
   }
}