namespace assignment1_n_puzzle;

public class Tile
{
   private int _number;
   private ConsoleColor _color;

   public Tile(int number, int y)
   {
      _number = number;
      Colorize(number, y);
   }

   public int GetNumber() => _number;
   public ConsoleColor GetColor() => _color;

   private void Colorize(int number, int y)
   {
      if (number == 0)
      {
         _color = ConsoleColor.Black;
      }
      else
      {
         // Each row gets its' own color which can be anything but black or white
         _color = (ConsoleColor)(y % 15 + 1);
      }
   }
}