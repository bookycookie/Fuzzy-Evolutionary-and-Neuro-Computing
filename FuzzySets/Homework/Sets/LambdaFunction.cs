namespace Homework.Sets
{
    public class LambdaFunction : IIntUnaryFunction
    {
        private readonly int _alpha;
        private readonly int _beta;
        private readonly int _gamma;

        public LambdaFunction(int alpha, int beta, int gamma) {
            _alpha = alpha;
            _beta = beta;
            _gamma = gamma;
        }

        public double ValueAt(int index)
        {
            if (index < _alpha || index >= _gamma) {
                return 0;
            }

            if (index < _beta) {
                return (double) (index - _alpha) / (_beta - _alpha);
            }

            return (double) (_gamma - index) / (_gamma - _beta);
        }
    }
}