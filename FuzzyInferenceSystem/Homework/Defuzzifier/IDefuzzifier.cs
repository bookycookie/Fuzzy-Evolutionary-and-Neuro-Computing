using Homework.Sets;

namespace Homework.Defuzzifier
{
    public interface IDefuzzifier
    {
        public int Defuzzify(IFuzzySet set);
    }
}