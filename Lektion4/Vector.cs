namespace Lektion4;

internal class Vector
{
    protected double X, Y;

    public Vector(double x, double y)
    {
        X = x; Y = y;
    }

    public double GetX() => X;
    public double GetY() => Y;
    public double GetLength() => X * X + Y * Y;
}

class MutableVector : Vector
{
    public MutableVector(double x, double y) : base(x, y)
    {
    }

    public void SetX(double x) => X = x;
    public void SetY(double y) => Y = y;

    public void Add(Vector other)
    {
        X += other.GetX();
        Y += other.GetY();
    }
}