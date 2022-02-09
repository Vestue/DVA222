namespace assignment1_n_puzzle;

public class UserInput
{
   private ConsoleKeyInfo _userInput;
   
   // Returns either ESC or a valid number for the setup process
   public char Init()
   {
      do
      {
         _userInput = Console.ReadKey(true);
      } while (_userInput.Key != ConsoleKey.Escape
               && char.IsDigit(_userInput.KeyChar) != true || _userInput.KeyChar == '1' || _userInput.KeyChar == '0');

      return _userInput.KeyChar;
   }

   public ConsoleKey Movement()
   {
      do
      {
         _userInput = Console.ReadKey(true);
      } while (_userInput.Key != ConsoleKey.Escape
               && _userInput.Key != ConsoleKey.UpArrow 
               && _userInput.Key != ConsoleKey.DownArrow 
               && _userInput.Key != ConsoleKey.RightArrow 
               && _userInput.Key != ConsoleKey.LeftArrow);

      return _userInput.Key;
   }
}