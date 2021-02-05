namespace NENR6.ANFIS
{
    /// <summary>
    /// Class used for file storage and data manipulation in the project.
    /// </summary>
    public class Sample
    {
        public double X { get; }
        public double  Y { get; }
        public double Z { get; }
        public Sample(double x, double y)
        {
            X = x;
            Y = y;
            Z = Functions.Functions.Z(x, y);
        }

        public Sample(double x, double y, double evaluated)
        {
            X = x;
            Y = y;
            Z = evaluated;
        }

        public override string ToString() => $"f({X}, {Y}) = {Z:F5}";
    }
}