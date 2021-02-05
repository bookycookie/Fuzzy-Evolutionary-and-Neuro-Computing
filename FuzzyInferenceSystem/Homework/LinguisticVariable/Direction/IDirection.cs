using Homework.Sets;

namespace Homework.LinguisticVariable.Direction
{
    public interface IDirection : IFuzzySet
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }
    }
}