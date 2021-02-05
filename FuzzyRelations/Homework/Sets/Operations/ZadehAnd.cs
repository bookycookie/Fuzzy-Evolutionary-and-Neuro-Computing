using System;

namespace Homework.Sets.Operations
{
    public class ZadehAnd : IBinaryFunction
    {
        public double ValueAt(double value1, double value2) => Math.Min(value1, value2);
    }
}