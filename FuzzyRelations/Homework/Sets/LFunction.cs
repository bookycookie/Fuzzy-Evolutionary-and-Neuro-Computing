namespace Homework.Sets
{
    public class LFunction : IIntUnaryFunction
    {
        private readonly int _alpha;
        private readonly int _beta;

        public LFunction(int alpha, int beta)
        {
            _alpha = alpha;
            _beta = beta;
        }
        public double ValueAt(int index)
        {
            if (index < _alpha) return 1;
            if (index >= _beta) return 0;
            
            return (double) (_beta - index) / (_beta - _alpha);
        }
    }
}