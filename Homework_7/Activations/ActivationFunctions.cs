using System;

namespace Homework_7.Activations
{
    public static class ActivationFunctions
    {
        public static double Likeness(double[] x, double[] w, double[] s)
        {
            var sum = 0.0;
            for (var i = 0; i < x.Length; i++) 
                sum += Math.Abs(x[i] - w[i]) / Math.Abs(s[i]);

            return 1.0 / (1 + sum);
        }

        public static double Sigmoid(double input) => 1.0 / (1 + Math.Exp(-input));
    }
}