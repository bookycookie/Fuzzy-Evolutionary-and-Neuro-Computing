using System;

namespace Homework_7.Crossover.DoubleCrossover
{
    public sealed class DiscreteUniformRecombination : ICrossover
    {
        private static readonly Random Random = new();

        public Individual Cross(Individual a, Individual b)
        {
            var dimension = a.Representation.Length;
            var child = new Individual(dimension);

            for (var i = 0; i < dimension; i++)
                child.Representation[i] = Random.NextDouble() > 0.5 ? a.Representation[i] : b.Representation[i];

            return child;
        }
    }
}