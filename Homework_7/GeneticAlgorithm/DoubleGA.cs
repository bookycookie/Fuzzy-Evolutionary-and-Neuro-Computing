using System;
using System.Collections.Generic;
using Homework_7.ANN;
using Homework_7.Selection;

namespace Homework_7.GeneticAlgorithm
{
    public class DoubleGa : IGeneticAlgorithm<double[]>
    {
        private List<Individual> _population;

        private readonly Func<double[], double> _fitnessFunction;

        private readonly int _iterations;

        private readonly int _dimensions;
        private readonly int _populationSize;

        private Individual _maxIndividual;


        public DoubleGa(NeuralNetwork nn, double[] parameters, int dimensions, int populationSize, int iterations)
        {
            _fitnessFunction = nn.MeanSquareError;
            _dimensions = nn.GetNumberOfRequiredParameters();
            _populationSize = populationSize;
            _iterations = iterations;
            _maxIndividual = new Individual(dimensions);
        }

        public Individual FindBestIndividual(bool speedRun, ISelection selection, double epsilon = 1E-7)
        {
            _population = InitializePopulation(_dimensions, _populationSize);
            for (var i = 0; i < _iterations; i++)
            {
                var child = selection.Select(_population);

                if (child.Fitness > _maxIndividual.Fitness)
                {
                    _maxIndividual = child;
                    Console.WriteLine($"i: {i:N0} - MSE: {-_maxIndividual.Fitness}");
                }

                if (speedRun && Math.Abs(_maxIndividual.Fitness) < epsilon)
                {
                    Console.WriteLine($"Total iterations: {i:N0}");
                    return _maxIndividual;
                }
            }

            return _maxIndividual;
        }

        private List<Individual> InitializePopulation(int dimensions, int populationSize)
        {
            var random = new Random();
            var population = new List<Individual>(populationSize);

            for (var i = 0; i < populationSize; i++)
            {
                population.Add(new Individual(dimensions));
                for (var j = 0; j < dimensions; j++)
                    population[i].Representation[j] = random.NextDouble(-1.0, 1.0);

                population[i].Fitness = -_fitnessFunction(population[i].Representation);
                if (population[i].Fitness > _maxIndividual.Fitness)
                    _maxIndividual = population[i];
            }

            return population;
        }
    }
}