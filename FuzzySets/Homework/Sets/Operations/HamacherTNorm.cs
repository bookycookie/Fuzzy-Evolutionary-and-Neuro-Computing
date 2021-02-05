namespace Homework.Sets.Operations
{
    public class HamacherTNorm : IBinaryFunction
    {
        private readonly double _nu;

        public HamacherTNorm(double nu) => _nu = nu;

        public double ValueAt(double a, double b) => a * b / (_nu + (1 - _nu) * (a + b - a * b));
        
        
    }
}
