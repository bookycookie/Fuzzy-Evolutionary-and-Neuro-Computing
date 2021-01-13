using System;
using Homework_7.Helpers;

namespace Homework_7.GA.Mutation.DoubleMutation
{
    public class GaussianAdditiveMutation : IMutation
    {
        private static readonly Random Random = new();
        private readonly double _mutationProbability;
        private readonly double _deviation;
        
        public GaussianAdditiveMutation(double mutationProbability, double deviation)
        {
            _mutationProbability = mutationProbability;
            _deviation = deviation;
        }

        public Individual Mutate(Individual individual)
        {
            var dimension = individual.Representation.Length;
            var child = new Individual(dimension);
            for (var i = 0; i < dimension; i++) {
                var next = Random.NextDouble();
                if (next < _mutationProbability)
                    child.Representation[i] = individual.Representation[i] + Random.NextGaussian(0, _deviation);
                else
                    child.Representation[i] = individual.Representation[i];
            }

            return child;
        }
    }
}