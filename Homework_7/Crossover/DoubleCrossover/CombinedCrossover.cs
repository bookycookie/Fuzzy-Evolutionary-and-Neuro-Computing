using System;

namespace Homework_7.Crossover.DoubleCrossover
{
    public class CombinedCrossover : ICrossover
    {
        private static readonly Random Random = new();

        private readonly ICrossover[] _crossovers;

        public CombinedCrossover(ICrossover[] crossovers) => _crossovers = crossovers;

        public Individual Cross(Individual a, Individual b)
        {
            var randomIdx = Random.Next(0, _crossovers.Length);
            return _crossovers[randomIdx].Cross(a, b);
        }
    }
}