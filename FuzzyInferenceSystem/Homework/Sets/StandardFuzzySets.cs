namespace Homework.Sets
{
    public static class StandardFuzzySets
    {
        public static IIntUnaryFunction LFunction(int alpha, int beta) => new LFunction(alpha, beta);

        public static IIntUnaryFunction GammaFunction(int alpha, int beta) => new GammaFunction(alpha, beta);

        public static IIntUnaryFunction LambdaFunction(int alpha, int beta, int gamma) => new LambdaFunction(alpha, beta, gamma);
    }
}