TTRiangle x = new();
x.Input();
x.Print();
Console.WriteLine("\nPerimeter = {0}", x.GetPerimeter());
Console.WriteLine("\nArea = {0}", x.GetArea());
TTRiangle y = new(3, 4, 5);
TTRiangle z = x + y * 2;
bool areEqual = x == y;

