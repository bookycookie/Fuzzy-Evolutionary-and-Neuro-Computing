using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Homework_7.ANN;
using Homework_7.ANN.Activations;
using Homework_7.GA;
using Homework_7.GA.Crossover;
using Homework_7.GA.Crossover.DoubleCrossover;
using Homework_7.GA.Mutation;
using Homework_7.GA.Mutation.DoubleMutation;
using Homework_7.GA.Selection;
using Homework_7.Helpers;

namespace Homework_7
{
    class Program
    {
        private const string Root = "./Files/Dataset/zad7-dataset.txt";

        private static void Main(string[] args)
        {
            // Task1();
            // NeuralNetworkSanityCheck();
            Run();
            // PrintTask5();
            // WriteParameterPoints(paramsTest);
        }

        private static void Run()
        {
            var dataset = new Dataset();
            var nn = new NeuralNetwork(dataset, 8); //8, 4
            const int populationSize = 10;
            const int iterations = 1_000_000;
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
                new GaussianAdditiveMutation(mutationProbability: 0.02, deviation: 0.2),
                new GaussianAdditiveMutation(mutationProbability: 0.02, deviation: 0.4),
                new GaussianSwapMutation(mutationProbability: 0.02, deviation: 0.5)
            };
            var desirability = new[] {18.0, 4.0, 2.0};

            var combinedCrossover = new CombinedCrossover(crossovers);
            var combinedMutation = new CombinedMutation(mutations, desirability);

            var selection =
                new KTournamentSelection(nn.MeanSquareError, combinedCrossover, combinedMutation, k: 3);

            var parametersCount = nn.GetNumberOfRequiredParameters();
            var parameters = new double[parametersCount];
            var doubleGa = new DoubleGa(nn, parameters, parametersCount, populationSize, iterations);

            var bestNetworkParameters = doubleGa.FindBestIndividual(speedRun: true, selection).Representation;

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

        private static void PrintTask5()
        {
            var paramsTest = new[]
            {
                0.6169502402684119, -0.18921600945626593, 0.7400752538596453, -0.21864772272936595, 0.38613232905131434,
                -0.16067326051262137, 0.2661089566398632, 0.2581632894148127, 0.13608801282814106, 0.07368036906580472,
                0.26604869869048264, -0.2073535594933702, 0.6438656277837572, -0.27030524360853014, 0.26748316771950165,
                -0.29546186132935137, 0.8723551151141611, -0.05084933889577109, 0.25641152771237496,
                0.18426375661713784, 0.3726872911641915, 0.07350186842530243, 0.7425450865482748, 0.10797186498025088,
                0.8695444491605899, -0.12088527241845112, 0.7364868954336807, 0.27131556339277174, 0.13153100475840476,
                -0.05407656596014572, 0.7442220083582236, 0.18375145864282488, 0.053842022702828625, 58.76515948272971,
                -71.43309533556608, 82.41010778762119, -10.009380298530854, 72.64333628283458, -20.808420994527722,
                -53.5622992460852, -47.5947861467293, -2.3219271592285313, -39.19322596603079, 57.99604910124557,
                -51.26476867449141, -29.69133980093894, -35.04619017511216, -36.12893386011313, 61.684960398613995,
                65.6006693066214, 9.713105911932098, -31.514811903344608, -39.24682210227698, -52.19324765838407,
                55.8699289679712, -44.70387383614851, 72.67793912324004, -40.800813623623064, -21.50269584678943
            };
            for (var i = 0; i < paramsTest.Length; i++)
            {
                if (i < 2 * 2 * 8)
                {
                    if (i % 4 == 0)
                        Console.WriteLine($"x -> {(i / 4.0)} neuron = {paramsTest[i]}");
                    if((i + 1) % 4 == 0)
                        Console.WriteLine($"s_x -> {((i - 1) / 4.0)} neuron = {paramsTest[i]}");
                    if ((i + 2) % 4 == 0)
                        Console.WriteLine($"y -> {(i - 2) / 4.0} neuron  = {paramsTest[i]}");
                    if((i + 3) % 4 == 0)
                        Console.WriteLine($"s_y -> {((i - 3) / 4.0)} neuron = {paramsTest[i]}");
                }
                else
                {
                    Console.WriteLine(paramsTest[i]);
                }
            }

            Console.WriteLine(paramsTest.Length);
        }

        private static void WriteParameterPoints(IReadOnlyList<double> parameters, int likenessLayerNeurons = 8)
        {
            const string path =
                "C:/git/Fuzzy-Evolutionary-and-Neuro-Computing/Homework_7/Files/Parameters/Data/test_data.txt";
            var x = new List<double>(likenessLayerNeurons);
            var y = new List<double>(likenessLayerNeurons);

            var varianceX = new List<double>(likenessLayerNeurons);
            var varianceY = new List<double>(likenessLayerNeurons);
            using var sw = File.AppendText(path);
            for (var i = 0; i < 2 * 2 * likenessLayerNeurons; i += 4)
            {
                x.Add(parameters[i]);
                varianceX.Add(Math.Abs(parameters[i + 1]));
                y.Add(parameters[i + 2]);
                varianceY.Add(Math.Abs(parameters[i + 3]));
                // sw.WriteLine(
                // $"{parameters[i].ToString(CultureInfo.InvariantCulture)} {parameters[i + 2].ToString(CultureInfo.InvariantCulture)}");
            }

            sw.WriteLine(string.Join(", ", x.Select(val => val.ToString(CultureInfo.InvariantCulture))));
            sw.WriteLine(string.Join(", ", varianceX.Select(val => val.ToString(CultureInfo.InvariantCulture))));
            sw.WriteLine();
            sw.WriteLine(string.Join(", ", y.Select(val => val.ToString(CultureInfo.InvariantCulture))));
            sw.WriteLine(string.Join(", ", varianceY.Select(val => val.ToString(CultureInfo.InvariantCulture))));
            sw.WriteLine();
            sw.WriteLine();
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

            Console.WriteLine(nn.MeanSquareError(paramsTest));
        }

        private static void Task1()
        {
            var path = Root + "Files/Data/Task1/";
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