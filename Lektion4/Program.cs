// See https://aka.ms/new-console-template for more information

using Lektion4;

var v1 = new Vector(1, 2);
Console.WriteLine(v1.GetLength());

var v2 = new MutableVector(3, 4);
v2.SetX(v1.GetX());