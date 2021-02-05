using Homework.LinguisticVariable.Velocity;
using Homework.Sets;

namespace Homework.Fuzzifier
{
    public static class VelocityFuzzifier
    {
        public static IFuzzySet Fuzzify(int value, string name)
        {
            if (value < 15)
                return new SlowVelocity(name);
            if (value > 30)
                return new HighVelocity(name);
            
            return new MediumVelocity(name);
        }
    }
}