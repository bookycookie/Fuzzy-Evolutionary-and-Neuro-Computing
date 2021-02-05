using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.Acceleration
{
    public class PositiveAcceleration : IAcceleration
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public PositiveAcceleration(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(-50, 51), StandardFuzzySets.GammaFunction(0, 50));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();
        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}