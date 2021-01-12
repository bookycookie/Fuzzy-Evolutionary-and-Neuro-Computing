namespace Homework_7
{
    public class Sample
    {
        public double X { get; init; }
        public double Y { get; init; }
        public double A { get; init; }
        public double B { get; init; }
        public double C { get; init; }

        public override string ToString() => $"({X}, {Y}, {A}, {B}, {C})";
    }
}