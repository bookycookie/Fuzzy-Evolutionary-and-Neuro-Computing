using System;
using MathNet.Numerics.LinearAlgebra;

namespace Homework_7.Activations
{
    public static class ActivationFunctions
    {
        public static double Task1(double x, double w, double s) => 1.0 / (1 + Math.Abs(x - w) / Math.Abs(s));
    }
}