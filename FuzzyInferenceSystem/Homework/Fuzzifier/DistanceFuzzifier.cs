using Homework.LinguisticVariable.Distance;
using Homework.Sets;

namespace Homework.Fuzzifier
{
    public static class DistanceFuzzifier
    {
        public static IFuzzySet Fuzzify(int value, string name)
        {
            if (value < 60)
                return new ShortDistance(name);
            
            if (value >= 150)
                return new LongDistance(name);
            
            return new MiddleDistance(name);
        }
    }
}