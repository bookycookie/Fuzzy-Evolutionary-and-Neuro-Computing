using System;
using System.Collections.Generic;
using System.Globalization;
using Homework_7.Activations;
using Homework_7.Helpers;

namespace Homework_7
{
    class Program
    {
        private const string Root = "C:/git/Fuzzy-Evolutionary-and-Neuro-Computing/Homework_7/";

        private static void Main(string[] args)
        {
            // Task1();
            var dataset = new Dataset();
            var nn = new NeuralNetwork(8, 3, 4);
            nn.CalculateOutput(1, 2, new double[90]);
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
