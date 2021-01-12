using System;

namespace Homework_7.Crossover.DoubleCrossover
{
    public class SimpleArithmeticRecombination : ICrossover
    {
        private static readonly Random Random = new();

        public Individual Cross(Individual a, Individual b)
        {
            var dimension = a.Representation.Length;
            var crossoverPoint = Random.Next(a.Representation.Length - 1);
            var child = new Individual(dimension);

            for (var i = 0; i < dimension; i++)
                child.Representation[i] = i <= crossoverPoint
                    ? a.Representation[i]
                    : (a.Representation[i] + b.Representation[i]) / 2;

            return child;
        }
    }
}