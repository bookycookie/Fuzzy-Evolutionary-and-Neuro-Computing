using System;
using System.Linq;

namespace Homework_7.Mutation.DoubleMutation
{
    /// <summary>
    /// Mutates an individual using 
    /// </summary>
    public class CombinedMutation : IMutation
    {
        private static readonly Random Random = new();

        private readonly IMutation[] _mutations;
        private readonly double[] _deviations;
        private readonly double[] _mutationProbabilities;
        private readonly double[] _desirability;

        public CombinedMutation(IMutation[] mutations,
            double[] desirability)
        {
            _mutations = mutations;
            _desirability = desirability;
        }

        public Individual Mutate(Individual individual)
        {
            var desirability = _desirability.Sum();
            var random = Random.NextDouble(0, desirability);

            var dimension = individual.Representation.Length;
            var child = new Individual(dimension);

            var choice = 0.0;
            for (var mutationIdx = 0; mutationIdx < _desirability.Length; mutationIdx++)
            {
                choice += _desirability[mutationIdx];
                if (random <= choice) 
                    child = _mutations[mutationIdx].Mutate(individual);
            }

            return child;
        }
    }
}