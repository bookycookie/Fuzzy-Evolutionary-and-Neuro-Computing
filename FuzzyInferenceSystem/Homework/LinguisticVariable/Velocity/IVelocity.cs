using Homework.Sets;

namespace Homework.LinguisticVariable.Velocity
{
    public interface IVelocity : IFuzzySet
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }
    }
}