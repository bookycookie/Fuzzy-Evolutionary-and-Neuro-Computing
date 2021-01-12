using System;
using System.Collections.Generic;
using System.Globalization;
using Homework_7.Activations;
using Homework_7.ANN;
using Homework_7.Crossover;
using Homework_7.Crossover.DoubleCrossover;
using Homework_7.GeneticAlgorithm;
using Homework_7.Helpers;
using Homework_7.Mutation;
using Homework_7.Mutation.DoubleMutation;
using Homework_7.Selection;

namespace Homework_7
{
    class Program
    {
        private const string Root = "C:/git/Fuzzy-Evolutionary-and-Neuro-Computing/Homework_7/";

        private static void Main(string[] args)
        {
            // Task1();
            var dataset = new Dataset();
            var nn = new NeuralNetwork(dataset, 8);
            const int populationSize = 20;
            const int iterations = 100_000;
            var crossovers = new ICrossover[]
            {
                new BlxDoubleCrossover(),
                new ArithmeticCrossover(),
                new DiscreteUniformRecombination(),
                new OnePointCrossover(),
                new SimpleArithmeticRecombination()
            };
            var mutations = new IMutation[]
            {
                new GaussianAdditiveMutation(mutationProbability: 0.01, deviation: 0.2),
                new GaussianAdditiveMutation(mutationProbability: 0.01, deviation: 0.4),
                new GaussianSwapMutation(mutationProbability: 0.01, deviation: 1.0)
            };
            var desirability = new[] {4.0, 2.0, 1.0}; // 4.0, 2.0, 1.0

            var combinedCrossover = new CombinedCrossover(crossovers);
            var combinedMutation = new CombinedMutation(mutations, desirability);

            var selection =
                new KTournamentSelection(nn.MeanSquareError, combinedCrossover, combinedMutation, k: 3);

            var parametersCount = nn.GetNumberOfRequiredParameters();
            var parameters = new double[parametersCount];
            var doubleGa = new DoubleGa(nn, parameters, parametersCount, populationSize, iterations);

            var bestNetworkParameters = doubleGa.FindBestIndividual(speedRun: false, selection).Representation;

            var success = 0;
            Console.WriteLine($"\nPrediction | Actual");
            foreach (var sample in dataset)
            {
                var prediction = nn.CalculateOutput(sample.X, sample.Y, bestNetworkParameters);
                var A = prediction[0] < 0.5 ? 0 : 1;
                var B = prediction[1] < 0.5 ? 0 : 1;
                var C = prediction[2] < 0.5 ? 0 : 1;
                Console.WriteLine($"[{A} {B} {C}] | [{sample.A} {sample.B} {sample.C}]");

                if (A == sample.A && B == sample.B && C == sample.C)
                    success++;
            }

            Console.WriteLine(
                $"Prediction rate: {success}/{dataset.DatasetCount()} = {(double) success / dataset.DatasetCount() * 100}%");
        }

        private static void NeuralNetworkSanityCheck()
        {
            var dataset = new Dataset();
            var nn = new NeuralNetwork(dataset, 2);
            var paramsTest = new double[17]
            {
                0.3020109288869477, 0.6376704767521799, 0.5267740402029706, 0.9408152335979116, -0.06873436321911142,
                -0.28670631502135946, -0.48398473555407706, -0.7688246275153126, 0.3930492482115744,
                0.25000403414014905, 0.0371730844663331, 0.5647879268810096, 0.9375631650619036, 0.8069575148667012,
                0.20274918582418433, -0.4666248929997091, -0.700613468746009
            };
            var xs = new[] {1, 20, 15, 5};
            var ys = new[] {1, 20, 10, 25};

            for (var i = 0; i < xs.Length; i++)
            {
                var toPrint = nn.CalculateOutput(xs[i], ys[i], paramsTest);
                Console.WriteLine(string.Join(" ", toPrint));
            }
        }

        private static void Task1()
        {
            var path = Root + "Files/Task1/";
            var y = new List<double>();
            var x = new List<double>();
            const double w = 2.0;
            var ss = new[] {1.0, 0.25, 4.0};
            foreach (var s in ss)
            {
                for (var i = -8; i <= 10; i++)
                {
                    x.Add(i);
                    y.Add(ActivationFunctions.Likeness(new double[] {i}, new[] {w}, new[] {s}));
                }

                path = $"{path}/s{s.ToString(CultureInfo.InvariantCulture).Replace('.', '_')}.txt";
                FileHelper.WriteToFile(x, y, path);
                y.Clear();
                x.Clear();
            }
        }
    }
}