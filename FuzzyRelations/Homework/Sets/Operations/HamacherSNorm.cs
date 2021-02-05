namespace Homework.Sets.Operations
{
    public class HamacherSNorm : IBinaryFunction
    {
        private readonly double _nu;

        public HamacherSNorm(double nu) => _nu = nu;

        public double ValueAt(double a, double b) => (a + b - (2 - _nu) * a * b) / (1 - (1 - _nu) * a * b);
    }
}