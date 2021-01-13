using System;
using System.Collections.Generic;
using System.Linq;
using Homework_7.Crossover;
using Homework_7.Mutation;

namespace Homework_7.Selection
{
    public class KTournamentSelection : ISelection
    {
        private readonly Func<double[], double> _fitnessFunction;
        private readonly ICrossover _crossover;
        private readonly IMutation _mutation;
        private readonly int _k;

        private static readonly Random Random = new();

        public KTournamentSelection(Func<double[], double> fitnessFunction, ICrossover crossover, IMutation mutation,
            int k = 3)
        {
            _fitnessFunction = fitnessFunction;
            _crossover = crossover;
            _mutation = mutation;
            _k = k;
        }

        public Individual Select(List<Individual> population)
        {
            var randomized = new int[_k];
            for (var i = 0; i < _k; i++)
            {
                var index = Random.Next(population.Count);
                randomized[i] = index;
            }

            randomized = randomized.OrderByDescending(i => population[i].Fitness).ToArray();

            var best = population[randomized[0]];
            var secondBest = population[randomized[1]];
            var worst = population[randomized[2]];

            var child = _crossover.Cross(best, secondBest);
            child = _mutation.Mutate(child);
            child.Fitness = -_fitnessFunction(child.Representation);

            // if(child.Fitness > worst.Fitness)
                // population[randomized[2]] = child;
            population[randomized[2]] = child;
            
            return child;
        }
    }
}