namespace Lektion4;

internal class OpaqueValue
{
    private int Value;

    public OpaqueValue(int value)
    {
        Value = value;
    }

    public void Display()
    {
        Console.Write($"{Value}");
    }
}