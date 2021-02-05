namespace Homework.Sets
{
    public class GammaFunction : IIntUnaryFunction
    {
        private readonly int _alpha;
        private readonly int _beta;

        public GammaFunction(int alpha, int beta)
        {
            _alpha = alpha;
            _beta = beta;
        }

        public double ValueAt(int index)
        {
            if (index < _alpha) return 0;
            if (index >= _beta) return 1;
            
            return (double) (index - _alpha) / (_beta - _alpha);
        }
    }
}