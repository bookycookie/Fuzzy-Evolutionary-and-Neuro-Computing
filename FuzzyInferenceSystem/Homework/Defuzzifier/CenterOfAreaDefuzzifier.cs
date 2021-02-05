using System;
using Homework.Sets;

namespace Homework.Defuzzifier
{
    public class CenterOfAreaDefuzzifier : IDefuzzifier
    {
        public int Defuzzify(IFuzzySet set)
        {
            var numerator = 0.0;
            var denominator = 0.0;
            foreach (var element in set.GetDomain())
            {
                numerator += set.GetValueAt(element) * element.GetComponentValue(0);
                denominator += Math.Round(set.GetValueAt(element));
            }

            if (denominator == 0.0) return 0;

            return (int) (numerator / denominator);
        }
    }
}