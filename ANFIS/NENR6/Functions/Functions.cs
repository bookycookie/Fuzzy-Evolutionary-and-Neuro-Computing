using System;

namespace NENR6.Functions
{
    public static class Functions
    {
        public static double Z(double x, double y) =>
            (Math.Pow(x - 1.0, 2) + Math.Pow(y + 2.0, 2) - 5.0 * x * y + 3.0) * Math.Pow(Math.Cos(x / 5.0), 2);

        public static double Sigmoid(double a, double b, double x) => 1.0 / (1.0 + Math.Exp(b * (x - a)));
    }
}