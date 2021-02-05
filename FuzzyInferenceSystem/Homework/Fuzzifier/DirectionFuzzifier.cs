using Homework.LinguisticVariable.Direction;
using Homework.Sets;

namespace Homework.Fuzzifier
{
    public static class DirectionFuzzifier
    {
        public static IFuzzySet Fuzzify(int value, string name)
        {
            if (value == 0)
                return new BadDirection(name);
            
            return new GoodDirection(name);
        }
    }
}