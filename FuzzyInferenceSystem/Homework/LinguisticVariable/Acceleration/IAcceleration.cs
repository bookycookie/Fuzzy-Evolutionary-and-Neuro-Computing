using Homework.Sets;

namespace Homework.LinguisticVariable.Acceleration
{
    public interface IAcceleration : IFuzzySet
    {
        public string Name { get; set; }
        
        public IFuzzySet FuzzySet { get; set; }
    }
}