using System;
using Homework_7.Helpers;

namespace Homework_7.GA.Mutation.DoubleMutation
{
    public class GaussianSwapMutation : IMutation
    {
        private static readonly Random Random = new();
        
        private readonly double _mutationProbability;
        private readonly double _deviation;
        
        public GaussianSwapMutation(double mutationProbability, double deviation)
        {
            _mutationProbability = mutationProbability;
            _deviation = deviation;
        }

        public void Mutate(Individual individual)
        {
            var dimension = individual.Representation.Length;
            for (var i = 0; i < dimension; i++) {
                var next = Random.NextDouble();
                if (next < _mutationProbability)
                    individual.Representation[i] = Random.NextGaussian(0, _deviation);
            }
        }
    }
}