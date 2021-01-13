using System;

namespace Homework_7.GA.Crossover.DoubleCrossover
{
        public class OnePointCrossover : ICrossover
        {
            private static readonly Random Random = new();

            public Individual Cross(Individual a, Individual b)
            {
                var dimension = a.Representation.Length;
                var crossoverPoint = Random.Next(dimension - 1);
                var child = new Individual(dimension);

                for (var i = 0; i < dimension; i++)
                    child.Representation[i] = i <= crossoverPoint ? a.Representation[i] : b.Representation[i];

                return child;
        }
    }
}