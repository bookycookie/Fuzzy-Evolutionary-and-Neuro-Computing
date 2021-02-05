using Homework.Sets;

namespace Homework.LinguisticVariable.Distance
{
    public interface IDistance : IFuzzySet
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }
    }
}