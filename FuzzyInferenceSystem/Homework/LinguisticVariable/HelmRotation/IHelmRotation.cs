using Homework.Sets;

namespace Homework.LinguisticVariable.HelmRotation
{
    public interface IHelmRotation : IFuzzySet
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }
    }
}