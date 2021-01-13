using System;

namespace Homework_7.GA.Crossover.DoubleCrossover
{
    public class BlxDoubleCrossover : ICrossover
    {
        private static readonly Random Random = new();
        public Individual Cross(Individual a, Individual b)
        {
            var dimension = a.Representation.Length;
            var child = new Individual(dimension);
            const double alpha = 0.5;

            for (var i = 0; i < dimension; i++)
            {
                var min = Math.Min(a.Representation[i], b.Representation[i]);
                var max = Math.Max(a.Representation[i], b.Representation[i]);
                var difference = max - min;

                var lowerBound = min - difference * alpha;
                var upperBound = max + difference * alpha;

                child.Representation[i] = lowerBound + Random.NextDouble() * (upperBound - lowerBound);
            }

            return child;
        }
    }
}