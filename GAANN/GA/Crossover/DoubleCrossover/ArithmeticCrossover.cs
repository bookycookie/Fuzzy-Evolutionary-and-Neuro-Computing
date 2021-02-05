using System;

namespace Homework_7.GA.Crossover.DoubleCrossover
{
    public class ArithmeticCrossover : ICrossover
    {
        public static readonly Random Random = new();

        public Individual Cross(Individual a, Individual b)
        {
            var dimension = a.Representation.Length;
            var child = new Individual(dimension);

            for (var i = 0; i < dimension; i++)
            {
                var nextDouble = Random.NextDouble();
                child.Representation[i] = nextDouble * a.Representation[i] + (1 - nextDouble) * b.Representation[i];
            }

            return child;
        }
    }
}