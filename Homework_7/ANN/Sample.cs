namespace Homework_7.ANN
{
    public class Sample
    {
        public double X { get; init; }
        public double Y { get; init; }
        public int A { get; init; }
        public int B { get; init; }
        public int C { get; init; }

        public override string ToString() => $"({X}, {Y}, {A}, {B}, {C})";
    }
}