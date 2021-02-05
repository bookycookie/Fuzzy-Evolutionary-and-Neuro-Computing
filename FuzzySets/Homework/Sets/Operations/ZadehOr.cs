using System;

namespace Homework.Sets.Operations
{
    public class ZadehOr : IBinaryFunction
    {
        public double ValueAt(double value1, double value2) => Math.Max(value1, value2);
    }
}