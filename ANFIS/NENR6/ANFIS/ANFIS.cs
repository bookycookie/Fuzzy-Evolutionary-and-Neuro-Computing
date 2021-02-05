using System;
using System.Collections.Generic;
using System.Linq;
using NENR6.Helpers;

namespace NENR6.ANFIS
{
    public class ANFIS
    {
        private readonly List<Sample> _samples;
        private readonly List<Rule> _rules;
        private readonly List<Sample> _initialSamples;

        private const int LowerBound = -4;
        private const int UpperBound = 4;

        private readonly int _iterations;

        private static readonly Random Random = new Random();


        public ANFIS(int numberOfRules, int iterations)
        {
            _iterations = iterations;

            _rules = GenerateRules(numberOfRules);
            _samples = GenerateSamples();
            _initialSamples = _samples;
        }

        /// <summary>
        /// Samples the F(x, y) from the given [<see cref="LowerBound"/>, <see cref="UpperBound"/>] interval.
        /// </summary>
        /// <returns></returns>
        public static List<Sample> GenerateSamples()
        {
            var samples = new List<Sample>();
            for (var x = LowerBound; x <= UpperBound; x++)
            for (var y = LowerBound; y <= UpperBound; y++)
            {
                var sample = new Sample(x, y);
                samples.Add(sample);
            }

            return samples;
        }

        /// <summary>
        /// Generates a list of rules using a random seed for the activation coefficients.
        /// </summary>
        /// <param name="numberOfRules"></param>
        /// <returns></returns>
        private static List<Rule> GenerateRules(int numberOfRules)
        {
            var rules = new List<Rule>();
            for (var i = 0; i < numberOfRules; i++)
                rules.Add(new Rule(Random));

            return rules;
        }

        /// <summary>
        /// Runs the learning process of the ANFIS.
        /// </summary>
        /// <param name="useBatch">Flag whether to use batches or not.</param>
        /// <param name="batchSize">Indicates the size of the batch</param>
        /// <param name="eta">Eta for the inference nodes.</param>
        /// <param name="etaZ">Eta for the linear function.</param>
        /// <param name="verbose">Flag if true prints the resulting sample evaluations.</param>
        /// <param name="writeRulesToFile">Flag to write rules to a file.</param>
        /// <param name="saveErrors">Flag to save errors to a file.</param>
        /// <returns></returns>
        public List<Sample> Run(bool useBatch = false, int batchSize = 4, double eta = 0.001, double etaZ = 0.01,
            bool verbose = false, bool writeRulesToFile = false, bool saveErrors = false)
        {
            var iteration = 0;
            var errorList = new List<Sample>(_iterations);
            while (iteration < _iterations)
            {
                var sampleIdx = 0;
                foreach (var sample in _samples)
                {
                    sampleIdx++;

                    var o = Evaluate(sample);
                    var error = sample.Z - o;

                    foreach (var rule in _rules)
                    {
                        var alphaSum = SumOfTNorms(sample);
                        var wZiZjDifferenceSum = alphaSum * rule.Z(sample) - SumOfTNormsTimesZ(sample);

                        rule.CalculateCorrections(error, sample, wZiZjDifferenceSum, alphaSum, eta, etaZ);
                        if (!useBatch)
                            rule.Update();
                    }

                    if (useBatch && sampleIdx % batchSize == 0)
                        foreach (var rule in _rules)
                            rule.Update();
                }

                var mse = MeanSquaredError();
                errorList.Add(new Sample(iteration, mse));

                if (iteration % 10 == 0) 
                    Console.WriteLine($"i: {iteration} -- {mse}");
                
                iteration++;
            }

            if (verbose)
                foreach (var sample in _samples)
                    Console.WriteLine($"real: {sample} -- evaluated: {Evaluate(sample)}");



            if (writeRulesToFile)
                for (var i = 0; i < _rules.Count; i++)
                {
                    var listOfACoefs = new List<Sample>(_rules.Count);
                    var listOfBCoefs = new List<Sample>(_rules.Count);
                    for (var x = -4; x <= 4; x++)
                    {
                        listOfACoefs.Add(new Sample(x, Functions.Functions.Sigmoid(_rules[i].A, _rules[i].B, x)));
                        listOfBCoefs.Add(new Sample(x, Functions.Functions.Sigmoid(_rules[i].C, _rules[i].D, x)));
                    }

                    FileHelper.WriteToFile(listOfACoefs, $"C:/Faks/NENR/NENR6/NENR6/Data/Coefs/A/rule{i}.txt");
                    FileHelper.WriteToFile(listOfBCoefs, $"C:/Faks/NENR/NENR6/NENR6/Data/Coefs/B/rule{i}.txt");
                }
            if (saveErrors)
                FileHelper.WriteToFile(errorList, $"C:/Faks/NENR/NENR6/NENR6/Data/BGD/Etas/11.txt");

            var evaluatedSamples = _samples.Select(sample => new Sample(sample.X, sample.Y, Evaluate(sample))).ToList();

            return evaluatedSamples;
        }

        /// <summary>
        /// Ok = Sum(πi * zi) / Sum(πi)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private double Evaluate(Sample s)
        {
            var output = 0.0;
            var numerator = 0.0;
            var denominator = 0.0;

            foreach (var _ in _rules)
            {
                numerator += SumOfTNormsTimesZ(s);
                denominator += SumOfTNorms(s);
            }

            output = numerator / denominator;
            return output;
        }

        /// <summary>
        /// Mean squared error. 0.5 * (real - predicted)^2
        /// </summary>
        /// <returns></returns>
        private double MeanSquaredError() => _samples.Sum(s => 0.5 * Math.Pow(s.Z - Evaluate(s), 2)) / _samples.Count;

        /// <summary>
        /// Adds together the TNorm (product of memA(x) and memB(y)) of every rule.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private double SumOfTNorms(Sample s) => _rules.Sum(rule => rule.TNorm(s));

        /// <summary>
        /// Adds together the TNorm (product of memA(x) and memB(y)) multiplied by the calculated value of f(x, y) for every rule.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private double SumOfTNormsTimesZ(Sample s) => _rules.Sum(rule => rule.TNorm(s) * rule.Z(s));

        /// <summary>
        /// Prints samples to the console.
        /// </summary>
        /// <param name="samples"></param>
        private static void PrintSamples(IEnumerable<Sample> samples)
        {
            Console.WriteLine("SAMPLES");
            Console.WriteLine("==========================");
            Console.WriteLine(string.Join("\n", samples));
            Console.WriteLine("===========================");
        }
    }
}