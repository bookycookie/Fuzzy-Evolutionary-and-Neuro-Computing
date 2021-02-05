using System;
using System.Linq;
using Homework_7.Helpers;

namespace Homework_7.GA.Mutation.DoubleMutation
{
    /// <summary>
    /// Mutates an individual using 
    /// </summary>
    public class CombinedMutation : IMutation
    {
        private static readonly Random Random = new();

        private readonly IMutation[] _mutations;
        private readonly double[] _desirability;

        public CombinedMutation(IMutation[] mutations,
            double[] desirability)
        {
            _mutations = mutations;
            _desirability = desirability;
        }

        public void Mutate(Individual individual)
        {
            var desirability = _desirability.Sum();
            var random = Random.NextDouble(0, desirability);

            var choice = 0.0;
            for (var mutationIdx = 0; mutationIdx < _desirability.Length; mutationIdx++)
            {
                choice += _desirability[mutationIdx];
                if (random <= choice) 
                {
                    _mutations[mutationIdx].Mutate(individual);
                    break;
                }
            }

        }
    }
}